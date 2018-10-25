using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TrafficIntersection;

namespace TrafficIntersection
{
    /// <summary>
    /// Object used to model an Intersection.
    /// An intersection has an ISignalStrategy, a Grid, and a list of start coordinates
    /// </summary>
    public class Intersection : IEnumerable
    {
        private List<IVehicle> vehicles;
        private List<Vector2> startCoords;
        private Grid grid;
        private ISignalStrategy signal;
        static private Random random;

        /// <summary>
        /// Class parameterless Constructor
        /// </summary>
        public Intersection()
        {
            random = new Random();
            this.vehicles = new List<IVehicle>();
        }

        /// <summary>
        /// Class constructor; takes an ISignalStrategy, a list of vehicle starting coordinates, and a Grid as input
        /// </summary>
        public Intersection(ISignalStrategy signal, List<Vector2> startCoords, Grid grid)
        {
            if (grid == null)
                throw new ArgumentException("The grid cannot be null");
            if (signal == null)
                throw new ArgumentException("The signal cannot be null");
            if (startCoords == null)
                throw new ArgumentException("The list cannot be null");
            this.signal = signal;
            this.startCoords = startCoords;
            this.grid = grid;
            random = new Random();
            this.vehicles = new List<IVehicle>();
        }

        /// <summary>
        /// Causes interection to update. Moves all vehicles in the list of vehicles and updates the signal
        /// </summary>
        public void Update()
        {
            signal.Update();
            foreach (IVehicle i in vehicles)
            {
                i.Move(signal);
            }
        }

        /// <summary>
        /// Method used to add a new Vehicle to the class's Vehicle list.
        /// </summary>
        public void Add(IVehicle v)
        {
            if (v == null)
                throw new ArgumentException("Cannot add null vehicle");
            vehicles.Add(v);
            int rand = random.Next(startCoords.Count);
            v.X = (int)(startCoords[rand].X);
            v.Y = (int)(startCoords[rand].Y);
            v.Direction = this.grid[v.Y, v.X].Direction;
            v.Done += RemoveFromIntersection;
        }

        /// <summary>
        /// Event handler called when a vehicle's direction is None.
        /// </summary>
        protected void RemoveFromIntersection(IVehicle v)
        {
            if (v == null)
                throw new ArgumentException("Cannot remove null vehicle");
            vehicles.Remove(v);
        }

        /// <summary>
        /// Returns enumerator object used to enumerate through the class's vehicle list. 
        /// </summary>
        public IEnumerator GetEumerator()
        {
            return vehicles.GetEnumerator();
        }
    }
}