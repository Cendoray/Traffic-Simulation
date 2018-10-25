using System;
using System.Collections.Generic;
using System.Text;
using TrafficIntersection;

namespace TrafficIntersection
{
    /*
        Concrete implementation if ISignalStrategy.
        Works for intersection with 4 lights; the length of each light is of fixed length.
    */
    public class FixedSignal : ISignalStrategy
    {
        // Timing of lights: 
        // { rightleft Green length, rightleft Amber length,  updown Green length,  updown Amber length }
        private int[] timing;
        private int currentIndex;
        private int counter;
        private Colour rightleft;
        private Colour updown;

        /// <summary>
        /// Class constructor, assigns initial value to fields, does input validation for timing array.
        /// </summary>
        /// <param name="timing">Timing of the lights</param>
        public FixedSignal(params int[] timing)
        {
            // Checks if the length of the array is 4; if it isn't an exception is thrown.
            this.timing = timing.Length == 4 ? timing :
                throw new ArgumentException("Array passed to constructor has invalid Length: " + timing.Length);
            this.currentIndex = 0;
            this.counter = 0;
            rightleft = Colour.Green;
            updown = Colour.Red;
        }

        /// <summary>
        /// Return the colour of the light in a given direction.
        /// Up and Down always have the same Colour and so do Left and Right.
        /// </summary>
        /// <param name="dir">Direction the given light is facing</param>
        /// <returns>The Colour </returns>
        public Colour GetColour(Direction dir)
        {
            if (dir == Direction.Down || dir == Direction.Up)
            {
                return updown;
            }
            else if (dir == Direction.Right || dir == Direction.Left)
            {
                return rightleft;
            }
            else
            {
                throw new ArgumentException("In valid direction -- Direction." + dir);
            }
        }

        /// <summary>
        /// Increments the counter, updates the colour of the lights
        /// </summary>
        public void Update()
        {
            this.counter++;

            if (this.counter == this.timing[currentIndex])
            {
                if (this.rightleft == Colour.Green
                    && this.updown == Colour.Red)
                {
                    this.rightleft = Colour.Amber;
                }
                else if (this.rightleft == Colour.Red
                  && this.updown == Colour.Green)
                {
                    this.rightleft = Colour.Amber;
                }
                else if (this.updown == Colour.Amber)
                {
                    this.updown = Colour.Red;
                    this.rightleft = Colour.Green;
                }
                else
                {
                    this.rightleft = Colour.Red;
                    this.updown = Colour.Green;
                }

                this.counter = 0;
                this.currentIndex++;

                if (this.currentIndex == this.timing.Length)
                {
                    this.currentIndex = 0;
                }
            }
        }
    }
}
