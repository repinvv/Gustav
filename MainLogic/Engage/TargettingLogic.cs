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
        private readonly EnemyRotationCalculator rotationCalculator;

        public TargettingLogic(CombatParametersStorage storage, TurretHeadingCalculator turretHeadingCalculator, AnglesCalculator anglesCalculator, EnemyRotationCalculator rotationCalculator)
        {
            this.storage = storage;
            this.turretHeadingCalculator = turretHeadingCalculator;
            this.anglesCalculator = anglesCalculator;
            this.rotationCalculator = rotationCalculator;
        }

        public void DetermineTargettingRates(Rates rates, EnemyData enemy)
        {
            var currentHeading = turretHeadingCalculator.GetCurrentTurnHeading(enemy);
            var diff = anglesCalculator.GetHeadingDiff(currentHeading, storage.Robot.GunHeading);
            var miss = Math.Abs(diff.Sin() * enemy.Distance);
            rates.BulletPower = 0;
            if (miss < Settings.Default.TargettingTolerance && Math.Abs(storage.Robot.GunHeat) < Settings.Default.ComparisionTolerance)
            {
                var maxfire = Settings.Default.MaxFireDistance;
                if (Math.Abs(enemy.Velocity) > 2)
                {
                    maxfire -= 100;
                    if (Math.Abs(rotationCalculator.GetEnemyRotation(enemy)) > 1)
                    {
                        maxfire -= 100;
                    }
                }

                if (enemy.Distance <= maxfire)
                {
                    rates.BulletPower = storage.Engage.BulletPower;
                }
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
