using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{
    public class Car : Vehicle
    {
        /// <summary>
        /// Constructor used to create car
        /// </summary>
        /// <param name="grid">the grid where the car is on</param>
        public Car(Grid grid) : base(5, 2, 3, grid) { }
    }
}
