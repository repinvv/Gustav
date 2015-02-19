namespace Gustav.MainLogic
{
    using System;
    using Gustav.Storage;

    internal class RateDeterminationLogic
    {
        private readonly ScanLogic scanLogic;
        private readonly CombatParametersStorage storage;

        public RateDeterminationLogic(ScanLogic scanLogic, CombatParametersStorage storage)
        {
            this.scanLogic = scanLogic;
            this.storage = storage;
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
                    storage.Robot.Out.WriteLine("Scan mode");
                    return scanLogic.DetermineRates();
                case CombatMode.Engage:
                    storage.Robot.Out.WriteLine("Engage mode");
                    return null;
                case CombatMode.Search:
                    storage.Robot.Out.WriteLine("Search mode");
                    return null;
                default:
                    throw new Exception("wtf?");
            }
        }
    }
}
