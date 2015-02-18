namespace Gustav
{
    using Gustav.RobotPosition;
    using Robocode;

    public class Loyalist : RateControlRobot
    {
        public override void Run()
        {
            new Resolver().Get<Runner>().Run(this);
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            PositionRegister.OnScan(e);
        }
    }
}
