namespace Gustav.MathServices
{
    using System;
    using Gustav.Storage;

    internal class CoordinatesCalculator
    {
        private readonly CombatParametersStorage storage;

        public CoordinatesCalculator(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public DoublePoint GetCoordinatesByAngle(double distance, double angle)
        {
            var x = Math.Cos(angle) * distance + storage.Robot.X;
            var y = Math.Sin(angle) * distance + storage.Robot.Y;

            return new DoublePoint(x.ToDegrees(), y.ToDegrees());
        }

        public double GetBearingToCoordinates(double x, double y)
        {
            return Math.Atan2(x - storage.Robot.X, y - storage.Robot.Y).ToDegrees();
        }

        public double GetBearingDiff(double bearing, double heading)
        {
            return (((((bearing - heading) % 360) + 540) % 360) - 180);
        }
    }
}
