namespace Gustav.MainLogic
{
    using System;
    using System.Data;
    using Gustav.MathServices;
    using Gustav.Properties;
    using Gustav.Storage;
    using Robocode;

    class ScanLogic
    {
        private readonly CombatParametersStorage storage;
        private readonly AnglesCalculator anglesCalculator;
        private readonly EnemyDataStorage enemyDataStorage;

        public ScanLogic(CombatParametersStorage storage, AnglesCalculator anglesCalculator, EnemyDataStorage enemyDataStorage)
        {
            this.storage = storage;
            this.anglesCalculator = anglesCalculator;
            this.enemyDataStorage = enemyDataStorage;
        }

        public Rates DetermineRates()
        {
            var scan = GetScanParameters();
            if (enemyDataStorage.HaveActiveEnemy())
            {
                storage.CombatMode = CombatMode.Engage;
                return null;
            }

            var direction = scan.Clockwize ? 1 : -1;
            if (ScanEnded(scan))
            {
                scan.Stage++;

                if (scan.Stage > 1)
                {
                    storage.CombatMode = CombatMode.Search;
                    return null;
                }

                scan.TargetBearing = scan.TargetBearing.AddAngle(scan.Clockwize ? 179 : -179);
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
            var diff = direction * anglesCalculator.GetBearingDiff(scan.TargetBearing, storage.Robot.RadarHeading);
            return diff >= 0;
        }

        private ScanParameters GetScanParameters()
        {
            if (storage.ScanParameters != null)
            {
                return storage.ScanParameters;
            }

            var centerBearing = anglesCalculator.GetBearingToCoordinates(storage.Robot.BattleFieldWidth / 2, storage.Robot.BattleFieldHeight / 2);
            var diff = anglesCalculator.GetBearingDiff(centerBearing, storage.Robot.Heading);
            var clockwize = diff > 0;
            var target = storage.Robot.Heading.AddAngle(clockwize ? 179 : -179);

            var scan = storage.ScanParameters = new ScanParameters
                       {
                           Clockwize = clockwize,
                           Stage = 0,
                           TargetBearing = target
                       };
            return scan;
        }
    }
}
