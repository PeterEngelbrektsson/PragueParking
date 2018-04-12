using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
    public class VehicleNotFoundException : Exception
    {
        const string message = "The vehilce can not be found.";
        public VehicleNotFoundException() : base(message)
        {

        }
        public VehicleNotFoundException(string msg) : base(msg)
        {
        }
    }
}
