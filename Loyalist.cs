namespace Gustav
{
    using System.Runtime.InteropServices;
    using Gustav.Position;
    using Robocode;

    public class Loyalist : RateControlRobot
    {
        private PositionRegister positionRegister;

        public override void Run()
        {
            var resolver = new Resolver();
            positionRegister = resolver.Get<PositionRegister>();
            resolver.Get<Runner>().Run(this);
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            positionRegister.OnScan(e);
        }
    }
}
