namespace Gustav.MainLogic.Engage
{
    using System;
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Properties;
    using Gustav.Storage;
    using Robocode;

    class TurretHeadingCalculator
    {
        private readonly CombatParametersStorage storage;
        private readonly AnglesCalculator anglesCalculator;
        private readonly EnemyRotationCalculator rotationCalculator;

        public TurretHeadingCalculator(CombatParametersStorage storage, 
            AnglesCalculator anglesCalculator,
            EnemyRotationCalculator rotationCalculator)
        {
            this.storage = storage;
            this.anglesCalculator = anglesCalculator;
            this.rotationCalculator = rotationCalculator;
        }

        public double GetCurrentTurnHeading(EnemyData enemy)
        {
            if (Double.IsNaN(storage.Engage.TargetHeading))
            {
                storage.Engage.TargetHeading = anglesCalculator.GetHeadingTo(enemy.Position);
            }

            return storage.Engage.TargetHeading;
        }

        public double GetNextTurnHeading(EnemyData enemy)
        {
            var robotPosition = anglesCalculator.GetCoordinatesByAngle(storage.Robot.Velocity, storage.Robot.Heading);
            var enemyPosition = anglesCalculator.GetCoordinatesByAngle(enemy.Velocity, enemy.Heading, enemy.Position);
            var diff = GetHeadingDiffToTarget(robotPosition, enemyPosition, enemy);
            var heading = anglesCalculator.GetHeading(enemyPosition, robotPosition);
            var spread = GetSpread(enemy.Velocity);
            return storage.Engage.TargetHeading = heading.AddAngle(diff * spread);
        }

        private double GetSpread(double velocity)
        {
            var random = storage.Engage.Random.NextDouble() +
                         storage.Engage.Random.NextDouble() +
                         storage.Engage.Random.NextDouble() +
                         storage.Engage.Random.NextDouble();
            return Math.Abs(velocity) < Rules.MAX_VELOCITY / 2 ? random / 2 : (1 - Math.Abs(random / 2 - 1));
        }

        private double GetHeadingDiffToTarget(DoublePoint robotPosition, DoublePoint enemyPosition, EnemyData enemy)
        {
            var heading = anglesCalculator.GetHeading(robotPosition, enemyPosition);
            if (Math.Abs(enemy.Velocity) < 1)
            {
                return 0;
            }

            var angleB = anglesCalculator.GetHeadingDiff(heading, enemy.Heading);
            var sinA = (enemy.Velocity * angleB.Sin()) / Rules.GetBulletSpeed(storage.Engage.BulletPower);
            var rotation = rotationCalculator.GetEnemyRotation(enemy);
            if (Math.Abs(rotation) > Settings.Default.ComparisionTolerance)
            {
                var angleA = sinA.Asin();
                var angleC = 180 - Math.Abs(angleA) - Math.Abs(angleB);
                var sinC = angleC.Sin();
                var distance = anglesCalculator.GetDistance(robotPosition, enemyPosition);
                var sideA = Math.Abs((distance * sinA) / sinC);
                var turnsToHit = sideA / enemy.Velocity;
                var projectedHeading = enemy.Heading.AddAngle(rotation * turnsToHit / 2);
                var projectedVelocity = Math.Abs(enemy.Velocity) > (Rules.MAX_VELOCITY / 2) ? enemy.Velocity * Settings.Default.TargetSpeedAdjust : enemy.Velocity;
                var newAngleB = anglesCalculator.GetHeadingDiff(heading, projectedHeading);
                sinA = (projectedVelocity * newAngleB.Sin()) / Rules.GetBulletSpeed(storage.Engage.BulletPower);
            }

            return sinA.Asin();
        }
    }
}
