namespace Gustav.Storage
{
    using Gustav.MainLogic;
    using Gustav.MainLogic.Engage;

    class CombatParametersStorage
    {
        public Loyalist Robot { get; set; }

        public CombatMode CombatMode { get; set; }

        public ScanParameters ScanParameters { get; set; }

        public EngageParameters  Engage { get; set; }
    }
}
