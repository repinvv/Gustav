namespace Gustav.MainLogic.Movement
{
    using System;
    using System.Linq;
    using Gustav.MathServices;
    using Gustav.Storage;

    class RandomMovementAssign
    {
        private readonly CombatParametersStorage storage;

        public RandomMovementAssign(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public void AssignDestination()
        {
            if (storage.Movement.Path.Any())
            {
                return;
            }

            var rnd = new Random();
            var x = 25 + rnd.NextDouble() * (storage.Robot.BattleFieldWidth - 50);
            var y = 25 + rnd.NextDouble() * (storage.Robot.BattleFieldHeight - 50);
            storage.Movement.Path.Enqueue(new DoublePoint(x, y));
        }
    }
}
