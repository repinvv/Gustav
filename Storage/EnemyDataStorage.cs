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
        readonly Dictionary<string, EnemyData> previousData = new Dictionary<string, EnemyData>();

        public bool Collision { get; set; }

        public EnemyDataStorage(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public void StoreEnemy(EnemyData enemyData)
        {
            lock (sync)
            {
                EnemyData previous;
                if (enemies.TryGetValue(enemyData.Name, out previous))
                {
                    previousData[enemyData.Name] = previous;
                }

                enemies[enemyData.Name] = enemyData;
            }
        }

        public EnemyData GetEnemy(string name)
        {
            lock (sync)
            {
                var enemy = enemies[name];
                return NotExpired(enemy) ? enemy : null;
            }
        }

        public bool HaveActiveEnemy()
        {
            lock (sync)
            {
                return enemies.Values.Any(NotExpired);
            }
        }

        public List<EnemyData> GetAvailableEnemies()
        {
            lock (sync)
            {
                return enemies.Values.Where(NotExpired).ToList();
            }
        }

        private bool NotExpired(EnemyData data)
        {
            lock (sync)
            {
                return storage.Robot.Time - data.LastSeen < Settings.Default.ScanExpiration;
            }
        }

        public void Clear()
        {
            enemies.Clear();
            previousData.Clear();
        }

        public EnemyData GetPrevious(string name)
        {
            lock (sync)
            {
                EnemyData enemy;
                return previousData.TryGetValue(name, out enemy) ? enemy : GetEnemy(name);
            }
        }
    }
}
