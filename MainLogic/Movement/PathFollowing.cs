namespace Gustav.MainLogic.Movement
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Gustav.MathServices;
    using Gustav.Properties;
    using Gustav.Storage;
    using Robocode;

    class PathFollowing
    {
        private readonly CombatParametersStorage storage;
        private readonly AnglesCalculator anglesCalculator;

        public PathFollowing(CombatParametersStorage storage, AnglesCalculator anglesCalculator)
        {
            this.storage = storage;
            this.anglesCalculator = anglesCalculator;
        }

        public void FollowPath(Rates rates)
        {
            if (!storage.Movement.Path.Any())
            {
                return;
            }

            var target = storage.Movement.Path.Peek();
            var robot = new DoublePoint(storage.Robot.X, storage.Robot.Y);
            if (anglesCalculator.GetDistance(target, robot) < (storage.Robot.Height / 2))
            {
                storage.Movement.Path.Dequeue();
                return;
            }

            var heading = anglesCalculator.GetHeadingTo(target);
            var diff = anglesCalculator.GetHeadingDiff(heading, storage.Robot.Heading);
            if (Math.Abs(diff) <= 90)
            {
                MoveForward(diff, rates);
            }
            else
            {
                MoveBackward(diff, rates);
            }
        }

        private void MoveForward(double target, Rates rates)
        {
            rates.BodyTurn = target > 0 ? Math.Min(target, Rules.MAX_TURN_RATE) : Math.Max(target, -Rules.MAX_TURN_RATE);
            rates.Velocity = Math.Abs(rates.BodyTurn) > 5 ? Settings.Default.TurnVelocity : Rules.MAX_VELOCITY;
        }

        private void MoveBackward(double target, Rates rates)
        {
            target = anglesCalculator.GetHeadingDiff(storage.Robot.Heading.AddAngle(180), target);
            rates.BodyTurn = target > 0 ? Math.Min(target, Rules.MAX_TURN_RATE) : Math.Max(target, -Rules.MAX_TURN_RATE);
            rates.Velocity = Math.Abs(rates.BodyTurn) > 5 ? -Settings.Default.TurnVelocity : -Rules.MAX_VELOCITY;
        }
    }
}
