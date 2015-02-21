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

        public BulletPowerCalculator(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public void CalculateBulletPower(EnemyData enemy)
        {
            var reduction = (enemy.Distance - Settings.Default.MaxPowerDistance)/Settings.Default.DistanceRate;
            var power = Settings.Default.MaxPower - reduction;
            power = Math.Min(power, Settings.Default.MinPower);
            storage.Engage.BulletPower = power;
        }
    }
}
