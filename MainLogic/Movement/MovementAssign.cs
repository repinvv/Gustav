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
        private readonly EnemyDataStorage enemyDataStorage;
        private readonly RandomMovementAssign randomMovementAssign;

        public MovementAssign(CombatParametersStorage storage, AnglesCalculator anglesCalculator, MovementAssignHelper helper, EnemyDataStorage enemyDataStorage, RandomMovementAssign randomMovementAssign)
        {
            this.storage = storage;
            this.anglesCalculator = anglesCalculator;
            this.helper = helper;
            this.enemyDataStorage = enemyDataStorage;
            this.randomMovementAssign = randomMovementAssign;
        }

        public void AssignDestination(EnemyData enemy)
        {
            if (storage.Movement.Destination != null)
            {
                return;
            }

            var range = Settings.Default.CombatRange;
            if (storage.Robot.Energy > Settings.Default.AggressionHealth && 
                storage.Robot.Others < 5 &&
                storage.Robot.Energy - enemy.Energy > enemy.Energy * Settings.Default.CloseUpHealthGap)
            {
                range = Settings.Default.CloseUpRange;
            }

            if (Math.Abs(enemy.Distance - range) > Settings.Default.StepDistance)
            {
                var destination = PullRange(enemy, range);
                if (helper.DestinationValid(destination))
                {
                    storage.Movement.Destination = destination;
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
            helper.DecideMovement(left, right, () => randomMovementAssign.AssignDestination());
        }

        private DoublePoint MoveRight(EnemyData enemy)
        {
            return helper.CreateDestination(anglesCalculator.GetHeadingTo(enemy.Position).AddAngle(70), Settings.Default.ManeuverAngle);
        }

        private DoublePoint MoveLeft(EnemyData enemy)
        {
            return helper.CreateDestination(anglesCalculator.GetHeadingTo(enemy.Position).AddAngle(290), Settings.Default.ManeuverAngle);
        }

        private DoublePoint PullRange(EnemyData enemy, double range)
        {
            if (enemy.Distance < range)
            {
                double target =  anglesCalculator.GetHeadingTo(enemy.Position).AddAngle(180);
                return helper.CreateDestination(target, Settings.Default.ManeuverAngle);
            }
            else
            {
                double target = anglesCalculator.GetHeadingTo(enemy.Position);
                return helper.CreateDestination(target, Settings.Default.CloseUpAngle);
            }
        }
    }
}
