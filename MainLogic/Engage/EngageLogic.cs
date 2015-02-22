namespace Gustav.MainLogic.Engage
{
    using System;
    using Gustav.Storage;

    class EngageLogic
    {
        private readonly MovementLogic movement;
        private readonly TargettingLogic targettingLogic;
        private readonly ScanHoldLogic scanHold;
        private readonly CurrentEnemySelection enemySelection;
        private readonly EnemyDataStorage enemyDataStorage;
        private readonly CombatParametersStorage storage;
        private readonly BulletPowerCalculator bulletPowerCalculator;
        private readonly ModeSelector modeSelector;

        public EngageLogic(MovementLogic movement,
            TargettingLogic targettingLogic,
            ScanHoldLogic scanHold, 
            CurrentEnemySelection enemySelection, 
            EnemyDataStorage enemyDataStorage,
            CombatParametersStorage storage,
            BulletPowerCalculator bulletPowerCalculator, 
            ModeSelector modeSelector)
        {
            this.movement = movement;
            this.targettingLogic = targettingLogic;
            this.scanHold = scanHold;
            this.enemySelection = enemySelection;
            this.enemyDataStorage = enemyDataStorage;
            this.storage = storage;
            this.bulletPowerCalculator = bulletPowerCalculator;
            this.modeSelector = modeSelector;
        }

        public Rates DetermineRates()
        {
            var targetting = enemySelection.GetTargettingParameters();
            var enemy = enemyDataStorage.GetEnemy(targetting.CurrentEnemy);
            if (enemy == null)
            {
                modeSelector.SelectMode(CombatMode.Scan);
                return null;
            }

            bulletPowerCalculator.CalculateBulletPower(enemy);

            var rates = new Rates();
            movement.DetermineMovementRates(rates, enemy);
            targettingLogic.DetermineTargettingRates(rates, enemy);
            scanHold.DetermineScanRates(rates, enemy);
            return rates;
        }
    }
}
