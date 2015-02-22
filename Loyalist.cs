namespace Gustav
{
    using Gustav.Position;
    using Gustav.Storage;
    using Robocode;

    public class Loyalist : RateControlRobot
    {
        private PositionRegister positionRegister;
        private CombatParametersStorage combatParametersStorage;

        public override void Run()
        {
            var resolver = new Resolver();
            positionRegister = resolver.Get<PositionRegister>();
            combatParametersStorage = resolver.Get<CombatParametersStorage>();
            resolver.Get<Runner>().Run(this);
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            positionRegister.OnScan(e);
        }

        public override void OnBattleEnded(BattleEndedEvent evnt)
        {
            combatParametersStorage.CombatEnded = true;
        }

        public override void OnRoundEnded(RoundEndedEvent evnt)
        {
            combatParametersStorage.CombatEnded = true;
        }

        public override void OnHitRobot(HitRobotEvent evnt)
        {
            positionRegister.OnCollistion();
        }
    }
}
