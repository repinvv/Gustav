namespace Gustav.MainLogic.Engage
{
    using System;
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Properties;
    using Gustav.Storage;
    using Robocode;

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
            var heading = anglesCalculator.GetHeadingTo(enemy.Position);
            var diff = anglesCalculator.GetHeadingDiff(heading, storage.Robot.RadarHeading);
            heading = diff > 0 ? heading.AddAngle(Settings.Default.ScanArc) : heading.AddAngle(-Settings.Default.ScanArc);
            diff = anglesCalculator.GetHeadingDiff(heading, storage.Robot.RadarHeading);
            diff = anglesCalculator.GetHeadingDiff(diff, rates.BodyTurn);
            diff = anglesCalculator.GetHeadingDiff(diff, rates.TurretTurn);
            rates.RadarTurn = !(diff < 0) ? Math.Min(diff, Rules.RADAR_TURN_RATE) : Math.Max(diff, -Rules.RADAR_TURN_RATE);
        }
    }
}
