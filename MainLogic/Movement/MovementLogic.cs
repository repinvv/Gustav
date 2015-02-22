namespace Gustav.MainLogic.Movement
{
    using System.Collections.Generic;
    using System.Linq;
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Storage;

    class MovementLogic
    {
        private readonly MovementAssign movementAssign;
        private readonly CombatParametersStorage storage;
        private readonly PathFollowing pathFollowing;
        private readonly EnemyDataStorage enemyDataStorage;

        public MovementLogic(MovementAssign movementAssign, CombatParametersStorage storage, PathFollowing pathFollowing, EnemyDataStorage enemyDataStorage)
        {
            this.movementAssign = movementAssign;
            this.storage = storage;
            this.pathFollowing = pathFollowing;
            this.enemyDataStorage = enemyDataStorage;
        }

        public void DetermineMovementRates(Rates rates, EnemyData enemy)
        {
            storage.Movement = storage.Movement ?? new MovementParameters
                                                   {
                                                       Path = new Queue<DoublePoint>()
                                                   };
            if (enemyDataStorage.Collision && storage.Movement.Path.Any())
            {
                storage.Movement.Path.Dequeue();
            }

            enemyDataStorage.Collision = false;
            movementAssign.AssignDestination(enemy);
            pathFollowing.FollowPath(rates);
        }
    }
}
