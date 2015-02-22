namespace Gustav.MainLogic
{
    using Gustav.MathServices;
    using Gustav.Storage;
    using Robocode;

    class ScanLogic
    {
        private readonly CombatParametersStorage storage;
        private readonly AnglesCalculator anglesCalculator;
        private readonly EnemyDataStorage enemyDataStorage;
        private readonly ModeSelector modeSelector;

        public ScanLogic(CombatParametersStorage storage, AnglesCalculator anglesCalculator, EnemyDataStorage enemyDataStorage, ModeSelector modeSelector)
        {
            this.storage = storage;
            this.anglesCalculator = anglesCalculator;
            this.enemyDataStorage = enemyDataStorage;
            this.modeSelector = modeSelector;
        }

        public Rates DetermineRates()
        {
            var scan = GetScanParameters();
            if (enemyDataStorage.HaveActiveEnemy())
            {
                modeSelector.SelectMode(CombatMode.Engage);
                return null;
            }

            var direction = scan.Clockwize ? 1 : -1;
            if (ScanEnded(scan))
            {
                scan.Stage++;

                if (scan.Stage > 2)
                {
                    modeSelector.SelectMode(CombatMode.Search);
                    return null;
                }

                scan.TargetHeading = scan.TargetHeading.AddAngle(180);
            }

            return new Rates
                   {
                       BodyTurn = direction * Rules.MAX_TURN_RATE,
                       TurretTurn = direction * Rules.GUN_TURN_RATE,
                       RadarTurn = direction * Rules.RADAR_TURN_RATE,
                   };
        }

        private bool ScanEnded(ScanParameters scan)
        {
            var direction = scan.Clockwize ? 1 : -1;
            var diff = direction * anglesCalculator.GetHeadingDiff(scan.TargetHeading, storage.Robot.RadarHeading);
            return diff >= 0 && diff < 90;
        }

        private ScanParameters GetScanParameters()
        {
            if (storage.Scan != null)
            {
                return storage.Scan;
            }

            var centerBearing = anglesCalculator.GetHeadingToCoordinates(storage.Robot.BattleFieldWidth / 2, storage.Robot.BattleFieldHeight / 2);
            var diff = anglesCalculator.GetHeadingDiff(centerBearing, storage.Robot.Heading);
            var clockwize = diff > 0;
            var scan = storage.Scan = new ScanParameters
                       {
                           Clockwize = clockwize,
                           Stage = 0,
                           TargetHeading = storage.Robot.RadarHeading.AddAngle(180)
                       };
            return scan;
        }
    }
}
