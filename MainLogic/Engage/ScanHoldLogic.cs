namespace Gustav.MainLogic.Engage
{
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Properties;
    using Gustav.Storage;

    class ScanHoldLogic
    {
        private readonly AnglesCalculator anglesCalculator;
        private readonly CombatParametersStorage storage;

        public ScanHoldLogic(AnglesCalculator anglesCalculator, CombatParametersStorage storage)
        {
            this.anglesCalculator = anglesCalculator;
            this.storage = storage;
        }

        public void DetermineScanRates(Rates rates, EnemyData enemy)
        {
            var bearing = anglesCalculator.GetBearingToCoordinates(enemy.Position.X, enemy.Position.Y);
            var diff = anglesCalculator.GetBearingDiff(bearing, storage.Robot.RadarHeading);
            bearing = diff > 0 ? bearing.AddAngle(Settings.Default.ScanArc) : bearing.AddAngle(-Settings.Default.ScanArc);

        }
    }
}
