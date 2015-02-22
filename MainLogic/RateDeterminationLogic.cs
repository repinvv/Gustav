namespace Gustav.MainLogic
{
    using System;
    using Gustav.MainLogic.Engage;
    using Gustav.Storage;

    internal class RateDeterminationLogic
    {
        private readonly ScanLogic scanLogic;
        private readonly CombatParametersStorage storage;
        private readonly EngageLogic engageLogic;
        private readonly ModeSelector modeSelector;
        private readonly SearchLogic searchLogic;

        public RateDeterminationLogic(ScanLogic scanLogic, CombatParametersStorage storage, EngageLogic engageLogic, ModeSelector modeSelector, SearchLogic searchLogic)
        {
            this.scanLogic = scanLogic;
            this.storage = storage;
            this.engageLogic = engageLogic;
            this.modeSelector = modeSelector;
            this.searchLogic = searchLogic;
        }

        public Rates DetermineRates()
        {
            Rates rates = null;
            while (rates == null)
            {
                rates = GetRates();
            }

            return rates;
        }

        private Rates GetRates()
        {
            switch (storage.CombatMode)
            {
                case CombatMode.Scan:
                    return scanLogic.DetermineRates();
                case CombatMode.Engage:
                    return engageLogic.DetermineRates();
                case CombatMode.Search:
                    return searchLogic.DetermineRates();
                default:
                    throw new Exception("wtf?");
            }
        }
    }
}
