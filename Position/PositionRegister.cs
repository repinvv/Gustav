namespace Gustav.Position
{
    using Gustav.MathServices;
    using Gustav.Storage;
    using Robocode;

    internal class PositionRegister
    {
        private readonly CoordinatesCalculator coordinatesCalculator;
        private readonly EnemyDataStorage storage;

        public PositionRegister(CoordinatesCalculator coordinatesCalculator, EnemyDataStorage storage)
        {
            this.coordinatesCalculator = coordinatesCalculator;
            this.storage = storage;
        }

        public void OnScan(ScannedRobotEvent e)
        {
            if (e.IsSentryRobot)
            {
                return;
            }
            
            var point = coordinatesCalculator.GetEnemyCoordinates(e.Distance, e.BearingRadians);
            storage.StoreEnemy(new EnemyData
                               {
                                   Position = point,
                                   Name = e.Name,
                                   Heading = e.HeadingRadians,
                                   Velocity = e.Velocity,
                                   Energy = e.Energy,
                                   LastSeen = e.Time
                               });
        }
    }
}
