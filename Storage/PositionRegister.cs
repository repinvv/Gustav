namespace Gustav.Position
{
    using Gustav.MathServices;
    using Gustav.Storage;
    using Robocode;

    internal class EventRegister
    {
        private readonly AnglesCalculator anglesCalculator;
        private readonly EnemyDataStorage enemyDataStorage;
        private readonly CombatParametersStorage storage;

        public EventRegister(AnglesCalculator anglesCalculator, EnemyDataStorage enemyDataStorage, CombatParametersStorage storage)
        {
            this.anglesCalculator = anglesCalculator;
            this.enemyDataStorage = enemyDataStorage;
            this.storage = storage;
        }

        public void OnScan(ScannedRobotEvent e)
        {
            if (e.IsSentryRobot)
            {
                return;
            }
            
            var point = anglesCalculator.GetCoordinatesByAngle(e.Distance, storage.Robot.Heading.AddAngle(e.Bearing));
            enemyDataStorage.StoreEnemy(new EnemyData
                               {
                                   Position = point,
                                   Name = e.Name,
                                   Distance = e.Distance,
                                   Heading = e.Heading,
                                   Velocity = e.Velocity,
                                   Energy = e.Energy,
                                   LastSeen = e.Time
                               });
        }

        public void OnCollision(HitRobotEvent e)
        {
            var point = anglesCalculator.GetCoordinatesByAngle(storage.Robot.Height, storage.Robot.Heading.AddAngle(e.Bearing));

            enemyDataStorage.StoreEnemy(new EnemyData
            {
                Position = point,
                Name = e.Name,
                Distance = storage.Robot.Height,
                Heading = 0,
                Velocity = 0,
                Energy = e.Energy,
                LastSeen = e.Time
            });

            enemyDataStorage.Collision = e.Name;
        }

        public void OnDeath(RobotDeathEvent e)
        {
            enemyDataStorage.RemoveEnemy(e.Name);
        }
    }
}
