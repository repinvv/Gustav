namespace Gustav.Position
{
    using Gustav.MathServices;

    class EnemyData
    {
        public string Name { get; set; }

        public DoublePoint Position { get; set; }

        public double Distance { get; set; }

        public double Heading { get; set; }

        public double Velocity { get; set; }

        public double Energy { get; set; }

        public long LastSeen { get; set; }

        public bool Dead { get; set; }
    }
}
