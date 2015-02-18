namespace Gustav
{
    using Gustav.RobotPosition;
    using Robocode;

    public class Loyalist : RateControlRobot
    {
        public override void Run()
        {
            Runner.Run(this);
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            PositionRegister.OnScan(e);
        }
    }
}
