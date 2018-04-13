using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
    public class RegistrationNumberAlreadyExistException : Exception
    {
        const string message = "There is already a vehicle with the same registration number.";
        public RegistrationNumberAlreadyExistException() : base(message)
        {

        }
        public RegistrationNumberAlreadyExistException(string msg) : base(msg)
        {
        }
    }
}
