using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{
    public interface ISignalStrategy
    {
        void Update();
        Colour GetColour(Direction dir);
    }
}
