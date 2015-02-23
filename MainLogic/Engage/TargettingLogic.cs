namespace Gustav.MainLogic.Engage
{
    using System;
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Properties;
    using Gustav.Storage;
    using Robocode;

    class TargettingLogic
    {
        private readonly CombatParametersStorage storage;
        private readonly TurretHeadingCalculator turretHeadingCalculator;
        private readonly AnglesCalculator anglesCalculator;
        private readonly MaxFireDistanceSelector maxFireDistanceSelector;

        public TargettingLogic(CombatParametersStorage storage, TurretHeadingCalculator turretHeadingCalculator, AnglesCalculator anglesCalculator, MaxFireDistanceSelector maxFireDistanceSelector)
        {
            this.storage = storage;
            this.turretHeadingCalculator = turretHeadingCalculator;
            this.anglesCalculator = anglesCalculator;
            this.maxFireDistanceSelector = maxFireDistanceSelector;
        }

        public void DetermineTargettingRates(Rates rates, EnemyData enemy)
        {
            var currentHeading = turretHeadingCalculator.GetCurrentTurnHeading(enemy);
            var diff = anglesCalculator.GetHeadingDiff(currentHeading, storage.Robot.GunHeading);
            var miss = Math.Abs(diff.Sin() * enemy.Distance);
            if (miss < Settings.Default.TargettingTolerance && Math.Abs(storage.Robot.GunHeat) < Settings.Default.ComparisionTolerance)
            {
                var save = enemy.Distance > maxFireDistanceSelector.GetMaxFireDistance(enemy) ? Settings.Default.LongDistanceSave : Settings.Default.BattleSave;
                if (storage.Engage.Random.NextDouble() < save)
                {
                    rates.BulletPower = storage.Engage.BulletPower;
                    storage.Engage.LastFired = storage.Robot.Time;
                }
            }

            var nextHeading = turretHeadingCalculator.GetNextTurnHeading(enemy);
            diff = anglesCalculator.GetHeadingDiff(nextHeading, storage.Robot.GunHeading);
            var neededRate = anglesCalculator.GetHeadingDiff(diff, rates.BodyTurn);

            if (neededRate < 0)
            {
                UpdateRatesNegative(neededRate, rates, enemy);
            }
            else
            {
                UpdateRatesPositive(neededRate, rates, enemy);
            }

        }

        private void UpdateRatesNegative(double neededRate, Rates rates, EnemyData enemy)
        {
            if (neededRate >= -Rules.GUN_TURN_RATE || enemy.Distance < storage.Robot.Height * 1.5)
            {
                rates.TurretTurn = Math.Max(neededRate, -Rules.GUN_TURN_RATE);
                return;
            }

            var bodyAdjust = neededRate.AddAngle(Rules.GUN_TURN_RATE);
            if (bodyAdjust < -2)
            {
                rates.Velocity = rates.Velocity / 2;
            }

            rates.BodyTurn = Math.Max(rates.BodyTurn.AddAngle(bodyAdjust), -Rules.MAX_TURN_RATE);
            rates.TurretTurn = -Rules.GUN_TURN_RATE;
        }

        private void UpdateRatesPositive(double neededRate, Rates rates, EnemyData enemy)
        {
            if (neededRate <= Rules.GUN_TURN_RATE || enemy.Distance < storage.Robot.Height * 1.5)
            {
                rates.TurretTurn = Math.Min(neededRate, Rules.GUN_TURN_RATE);
                return;
            }

            var bodyAdjust = neededRate.AddAngle(-Rules.GUN_TURN_RATE);
            if (bodyAdjust > 2)
            {
                rates.Velocity = rates.Velocity / 2;
            }

            rates.BodyTurn = Math.Min(rates.BodyTurn.AddAngle(bodyAdjust), Rules.MAX_TURN_RATE);
            rates.TurretTurn = Rules.GUN_TURN_RATE;
        }
    }
}
