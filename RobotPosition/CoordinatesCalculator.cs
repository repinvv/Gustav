namespace Gustav.RobotPosition
{
    using System.Drawing;

    internal class CoordinatesCalculator
    {
        public Point GetEnemyCoordinates(double distance, double bearing)
        {
            var x = RobotContainer.Robot.X;
            var y = RobotContainer.Robot.Y;
            var heading = RobotContainer.Robot.RadarHeadingRadians;
            return new Point();
        }
    }
}
