﻿namespace Gustav.MainLogic.Engage
{
    using System;
    using Gustav.Position;
    using Gustav.Properties;
    using Gustav.Storage;

    class BulletPowerCalculator
    {
        private readonly CombatParametersStorage storage;
        private readonly EnemyRotationCalculator rotationCalculator;

        public BulletPowerCalculator(CombatParametersStorage storage, EnemyRotationCalculator rotationCalculator)
        {
            this.storage = storage;
            this.rotationCalculator = rotationCalculator;
        }

        public void CalculateBulletPower(EnemyData enemy)
        {
            var distanceReduction = (enemy.Distance - Settings.Default.MaxPowerDistance)/Settings.Default.DistanceRate;
            var rotationReduction = rotationCalculator.GetEnemyRotation(enemy) / Settings.Default.RotationRate;
            var power = Settings.Default.MaxPower - distanceReduction - rotationReduction;
            power = Math.Max(power, Settings.Default.MinPower);
            storage.Engage.BulletPower = power;
        }
    }
}
