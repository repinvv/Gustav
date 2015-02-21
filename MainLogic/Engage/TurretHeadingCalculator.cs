namespace Gustav.MainLogic.Engage
{
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Storage;

    class TurretHeadingCalculator
    {
        private readonly CombatParametersStorage storage;
        private readonly AnglesCalculator anglesCalculator;

        public TurretHeadingCalculator(CombatParametersStorage storage, AnglesCalculator anglesCalculator)
        {
            this.storage = storage;
            this.anglesCalculator = anglesCalculator;
        }

        public double GetCurrentTurnHeading(EnemyData enemy)
        {
            return anglesCalculator.GetBearingToCoordinates(enemy.Position.X, enemy.Position.Y);
        }

        public double GetNextTurnHeading(EnemyData enemy)
        {
            return anglesCalculator.GetBearingToCoordinates(enemy.Position.X, enemy.Position.Y);
        }

    }
}
