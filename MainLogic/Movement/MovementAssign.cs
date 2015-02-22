namespace Gustav.MainLogic.Movement
{
    using System;
    using System.Linq;
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Properties;
    using Gustav.Storage;

    class MovementAssign
    {
        private readonly CombatParametersStorage storage;
        private readonly AnglesCalculator anglesCalculator;
        private readonly MovementAssignHelper helper;

        public MovementAssign(CombatParametersStorage storage, AnglesCalculator anglesCalculator, MovementAssignHelper helper)
        {
            this.storage = storage;
            this.anglesCalculator = anglesCalculator;
            this.helper = helper;
        }

        public void AssignDestination(EnemyData enemy)
        {
            if (storage.Movement.Path.Any())
            {
                return;
            }

            var range = Settings.Default.CombatRange;
            if (storage.Robot.Energy - enemy.Energy > enemy.Energy * Settings.Default.CloseUpHealthGap)
            {
                range = Settings.Default.CloseUpRange;
            }

            if (Math.Abs(enemy.Distance - range) < Settings.Default.StepDistance)
            {
                var destination = PullRange(enemy, range);
                if (helper.DestinationValid(destination))
                {
                    storage.Movement.Path.Enqueue(destination);
                    return;
                }
            }

            var left = MoveLeft(enemy);
            var right = MoveRight(enemy);
            helper.DecideMovement(left, right, () => GoAround(enemy));
        }

        private void GoAround(EnemyData enemy)
        {
            var right = helper.CreateAroundDestination(enemy, 120);
            var left = helper.CreateAroundDestination(enemy, -120);
            helper.DecideMovement(left, right, () => Ram(enemy));
        }

        private void Ram(EnemyData enemy)
        {
            storage.Movement.Path.Enqueue(enemy.Position);
        }

        private DoublePoint MoveRight(EnemyData enemy)
        {
            return helper.CreateDestination(anglesCalculator.GetHeadingTo(enemy.Position).AddAngle(90));
        }

        private DoublePoint MoveLeft(EnemyData enemy)
        {
            return helper.CreateDestination(anglesCalculator.GetHeadingTo(enemy.Position).AddAngle(270));
        }

        private DoublePoint PullRange(EnemyData enemy, double range)
        {
            double target = enemy.Distance < range ? anglesCalculator.GetHeadingTo(enemy.Position).AddAngle(180) : anglesCalculator.GetHeadingTo(enemy.Position);
            return helper.CreateDestination(target);
        }
    }
}
