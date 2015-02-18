namespace Gustav.RobotPosition
{
    using Robocode;

    internal static class PositionRegister
    {

        public static void OnScan(ScannedRobotEvent e)
        {
            if (e.IsSentryRobot)
            {
                return;
            }
            
            //var point = coordinatesCalculator.GetEnemyCoordinates(e.Distance, e.BearingRadians);
            //storage.StoreEnemy(e.Name, e.Heading, e.Velocity, point, e.Time);
        }
    }
}
