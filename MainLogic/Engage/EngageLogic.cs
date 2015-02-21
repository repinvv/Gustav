namespace Gustav.MainLogic.Engage
{
    using Gustav.Storage;

    class EngageLogic
    {
        private readonly MovementLogic movement;
        private readonly TargettingLogic targettingLogic;
        private readonly ScanHoldLogic scanHold;
        private readonly CurrentEnemySelection enemySelection;
        private readonly EnemyDataStorage enemyDataStorage;
        private readonly CombatParametersStorage storage;

        public EngageLogic(MovementLogic movement,
            TargettingLogic targettingLogic,
            ScanHoldLogic scanHold, 
            CurrentEnemySelection enemySelection, 
            EnemyDataStorage enemyDataStorage,
            CombatParametersStorage storage)
        {
            this.movement = movement;
            this.targettingLogic = targettingLogic;
            this.scanHold = scanHold;
            this.enemySelection = enemySelection;
            this.enemyDataStorage = enemyDataStorage;
            this.storage = storage;
        }

        public Rates DetermineRates()
        {
            var targetting = enemySelection.GetTargettingParameters();
            var enemyData = enemyDataStorage.GetEnemy(targetting.CurrentEnemy);
            if (enemyData == null)
            {
                storage.CombatMode = CombatMode.Scan;
                return null;
            }

            var rates = new Rates();
            movement.DetermineMovementRates(rates, enemyData);
            targettingLogic.DetermineTargettingRates(rates, enemyData);
            scanHold.DetermineScanRates(rates, enemyData);
            return rates;
        }
    }
}
