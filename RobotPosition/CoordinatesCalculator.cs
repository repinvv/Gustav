namespace Gustav.RobotPosition
{
    using System.Drawing;

    internal class CoordinatesCalculator
    {
        private readonly RobotContainer container;

        public CoordinatesCalculator(RobotContainer container)
        {
            this.container = container;
        }

        public Point GetEnemyCoordinates(double distance, double bearing)
        {
            var x = container.Robot.X;
            var y = container.Robot.Y;
            var heading = container.Robot.RadarHeadingRadians;
            return new Point();
        }
    }
}
