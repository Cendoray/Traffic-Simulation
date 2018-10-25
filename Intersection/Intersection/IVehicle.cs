using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{
    //creating delegates
    public delegate void Action(IVehicle act);

    /**
     * 
     * 
     */
    public interface IVehicle
    {
        //creating events
        event Action Moving;
        event Action Waiting;
        event Action Done;

        //creating parameters
        Direction Direction
        {
            get;
            set;
        }

        int X
        {
            get;
            set;
        }

        int Y
        {
            get;
            set;
        }

        int Passengers
        {
            get;
        }

        double EmissionIdle
        {
            get;
        }

        double EmissionMoving
        {
            get;
        }


        //creating methods
        void Move(ISignalStrategy signal);
        bool NextIsIntersection();
        bool InIntersection();
    }
}
