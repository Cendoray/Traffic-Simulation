using System;

namespace TrafficIntersection
{
    public class Light : Tile
    {
        private ISignalStrategy strategy;
        /// <summary>
        /// Constructor for a Light object, initializes the strategy and sets the Direction
        /// </summary>
        /// <param name="member"></param>
        /// <param name="d"></param>

        public Light(ISignalStrategy member, Direction d) : base(d)
        {
            if (member == null)
                throw new ArgumentException("ISignalStrategy cannot be null");
            if (d == Direction.None)
                throw new ArgumentException("The direction of the light cannot be null");
            this.strategy = member;
        }

        /// <summary>
        /// Automatic property for Colour, can get or set the value
        /// </summary>
        public Colour Colour
        {
            get; set;
        }
    }
}
