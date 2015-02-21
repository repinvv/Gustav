namespace Gustav.Position
{
    using Gustav.MathServices;
    using Gustav.Storage;
    using Robocode;

    internal class PositionRegister
    {
        private readonly AnglesCalculator anglesCalculator;
        private readonly EnemyDataStorage storage;

        public PositionRegister(AnglesCalculator anglesCalculator, EnemyDataStorage storage)
        {
            this.anglesCalculator = anglesCalculator;
            this.storage = storage;
        }

        public void OnScan(ScannedRobotEvent e)
        {
            if (e.IsSentryRobot)
            {
                return;
            }
            
            var point = anglesCalculator.GetCoordinatesByAngle(e.Distance, e.Bearing);
            storage.StoreEnemy(new EnemyData
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
