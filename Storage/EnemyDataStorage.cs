namespace Gustav.Storage
{
    using System.Collections.Generic;
    using Gustav.Position;

    internal class EnemyDataStorage
    {
        object sync = new object();
        readonly Dictionary<string, EnemyData> enemies = new Dictionary<string, EnemyData>();

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
    }
}
