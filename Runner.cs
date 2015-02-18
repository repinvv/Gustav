namespace Gustav
{
    using System;
    using Gustav.MainLogic;
    using Gustav.Properties;
    using Gustav.Storage;

    internal class Runner
    {
        private readonly CombatParametersStorage storage;

        public Runner(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public void Run(Loyalist loyalist)
        {
            Init(loyalist);
            while (loyalist.Energy > 0)
            {
                var rate = RateDeterminationLogic.DetermineRates();
                loyalist.TurnRateRadians = rate.BodyTurn;
                loyalist.GunRotationRateRadians = rate.TurretTurn;
                loyalist.RadarRotationRateRadians = rate.RadarTurn;
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
            storage.Combat = new CombatParameters
                             {
                                 Mode = CombatMode.Scan
                             };
        }
    }
}
