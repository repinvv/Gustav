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
            var robotPosition = new DoublePoint(storage.Robot.X, storage.Robot.Y);
            var enemyPosition = enemy.Position;
            return GetHeadingToTarget(robotPosition, enemyPosition, enemy);
        }

        public double GetNextTurnHeading(EnemyData enemy)
        {
            var robotPosition = anglesCalculator.GetCoordinatesByAngle(storage.Robot.Velocity, storage.Robot.Heading);
            var enemyPosition = anglesCalculator.GetCoordinatesByAngle(enemy.Velocity, enemy.Heading, enemy.Position);
            return GetHeadingToTarget(robotPosition, enemyPosition, enemy);
        }

        private double GetHeadingToTarget(DoublePoint robotPosition, DoublePoint enemyPosition, EnemyData enemy)
        {
            var heading = anglesCalculator.GetHeading(robotPosition, enemyPosition);
            if (Math.Abs(enemy.Velocity) < 1)
            {
                return heading.AddAngle(180);
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
                var projectedVelocity = enemy.Velocity > (Rules.MAX_VELOCITY * 2) ? enemy.Velocity * Settings.Default.TargetSpeedAdjust : enemy.Velocity;
                var newAngleB = anglesCalculator.GetHeadingDiff(heading, projectedHeading);
                sinA = (projectedVelocity * newAngleB.Sin()) / Rules.GetBulletSpeed(storage.Engage.BulletPower);
            }

            heading = heading.AddAngle(sinA.Asin());
            return heading.AddAngle(180);
        }
    }
}
