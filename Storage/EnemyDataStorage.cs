namespace Gustav.Storage
{
    using System.Collections.Generic;
    using System.Linq;
    using Gustav.Position;
    using Gustav.Properties;

    internal class EnemyDataStorage
    {
        private readonly CombatParametersStorage storage;
        readonly object sync = new object();
        readonly Dictionary<string, EnemyData> enemies = new Dictionary<string, EnemyData>();

        public EnemyDataStorage(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public void StoreEnemy(EnemyData enemyData)
        {
            lock (sync)
            {
                enemies[enemyData.Name] = enemyData;
            }
        }

        public EnemyData GetEnemy(string name)
        {
            lock (sync)
            {
                return enemies[name];
            }
        }

        public bool HaveActiveEnemy()
        {
            lock (sync)
            {
                return enemies.Values.Any(x => storage.Robot.Time - x.LastSeen < Settings.Default.ScanExpiration);
            }
        }
    }
}
