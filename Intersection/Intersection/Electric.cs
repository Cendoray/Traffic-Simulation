using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{
    public class Electric : IVehicle
    {

        IVehicle vehicle;


        /// <summary>
        /// Constructor used to create an eletric vehicle
        /// </summary>
        /// <param name="vehicle">the vehicle which becomes eletric</param>
        public Electric(IVehicle vehicle) {
            this.vehicle = vehicle;
            vehicle.Moving += Moving;
            vehicle.Waiting += Waiting;
            vehicle.Done += Done;
        }
        public IVehicle Vehicle
        {
            get { return vehicle; }
        }
        /// <summary>
        /// Parameter for the direction of the car (setup in Intersection)
        /// </summary>
        public Direction Direction {
            get { return Vehicle.Direction; }
            set {Vehicle.Direction = value;} }
        /// <summary>
        /// Parameter for X location of car
        /// </summary>
        public int X
        {
            get {

                return Vehicle.X; }
            set {
                if (value >= 0)
                {
                    Vehicle.X = value;
                }
                else throw new ArgumentException("Invalid Input"); }
        }
        /// <summary>
        /// Parameter for Y location of car
        /// </summary>
        public int Y
        {
            get { return Vehicle.Y; }
            set {
                if (value >= 0) Vehicle.Y = value;
                else throw new ArgumentException("Invalid Input"); }
        }
        /// <summary>
        /// Parameter for Number of passengers
        /// </summary>
        public int Passengers
        {
            get { return Vehicle.Passengers; }
            set { ((Vehicle)Vehicle).Passengers = value; }
        }
        /// <summary>
        /// Parameter for the emission when idle
        /// </summary>
        public double EmissionIdle
        {
            get { return 0; }
            set {; }
    }
        /// <summary>
        /// parameter for the emission when moving
        /// </summary>
        public double EmissionMoving
        {
            get { return (double)(Vehicle.EmissionMoving/4.0); }
            set {; }
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
            return Vehicle.InIntersection();
        }
        /// <summary>
        /// Method to move the vehicle using its turn signal
        /// </summary>
        /// <param name="signal"></param>
        public void Move(ISignalStrategy signal)
        {
            Vehicle.Move(signal);
        }
        /// <summary>
        /// Method used to verify if there is an intersection incocming
        /// </summary>
        /// <returns>A Boolean value</returns>
        public bool NextIsIntersection()
        {
            return Vehicle.NextIsIntersection();
        }
    }
}
