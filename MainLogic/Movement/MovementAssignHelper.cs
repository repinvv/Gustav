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
        Random rnd = new Random();

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
                    storage.Movement.Path.Enqueue(rnd.Next(1000) > 500 ? left : right);
                }
                else
                {
                    storage.Movement.Path.Enqueue(left);
                }
            }
            else
            {
                if (DestinationValid(right))
                {
                    storage.Movement.Path.Enqueue(right);
                }
                else
                {
                    bothInvalid();
                }
            }
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

        public DoublePoint CreateDestination(double target)
        {
            var distance = Settings.Default.StepDistance + rnd.NextDouble() * Settings.Default.StepDeviate;
            var angle = target + (-Settings.Default.ManeuverAngle + rnd.NextDouble() * 2 * Settings.Default.ManeuverAngle);
            return anglesCalculator.GetCoordinatesByAngle(distance, angle);
        }

        public DoublePoint CreateAroundDestination(EnemyData enemy, int angle)
        {
            var heading = anglesCalculator.GetHeadingTo(enemy.Position).AddAngle(angle);
            return anglesCalculator.GetCoordinatesByAngle(Settings.Default.CloseUpRange, heading, enemy.Position);
        }
    }
}
