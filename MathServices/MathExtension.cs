namespace Gustav.MathServices
{
    using System;
    using System.Linq;
    using Gustav.Properties;

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

        public static double AddAngle(this double angle, double add)
        {
            return (angle + add) % 360;
        }
        public static double NormalizeAngle(this double angle)
        {
            return (angle + 360) % 360;
        }

        public static double Sin(this double angle)
        {
            return Math.Sin(angle.ToRadians());
        }

        public static double Cos(this double angle)
        {
            return Math.Cos(angle.ToRadians());
        }

        public static double Asin(this double sin)
        {
            return Math.Asin(sin).ToDegrees();
        }

        public static double GaussDouble(this Random random)
        {
            double a = 0;
            for (int i = 0; i < Settings.Default.GaussNumber; i++)
            {
                a = a + random.NextDouble();
            }

            return a / Settings.Default.GaussNumber;
        }
    }
}
