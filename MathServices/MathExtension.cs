namespace Gustav.MathServices
{
    using System;

    internal static class MathExtension
    {
        public static double ToRadians(this double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static double ToRadians(this int angle)
        {
            return ((double)angle).ToRadians();
        }
    }
}
