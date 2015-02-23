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

        public string Collision { get; set; }

        public EnemyDataStorage(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public void StoreEnemy(EnemyData enemy)
        {
            lock (sync)
            {
                EnemyData previous;
                if (enemies.TryGetValue(enemy.Name, out previous))
                {
                    previousData[enemy.Name] = previous;
                    enemy.Dead = previous.Dead;
                }

                enemies[enemy.Name] = enemy;
            }
        }

        public EnemyData GetEnemy(string name)
        {
            lock (sync)
            {
                var enemy = enemies[name];
                return Valid(enemy) ? enemy : null;
            }
        }

        public bool HaveActiveEnemy()
        {
            lock (sync)
            {
                return enemies.Values.Any(Valid);
            }
        }

        public List<EnemyData> GetAvailableEnemies()
        {
            lock (sync)
            {
                return enemies.Values.Where(Valid).ToList();
            }
        }

        private bool Valid(EnemyData enemy)
        {
            lock (sync)
            {
                return !storage.Robot.Teammates.Contains(enemy.Name) 
                    && !enemy.Dead 
                    && storage.Robot.Time - enemy.LastSeen < Settings.Default.ScanExpiration;
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

        public void RemoveEnemy(string name)
        {
            lock (sync)
            {
                EnemyData enemy;
                if (enemies.TryGetValue(name, out enemy))
                {
                    enemy.Dead = true;
                }
            }
        }
    }
}
