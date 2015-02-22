namespace Gustav.MainLogic.Engage
{
    using System;
    using System.Runtime.InteropServices;
    using Gustav.MathServices;
    using Gustav.Position;
    using Gustav.Storage;
    using Robocode;

    class TurretHeadingCalculator
    {
        private readonly CombatParametersStorage storage;
        private readonly AnglesCalculator anglesCalculator;
        private readonly EnemyDataStorage enemyDataStorage;

        public TurretHeadingCalculator(CombatParametersStorage storage, AnglesCalculator anglesCalculator, EnemyDataStorage enemyDataStorage)
        {
            this.storage = storage;
            this.anglesCalculator = anglesCalculator;
            this.enemyDataStorage = enemyDataStorage;
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
            var angleB = anglesCalculator.GetHeadingDiff(storage.Robot.GunHeading.AddAngle(180), enemy.Heading);
            var sinA = (enemy.Velocity * angleB.Sin()) / Rules.GetBulletSpeed(storage.Engage.BulletPower);
            var angleA = sinA.Asin();
            var angleC = 180 - Math.Abs(angleA) - Math.Abs(angleB);
            var sinC = angleC.Sin();
            var distance = anglesCalculator.GetDistance(robotPosition, enemyPosition);
            var sideA = (distance * sinA) / sinC;
            var turnsToHit = sideA / enemy.Velocity;
            var rotation = anglesCalculator.GetHeadingDiff(enemy.Heading, enemyDataStorage.GetPrevious(enemy.Name).Heading) * turnsToHit / 2;
            var projectedHeading = enemy.Heading.AddAngle(rotation);
            var projectedVelocity = enemy.Velocity > (Rules.MAX_VELOCITY * 2) ? enemy.Velocity * 0.85 : enemy.Velocity;
            var newAngleB = anglesCalculator.GetHeadingDiff(storage.Robot.GunHeading.AddAngle(180), projectedHeading);
            var newsinA = (projectedVelocity * newAngleB.Sin()) / Rules.GetBulletSpeed(storage.Engage.BulletPower);
            return storage.Robot.GunHeading + newsinA.Asin();
        }
    }
}
