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

        public DoublePoint GetEnemyCoordinates(double distance, double bearing)
        {
            var x = Math.Cos(bearing) * distance + storage.Robot.X;
            var y = Math.Sin(bearing) * distance + storage.Robot.Y;

            return new DoublePoint(x, y);
        }
    }
}
