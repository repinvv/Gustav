﻿namespace Gustav.Storage
{
    using Gustav.MainLogic;

    class ModeSelector
    {
        private readonly CombatParametersStorage storage;

        public ModeSelector(CombatParametersStorage storage)
        {
            this.storage = storage;
        }

        public void SelectMode(CombatMode mode)
        {
            storage.CombatMode = mode;
            switch (mode)
            {
                case CombatMode.Scan:
                    storage.Scan = null;
                    storage.Movement = null;
                    break;
                case CombatMode.Engage:
                    storage.Engage = null;
                    break;
                case CombatMode.Search:
                    break;
            }
        }
    }
}
