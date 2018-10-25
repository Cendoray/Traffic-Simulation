using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{
    public abstract class Tile
    {

        /// <summary>
        /// Constructor for a Tile object, sets the direction of the tile
        /// </summary>
        /// <param name="d">Direction that indicates the orienatation of the tile</param>
        public Tile(Direction d)
        {

            this.Direction = d;
        }

        /// <summary>
        /// Automatic property for Direction, can get the value or set it
        /// </summary>
        public Direction Direction
        {
            get; private set;
        }

        /// <summary>
        /// Automatic property for Occupied, can get the value or set it
        /// </summary>
        public bool Occupied
        {
            get; set;
        }
    }
}
