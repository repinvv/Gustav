namespace Gustav.MainLogic.Movement
{
    using System;
    using System.Collections.Generic;
    using Gustav.MathServices;

    public class MovementParameters
    {
        public MovementParameters()
        {
            Random = new Random();
        }

        public DoublePoint Destination { get; set; }

        public Random Random { get; set; }

        public int Threshold { get; set; }
    }
}
