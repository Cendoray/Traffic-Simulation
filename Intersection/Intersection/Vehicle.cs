using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{
    public abstract class Vehicle : IVehicle
    {

        //private values for the parameters else they give stackoverflow due to set parameter recursive calls
        private int updatedX;
        private int updatedY;

        private Grid grid;
        /// <summary>
        /// Constructor creating a Vehicle based on the emission produced while moving, idle, the number of passengers and the grid
        /// </summary>
        /// <param name="emissionMoving">The emission rate while moving</param>
        /// <param name="emissionIdle">The emission rate while idle</param>
        /// <param name="passengers">Number of passengers</param>
        /// <param name="grid">The Grid it is situated on</param>
        public Vehicle(double emissionMoving, double emissionIdle, int passengers, Grid grid)
        {
            if (grid == null || emissionMoving <= 0 || emissionIdle <= 0 || passengers <= 0)
            {
                throw new ArgumentException("Your values are not valid, please enter correct ones");
            }
            this.grid = grid;
            Passengers = passengers;
            EmissionMoving = emissionMoving;
            EmissionIdle = emissionIdle;
            Direction = Direction.None;

        }
        /// <summary>
        /// Parameter for the direction of the car (setup in Intersection)
        /// </summary>
        public Direction Direction { get; set; }
        /// <summary>
        /// Parameter for X location of car
        /// </summary>
        public int X
        {
            get { return updatedX; }
            set {
                if (value >= 0)
                {
                    grid[updatedX, updatedY].Occupied = false;
                    updatedX = value;
                    grid[updatedX, updatedY].Occupied = true;
                }
                else throw new ArgumentException("Invalid Input"); }
        }
        /// <summary>
        /// Parameter for Y location of car
        /// </summary>
        public int Y
        {
            get { return updatedY; }
            set {
                if (value >= 0) {
                    grid[updatedX, updatedY].Occupied = false;
                    updatedY = value;
                    grid[updatedX, updatedY].Occupied = true;
                }
                else throw new ArgumentException("Invalid Input"); }
        }
        /// <summary>
        /// Parameter for Number of passengers
        /// </summary>
        public int Passengers
        {
            get; set;
        }
        /// <summary>
        /// Parameter for the emission when idle
        /// </summary>
        public double EmissionIdle
        {
            get; set;
        }
        /// <summary>
        /// parameter for the emission when moving
        /// </summary>
        public double EmissionMoving
        {
            get; set;
        }
        /// <summary>
        /// Event for when vehicle is moving
        /// </summary>
        public event Action Moving;
        /// <summary>
        /// Event for when vehicle is waiting (at intersection)
        /// </summary>
        public event Action Waiting;
        /// <summary>
        /// Event for when vehicle is finished (outside of the game space)
        /// </summary>
        public event Action Done;

        /// <summary>
        /// Method which returns whether or not a vehicle is in the intersection
        /// </summary>
        /// <returns>A Boolean value</returns>
        public bool InIntersection()
        {
            if (grid[Y, X] is IntersectionTile)
                return true;
            return false;
        }
        /// <summary>
        /// Method to move the vehicle using its turn signal
        /// </summary>
        /// <param name="signal"></param>
        public void Move(ISignalStrategy signal)
        {
            if (signal == null)
                throw new ArgumentException("signal is null");
            Boolean error = false;
            Boolean occupied = false;
            int updatedX = X;
            int updatedY = Y;
            switch (Direction)
            {
                case Direction.Up:
                    updatedY--;
                    break;
                case Direction.Down:
                    updatedY++;
                    break;
                case Direction.Left:
                    updatedX--;
                    break;
                case Direction.Right:
                    updatedX++;
                    break;
                default:
                    break;
            }
            if (!grid.InBounds(updatedX, updatedY) || Direction == Direction.None)
                error = true;
            else if (grid.InBounds(updatedX, updatedY) && grid.IsOccupied(updatedX, updatedY))
                    occupied = true;
            if (error)
            {
                Done?.Invoke(this);
                Direction = Direction.None;

            }
            else if ((NextIsIntersection() && !InIntersection()) || occupied)
            {
                
                if ((signal.GetColour(Direction) == Colour.Red || signal.GetColour(Direction) == Colour.Amber))
                {
                    Waiting?.Invoke(this);
                }
            }
            else if(!occupied)
            {
                Moving?.Invoke(this);
                grid[updatedX, updatedY].Occupied = true;
                grid[X, Y].Occupied = false;
                this.X = updatedX;
                this.Y = updatedY;
            }
        }
        /// <summary>
        /// Method used to verify if there is an intersection incocming
        /// </summary>
        /// <returns>A Boolean value</returns>
        public bool NextIsIntersection()
        {
            switch (Direction)
            {
                case Direction.Up:
                    if (grid[X, Y - 1] is IntersectionTile)
                        return true;
                    break;
                case Direction.Down:
                    if (grid[X, Y + 1] is IntersectionTile)
                        return true;
                    break;
                case Direction.Left:
                    if (grid[X - 1, Y] is IntersectionTile)
                        return true;
                    break;
                case Direction.Right:
                    if (grid[X + 1, Y] is IntersectionTile)
                        return true;
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
