using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{
    public class Motorcycle : Vehicle
    {
        /// <summary>
        /// Constructor used to create motorcycle
        /// </summary>
        /// <param name="grid">the grid where the motorcycle is on</param>
        public Motorcycle(Grid grid) : base(2,1,1, grid) { }
    }
}
