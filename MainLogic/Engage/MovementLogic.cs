namespace Gustav.MainLogic.Engage
{
    using Gustav.Position;
    using Gustav.Storage;

    class MovementLogic
    {
        private readonly CurrentEnemySelection enemySelection;
        private readonly EnemyDataStorage enemyDataStorage;

        public MovementLogic(CurrentEnemySelection enemySelection, EnemyDataStorage enemyDataStorage, CombatParametersStorage storage)
        {
            this.enemySelection = enemySelection;
            this.enemyDataStorage = enemyDataStorage;
        }

        public void DetermineMovementRates(Rates rates, EnemyData enemyData)
        {
            rates.BodyTurn = -3;
            rates.Velocity = 8;
        }
    }
}
