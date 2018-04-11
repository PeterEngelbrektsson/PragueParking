using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
    public class ParkingPlaceFullException : Exception
    {
        const string message = "The parking place has no room for the vehilce.";
        public ParkingPlaceFullException() : base(message)
        {

        }
        public ParkingPlaceFullException(string msg) : base(msg)
        {
        }
    }
}
