namespace Gustav
{
    using Gustav.MainLogic;
    using Gustav.Properties;
    using Gustav.Storage;

    internal class Runner
    {
        private readonly CombatParametersStorage storage;
        private readonly RateDeterminationLogic rateDeterminationLogic;
        private readonly EnemyDataStorage enemyDataStorage;
        private readonly ModeSelector modeSelector;

        public Runner(CombatParametersStorage storage, RateDeterminationLogic rateDeterminationLogic, EnemyDataStorage enemyDataStorage, ModeSelector modeSelector)
        {
            this.storage = storage;
            this.rateDeterminationLogic = rateDeterminationLogic;
            this.enemyDataStorage = enemyDataStorage;
            this.modeSelector = modeSelector;
        }

        public void Run(Loyalist loyalist)
        {
            Init(loyalist);
            while (loyalist.Energy > 0)
            {
                var rate = rateDeterminationLogic.DetermineRates();
                loyalist.VelocityRate = rate.Velocity;
                loyalist.TurnRate = rate.BodyTurn;
                loyalist.GunRotationRate = rate.TurretTurn;
                loyalist.RadarRotationRate = rate.RadarTurn;
                if (rate.BulletPower > 0)
                {
                    loyalist.SetFire(rate.BulletPower);
                }

                if (storage.CombatEnded)
                {
                    break;
                }

                loyalist.Execute();
            }
        }

        private void Init(Loyalist loyalist)
        {
            loyalist.GunColor = Settings.Default.GunColor;
            loyalist.RadarColor = Settings.Default.RadarColor;
            loyalist.BodyColor = Settings.Default.BodyColor;
            loyalist.BulletColor = Settings.Default.BulletColor;
            storage.Robot = loyalist;
            enemyDataStorage.Clear();
            modeSelector.SelectMode(CombatMode.Scan);
        }
    }
}
