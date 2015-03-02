namespace Gustav.MainLogic.Movement
{
    using System;
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Properties;
    using Gustav.Storage;

    class MovementAssignHelper
    {
        private readonly CombatParametersStorage storage;
        private readonly AnglesCalculator anglesCalculator;

        public MovementAssignHelper(CombatParametersStorage storage, AnglesCalculator anglesCalculator)
        {
            this.storage = storage;
            this.anglesCalculator = anglesCalculator;
        }

        public void DecideMovement(DoublePoint left, DoublePoint right, Action bothInvalid)
        {
            if (DestinationValid(left))
            {
                if (DestinationValid(right))
                {
                    storage.Movement.Destination = storage.Movement.Random.Next(GetRandomThreshold()) > 500 ? left : right;
                }
                else
                {
                    storage.Movement.Destination = left;
                }
            }
            else
            {
                if (DestinationValid(right))
                {
                    storage.Movement.Destination = right;
                }
                else
                {
                    bothInvalid();
                }
            }
        }

        private int GetRandomThreshold()
        {
            if (storage.Movement.Threshold > 750 || storage.Movement.Threshold < 250)
            {
                return storage.Movement.Threshold = 500;
            }

            return storage.Movement.Threshold = storage.Movement.Threshold + (storage.Movement.Random.Next(1000) > storage.Movement.Threshold ? 25 : - 25);
        }

        public bool DestinationValid(DoublePoint point)
        {
            if (point.X < Settings.Default.BorderRange || point.Y < Settings.Default.BorderRange)
            {
                return false;
            }

            if (point.X > storage.Robot.BattleFieldWidth - Settings.Default.BorderRange)
            {
                return false;
            }

            if (point.Y > storage.Robot.BattleFieldHeight - Settings.Default.BorderRange)
            {
                return false;
            }

            return true;
        }

        public DoublePoint CreateDestination(double target, double maneuverAngle)
        {
            var distance = Settings.Default.StepDistance + storage.Movement.Random.GaussDouble() * Settings.Default.StepDeviate;
            var angle = target + (-maneuverAngle + storage.Movement.Random.GaussDouble() * 2 * maneuverAngle);
            return anglesCalculator.GetCoordinatesByAngle(distance, angle);
        }

        public DoublePoint CreateAroundDestination(EnemyData enemy, int angle)
        {
            var heading = anglesCalculator.GetHeadingTo(enemy.Position).AddAngle(angle);
            return anglesCalculator.GetCoordinatesByAngle(Settings.Default.CloseUpRange, heading, enemy.Position);
        }
    }
}
