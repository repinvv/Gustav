namespace Gustav.MainLogic
{
    using Gustav.MathServices;

    internal static class RateDeterminationLogic
    {
        public static Rates DetermineRates()
        {
            var turn = 10.ToRadians();
            return new Rates
                   {
                       BodyTurn = turn,
                       TurretTurn = -turn,
                       RadarTurn = 45.ToRadians(),
                       FirePower = 0
                   };
        }
    }
}
