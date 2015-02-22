namespace Gustav.MainLogic
{
    using System;
    using Gustav.MainLogic.Engage;
    using Gustav.Position;
    using Gustav.Properties;

    class MaxFireDistanceSelector
    {
        private readonly EnemyRotationCalculator rotationCalculator;

        public MaxFireDistanceSelector(EnemyRotationCalculator rotationCalculator)
        {
            this.rotationCalculator = rotationCalculator;
        }

        public double GetMaxFireDistance(EnemyData enemy)
        {
            var maxfire = Settings.Default.MaxFireDistance;
            if (Math.Abs(enemy.Velocity) > 2)
            {
                maxfire -= 100;
                if (Math.Abs(rotationCalculator.GetEnemyRotation(enemy)) > 1)
                {
                    maxfire -= 100;
                }
            }

            return maxfire;
        }
    }
}
