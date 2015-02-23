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
            if (storage.Movement.Destination == null)
            {
                return;
            }

            var target = storage.Movement.Destination;
            var robot = new DoublePoint(storage.Robot.X, storage.Robot.Y);
            if (anglesCalculator.GetDistance(target, robot) < (storage.Robot.Height / 2))
            {
                storage.Movement.Destination = null;
                return;
            }

            var heading = anglesCalculator.GetHeadingTo(target);
            var diff = anglesCalculator.GetHeadingDiff(heading, storage.Robot.Heading);
            var direction = 1;
            if (Math.Abs(diff) > 90)
            {
                direction = -1;
                diff = anglesCalculator.GetHeadingDiff(heading, storage.Robot.Heading.AddAngle(180));
            }

            rates.BodyTurn = diff > 0 ? Math.Min(diff, Rules.MAX_TURN_RATE) : Math.Max(diff, -Rules.MAX_TURN_RATE);
            rates.Velocity = direction * (Math.Abs(rates.BodyTurn) > 5 ? Settings.Default.TurnVelocity : Rules.MAX_VELOCITY);
        }
    }
}
