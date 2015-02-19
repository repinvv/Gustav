namespace Gustav
{
    using Gustav.MainLogic;
    using Gustav.Properties;
    using Gustav.Storage;

    internal class Runner
    {
        private readonly CombatParametersStorage storage;
        private readonly RateDeterminationLogic rateDeterminationLogic;

        public Runner(CombatParametersStorage storage, RateDeterminationLogic rateDeterminationLogic)
        {
            this.storage = storage;
            this.rateDeterminationLogic = rateDeterminationLogic;
        }

        public void Run(Loyalist loyalist)
        {
            Init(loyalist);
            while (loyalist.Energy > 0)
            {
                var rate = rateDeterminationLogic.DetermineRates();
                loyalist.TurnRate = rate.BodyTurn;
                loyalist.GunRotationRate = rate.TurretTurn;
                loyalist.RadarRotationRate = rate.RadarTurn;
                if (rate.FirePower > 0) // && Math.Abs(loyalist.GunHeat) < Settings.Default.Tolerance)
                {
                    loyalist.SetFire(rate.FirePower);
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
            storage.CombatMode = CombatMode.Scan;
        }
    }
}
