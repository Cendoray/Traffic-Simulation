using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{
    public class Road : Tile
    {
        /// <summary>
        /// Constructor for a Road object, sets the Direction to None
        /// </summary>
        /// <param name="d">Direction that indicates the orienatation of the tile</param>
        public Road(Direction d) : base(d)
        {
        }
    }
}
