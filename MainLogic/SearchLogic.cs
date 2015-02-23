namespace Gustav.MainLogic
{
    using System.Collections.Generic;
    using Gustav.MainLogic.Movement;
    using Gustav.MathServices;
    using Gustav.Storage;
    using Robocode;

    internal class SearchLogic
    {
        private readonly CombatParametersStorage storage;
        private readonly RandomMovementAssign randomMovementAssign;
        private readonly PathFollowing pathFollowing;
        private readonly EnemyDataStorage enemyDataStorage;
        private readonly ModeSelector modeSelector;

        public SearchLogic(CombatParametersStorage storage,
            RandomMovementAssign randomMovementAssign,
            PathFollowing pathFollowing,
            EnemyDataStorage enemyDataStorage,
            ModeSelector modeSelector)
        {
            this.storage = storage;
            this.randomMovementAssign = randomMovementAssign;
            this.pathFollowing = pathFollowing;
            this.enemyDataStorage = enemyDataStorage;
            this.modeSelector = modeSelector;
        }

        public Rates DetermineRates()
        {
            if (enemyDataStorage.HaveActiveEnemy())
            {
                modeSelector.SelectMode(CombatMode.Engage);
                return null;
            }

            storage.Movement = storage.Movement ?? new MovementParameters();

            var rates = new Rates { RadarTurn = Rules.RADAR_TURN_RATE };
            randomMovementAssign.AssignDestination();
            pathFollowing.FollowPath(rates);
            return rates;
        }
    }
}
