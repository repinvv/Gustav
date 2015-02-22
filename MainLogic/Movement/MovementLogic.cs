namespace Gustav.MainLogic.Movement
{
    using System.Collections.Generic;
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Storage;

    class MovementLogic
    {
        private readonly MovementAssign movementAssign;
        private readonly CombatParametersStorage storage;
        private readonly PathFollowing pathFollowing;

        public MovementLogic(MovementAssign movementAssign, CombatParametersStorage storage, PathFollowing pathFollowing)
        {
            this.movementAssign = movementAssign;
            this.storage = storage;
            this.pathFollowing = pathFollowing;
        }

        public void DetermineMovementRates(Rates rates, EnemyData enemy)
        {
            storage.Movement = storage.Movement ?? new MovementParameters
                                                   {
                                                       Path = new Queue<DoublePoint>()
                                                   };
            movementAssign.AssignDestination(enemy);
            pathFollowing.FollowPath(rates);
        }
    }
}
