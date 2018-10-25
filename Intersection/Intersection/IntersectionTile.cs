using System;
using TrafficIntersection;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{

    public class IntersectionTile: Tile
    {
        /// <summary>
        /// Constructor for a IntersectionTile object, sets the Direction to None
        /// </summary>
        public IntersectionTile() : base(Direction.None)
        {
        }
    }
}
