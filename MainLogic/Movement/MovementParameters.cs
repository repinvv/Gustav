namespace Gustav.MainLogic.Movement
{
    using System;
    using System.Collections.Generic;
    using Gustav.MathServices;

    class MovementParameters
    {
        public MovementParameters()
        {
            Random = new Random();
        }

        public Queue<DoublePoint> Path { get; set; }

        public Random Random { get; set; }

        public int Threshold { get; set; }
    }
}
