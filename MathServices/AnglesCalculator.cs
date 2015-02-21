namespace Gustav.MathServices
{
    using System;
    using Gustav.Storage;

    internal class AnglesCalculator
    {
        private readonly CombatParametersStorage storage;

        public AnglesCalculator(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public DoublePoint GetCoordinatesByAngle(double distance, double angle)
        {
            angle = angle.NormalizeAngle();
            var x = angle.Sin() * distance + storage.Robot.X;
            var y = angle.Cos() * distance + storage.Robot.Y;
            return new DoublePoint(x, y);
        }

        public double GetBearingToCoordinates(double x, double y)
        {
            return Math.Atan2(x - storage.Robot.X, y - storage.Robot.Y).ToDegrees().NormalizeAngle();
        }

        public double GetBearingDiff(double bearing, double heading)
        {
            return (((((bearing - heading) % 360) + 540) % 360) - 180);
        }
    }
}
