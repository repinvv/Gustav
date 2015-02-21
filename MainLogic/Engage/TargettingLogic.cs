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

        public TargettingLogic(CombatParametersStorage storage, TurretHeadingCalculator turretHeadingCalculator, AnglesCalculator anglesCalculator)
        {
            this.storage = storage;
            this.turretHeadingCalculator = turretHeadingCalculator;
            this.anglesCalculator = anglesCalculator;
        }

        public void DetermineTargettingRates(Rates rates, EnemyData enemy)
        {
            var currentHeading = turretHeadingCalculator.GetCurrentTurnHeading(enemy);
            var diff = anglesCalculator.GetHeadingDiff(currentHeading, storage.Robot.GunHeading);
            var miss = Math.Abs(diff.Sin() * enemy.Distance);
            if (miss < Settings.Default.TargettingTolerance && Math.Abs(storage.Robot.GunHeat) < Settings.Default.ComparisionTolerance)
            {
                rates.BulletPower = storage.Engage.BulletPower;
            }
            else
            {
                rates.BulletPower = 0;
            }

            var nextHeading = turretHeadingCalculator.GetNextTurnHeading(enemy);
            diff = anglesCalculator.GetHeadingDiff(nextHeading, storage.Robot.GunHeading);
            var neededRate = anglesCalculator.GetHeadingDiff(diff, rates.BodyTurn);

            if (neededRate < 0)
            {
                UpdateRatesNegative(neededRate, rates);
            }
            else
            {
                UpdateRatesPositive(neededRate, rates);
            }

        }

        private void UpdateRatesNegative(double neededRate, Rates rates)
        {
            if (neededRate >= -Rules.GUN_TURN_RATE)
            {
                rates.TurretTurn = neededRate;
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

        private void UpdateRatesPositive(double neededRate, Rates rates)
        {
            if (neededRate <= Rules.GUN_TURN_RATE)
            {
                rates.TurretTurn = neededRate;
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
