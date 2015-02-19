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

        public static double ToDegrees(this double angle)
        {
            return 180.0 * angle / Math.PI;
        }
    }
}
