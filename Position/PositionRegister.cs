namespace Gustav.Position
{
    using Gustav.MathServices;
    using Gustav.Storage;
    using Robocode;

    internal class PositionRegister
    {
        private readonly AnglesCalculator anglesCalculator;
        private readonly EnemyDataStorage enemyDataStorage;
        private readonly CombatParametersStorage storage;

        public PositionRegister(AnglesCalculator anglesCalculator, EnemyDataStorage enemyDataStorage, CombatParametersStorage storage)
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
                                   Heading = e.HeadingRadians,
                                   Velocity = e.Velocity,
                                   Energy = e.Energy,
                                   LastSeen = e.Time
                               });
        }
    }
}
