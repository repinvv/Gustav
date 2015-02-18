namespace Gustav
{
    using System;
    using Gustav.MainLogic;
    using Gustav.Properties;

    internal class Runner
    {
        private readonly RobotContainer container;

        public Runner(RobotContainer container)
        {
            this.container = container;
        }

        public void Run(Loyalist loyalist)
        {
            loyalist.GunColor = Settings.Default.GunColor;
            loyalist.RadarColor = Settings.Default.RadarColor;
            loyalist.BodyColor = Settings.Default.BodyColor;
            loyalist.BulletColor = Settings.Default.BulletColor;
            container.Robot = loyalist;
            var rate = RateDeterminationLogic.DetermineRates();
            while (loyalist.Energy > 0)
            {
                loyalist.TurnRateRadians = rate.BodyTurn;
                loyalist.GunRotationRateRadians = rate.TurretTurn;
                loyalist.RadarRotationRateRadians = rate.RadarTurn;
                if (Math.Abs(loyalist.GunHeat) < Settings.Default.Tolerance)
                {
                    loyalist.SetFire(1);
                }

                loyalist.Execute();
            }
        }
    }
}
