namespace Gustav.MainLogic.Engage
{
    using System;

    public class EngageParameters
    {
        public EngageParameters()
        {
            TargetHeading = Double.NaN;
            Random = new Random();
        }

        public string CurrentEnemy { get; set; }

        public double BulletPower { get; set; }

        public double TargetHeading { get; set; }

        public Random Random { get; set; }

        public long LastFired { get; set; }
    }
}
