namespace Gustav.MainLogic.Engage
{
    using System.Linq;
    using Gustav.Position;
    using Gustav.Storage;

    class CurrentEnemySelection
    {
        private readonly CombatParametersStorage storage;
        private readonly EnemyDataStorage enemyDataStorage;

        public CurrentEnemySelection(CombatParametersStorage storage, EnemyDataStorage enemyDataStorage)
        {
            this.storage = storage;
            this.enemyDataStorage = enemyDataStorage;
        }

        public EngageParameters GetTargettingParameters()
        {
            var targetting = storage.Engage;
            if (targetting == null)
            {
                targetting = storage.Engage = new EngageParameters();
                var enemy = GetPreferrableEnemy();
                if (enemy != null)
                {
                    targetting.CurrentEnemy = enemy.Name;
                }

                return targetting;
            }

            var currentEnemy = enemyDataStorage.GetEnemy(storage.Engage.CurrentEnemy);
            var preferrable = GetPreferrableEnemy();

            if(currentEnemy == null || 
                (currentEnemy != preferrable && GetTargetPriority(currentEnemy) > 2*GetTargetPriority(preferrable)))
            {
                currentEnemy = preferrable;
            }

            if (currentEnemy != null)
            {
                targetting.CurrentEnemy = currentEnemy.Name;
            }

            return targetting;
        }

        private EnemyData GetPreferrableEnemy()
        {
            return enemyDataStorage.GetAvailableEnemies().OrderBy(GetTargetPriority).First();
        }

        private double GetTargetPriority(EnemyData enemyData)
        {
            return enemyData.Energy * (enemyData.Distance + 50);
        }
    }
}
