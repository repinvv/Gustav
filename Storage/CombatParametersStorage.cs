namespace Gustav.Storage
{
    using Gustav.MainLogic;
    using Gustav.MainLogic.Engage;
    using Gustav.MainLogic.Movement;

    public class CombatParametersStorage
    {
        public Loyalist Robot { get; set; }

        public CombatMode CombatMode { get; set; }

        public ScanParameters Scan { get; set; }

        public EngageParameters  Engage { get; set; }

        public MovementParameters Movement { get; set; }

        public bool CombatEnded { get; set; }
    }
}
