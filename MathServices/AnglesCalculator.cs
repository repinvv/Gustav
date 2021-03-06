﻿namespace Gustav.MathServices
{
    using System;
    using Gustav.Properties;
    using Gustav.Storage;
    using Robocode;

    internal class AnglesCalculator
    {
        private readonly CombatParametersStorage storage;

        public AnglesCalculator(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public DoublePoint GetCoordinatesByAngle(double distance, double angle)
        {
            return GetCoordinatesByAngle(distance, angle, new DoublePoint(storage.Robot.X, storage.Robot.Y));
        }

        public DoublePoint GetCoordinatesByAngle(double distance, double angle, DoublePoint position)
        {
            // angle = angle.NormalizeAngle();
            var x = angle.Sin() * distance + position.X;
            var y = angle.Cos() * distance + position.Y;
            return new DoublePoint(x, y);
        }

        public double GetHeadingTo(DoublePoint point)
        {
            return GetHeading(point, new DoublePoint(storage.Robot.X, storage.Robot.Y));
        }

        public double GetHeading(DoublePoint point, DoublePoint from)
        {
            return Math.Atan2(point.X - from.X, point.Y - from.Y).ToDegrees().NormalizeAngle();
        }

        public double GetHeadingDiff(double heading, double heading1)
        {
            return (((((heading - heading1) % 360) + 540) % 360) - 180);
        }

        public double GetDistance(DoublePoint point1, DoublePoint point2)
        {
            var x = point1.X - point2.X;
            var y = point1.Y - point2.Y;
            return Math.Sqrt(x * x + y * y);
        }
    }
}
