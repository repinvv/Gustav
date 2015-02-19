namespace Gustav.MainLogic
{
    using System;
    using Gustav.MathServices;
    using Gustav.Properties;
    using Gustav.Storage;

    class ScanLogic
    {
        private readonly CombatParametersStorage storage;
        private readonly CoordinatesCalculator coordinatesCalculator;
        private readonly EnemyDataStorage enemyDataStorage;

        public ScanLogic(CombatParametersStorage storage, CoordinatesCalculator coordinatesCalculator, EnemyDataStorage enemyDataStorage)
        {
            this.storage = storage;
            this.coordinatesCalculator = coordinatesCalculator;
            this.enemyDataStorage = enemyDataStorage;
        }

        public Rates DetermineRates()
        {
            var scan = storage.ScanParameters ?? GetScanParameters();
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

                scan.TargetBearing = (scan.TargetBearing + (scan.Clockwize ? 179 : -179)) % 360;
            }

            return new Rates
                   {
                       BodyTurn = direction * Settings.Default.MaxTurn,
                       TurretTurn = direction * Settings.Default.MaxTurretTurn,
                       RadarTurn = direction * Settings.Default.MaxRadarTurn,
                   };
        }

        private bool ScanEnded(ScanParameters scan)
        {
            var direction = scan.Clockwize ? 1 : -1;
            var diff = direction * coordinatesCalculator.GetBearingDiff(scan.TargetBearing, storage.Robot.RadarHeading);
            return diff >= 0;
        }

        private ScanParameters GetScanParameters()
        {
            var centerBearing = coordinatesCalculator.GetBearingToCoordinates(storage.Robot.BattleFieldWidth / 2, storage.Robot.BattleFieldHeight / 2);
            var diff = coordinatesCalculator.GetBearingDiff(centerBearing, storage.Robot.Heading);
            var clockwize = diff > 0;
            var target = (storage.Robot.Heading + (clockwize ? 179 : -179)) % 360;

            var scan = new ScanParameters
                       {
                           Clockwize = clockwize,
                           Stage = 0,
                           TargetBearing = target
                       };
            return scan;
        }
    }
}
