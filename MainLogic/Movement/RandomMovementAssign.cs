namespace Gustav.MainLogic.Movement
{
    using System;
    using System.Linq;
    using Gustav.MathServices;
    using Gustav.Properties;
    using Gustav.Storage;
    using Robocode;

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
            var x = Rules.RADAR_SCAN_RADIUS / 2 + rnd.NextDouble() * (storage.Robot.BattleFieldWidth - Rules.RADAR_SCAN_RADIUS);
            var y = Rules.RADAR_SCAN_RADIUS / 2 + rnd.NextDouble() * (storage.Robot.BattleFieldHeight - Rules.RADAR_SCAN_RADIUS);
            storage.Movement.Path.Enqueue(new DoublePoint(x, y));
        }
    }
}
