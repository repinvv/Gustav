namespace Gustav.Storage
{
    using Gustav.MainLogic;

    class CombatParametersStorage
    {
        public Loyalist Robot { get; set; }

        public CombatMode CombatMode { get; set; }

        public ScanParameters ScanParameters { get; set; }
    }
}
