using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{

    delegate void Gone(IVehicle over);
    delegate void Moving(IVehicle moved);
    delegate void idle(IVehicle wait);
    public class Total
    {

        //private values for the parameters else they give stackoverflow due to set parameter recursive calls
        private double emission;
        private int passengers;
        private int totalV;

        //properties
        public int Passengers
        {
            get { return passengers; }
            set {
                if (value >= 0) passengers = value;
                else throw new ArgumentException("Invalid Input"); }
        }

        public double Emissions
        {
            get { return emission; }
            set
            {
                if (value >= 0) emission = value;
                else throw new ArgumentException("Invalid Input");
            }
        }

        public Total(int totalVehicules)
        {
            if (totalVehicules < 0)
                throw new ArgumentException("Total vehicules cannot be under 0");
            this.totalV = totalVehicules;
        }


        private void addPassengers(int updateValue)
        {
            Passengers = updateValue + Passengers;
        }

        private void addEmissions(double updateValue)
        {
            Emissions = updateValue + Emissions;
        }

        public void updatePassengers(IVehicle vehicle)
        {
            if (vehicle == null) throw new ArgumentException("Vehicle is null");
            addPassengers(vehicle.Passengers);
        }

        public void updateEmission(IVehicle vehicle)
        {
            if (vehicle == null) throw new ArgumentException("Vehicle is null");
            if (vehicle.InIntersection())
                addEmissions(vehicle.EmissionIdle);
            else
                addEmissions(vehicle.EmissionMoving);
        }

    }
}
