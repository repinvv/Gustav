namespace Gustav.MainLogic.Engage
{
    using System;
    using Gustav.Position;
    using Gustav.Properties;
    using Gustav.Storage;
    using Robocode;

    class BulletPowerCalculator
    {
        private readonly CombatParametersStorage storage;
        private readonly EnemyRotationCalculator rotationCalculator;
        private readonly MaxFireDistanceSelector maxFireDistanceSelector;

        public BulletPowerCalculator(CombatParametersStorage storage, EnemyRotationCalculator rotationCalculator, MaxFireDistanceSelector maxFireDistanceSelector)
        {
            this.storage = storage;
            this.rotationCalculator = rotationCalculator;
            this.maxFireDistanceSelector = maxFireDistanceSelector;
        }

        public void CalculateBulletPower(EnemyData enemy)
        {
            if (enemy.Distance > maxFireDistanceSelector.GetMaxFireDistance(enemy))
            {
                storage.Engage.BulletPower = Rules.MIN_BULLET_POWER;
                return;
            }

            var distanceReduction = (enemy.Distance - Settings.Default.MaxPowerDistance)/Settings.Default.DistanceReductionRate;
            var rotationReduction = rotationCalculator.GetEnemyRotation(enemy) / Settings.Default.RotationReductionRate;
            var power = Rules.MAX_BULLET_POWER - distanceReduction - rotationReduction;
            power = Math.Max(power, Settings.Default.MinPower);
            storage.Engage.BulletPower = power;
        }
    }
}
