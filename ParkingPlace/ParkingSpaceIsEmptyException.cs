using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
  
    public class ParkingSpaceIsEmptyException : Exception
    {
        const string message = "The parking place doesn't contain any vehilce.";
        public ParkingSpaceIsEmptyException() : base(message)
        {

        }
        public ParkingSpaceIsEmptyException(string msg) : base(msg)
        {
        }
    }
}
