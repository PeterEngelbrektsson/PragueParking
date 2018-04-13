using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
   
    public class VehicleAlreadyAtThatPlaceException : Exception
    {
        const string message = "The vehicle is already at that place.";
        public VehicleAlreadyAtThatPlaceException() : base(message)
        {

        }
        public VehicleAlreadyAtThatPlaceException(string msg) : base(msg)
        {
        }
    }
}
