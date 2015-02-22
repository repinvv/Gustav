namespace Gustav.MainLogic.Movement
{
    using System;
    using System.Linq;
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Storage;

    class MovementAssign
    {
        private readonly CombatParametersStorage storage;

        public MovementAssign(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public void AssignDestination(EnemyData enemy)
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
