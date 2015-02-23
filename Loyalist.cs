namespace Gustav
{
    using Gustav.Position;
    using Gustav.Storage;
    using Robocode;

    public class Loyalist : RateControlRobot
    {
        private EventRegister eventRegister;
        private CombatParametersStorage combatParametersStorage;

        public override void Run()
        {
            var resolver = new Resolver();
            eventRegister = resolver.Get<EventRegister>();
            combatParametersStorage = resolver.Get<CombatParametersStorage>();
            resolver.Get<Runner>().Run(this);
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            eventRegister.OnScan(e);
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
            eventRegister.OnCollision(evnt);
        }

        public override void OnRobotDeath(RobotDeathEvent evnt)
        {
            eventRegister.OnDeath(evnt);
        }
    }
}
