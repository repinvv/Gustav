namespace Gustav.MainLogic.Engage
{
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Storage;

    class EnemyRotationCalculator
    {
        private readonly AnglesCalculator anglesCalculator;
        private readonly EnemyDataStorage enemyDataStorage;

        public EnemyRotationCalculator(AnglesCalculator anglesCalculator, EnemyDataStorage enemyDataStorage)
        {
            this.anglesCalculator = anglesCalculator;
            this.enemyDataStorage = enemyDataStorage;
        }

        public double GetEnemyRotation(EnemyData enemy)
        {
            return anglesCalculator.GetHeadingDiff(enemy.Heading, enemyDataStorage.GetPrevious(enemy.Name).Heading);
        }
    }
}
