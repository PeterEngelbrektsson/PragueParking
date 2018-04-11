using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
    public class Parking
    {
        public static int Add(string [] parkingPlace, string registrationNumber, VehicleType vehicleType)
        {
            int pos = FindFreePlace(parkingPlace, registrationNumber, VehicleType);

            if (parkingPlace[pos] != null && VehicleType.MC) // parkingPlace[pos] ! inte pos!
            {
                parkingPlace[pos] = string.Concat(parkingPlace[pos],registrationNumber);
            }

            else
            {               
                parkingPlace[pos] = registrationNumber;
            }               

            return pos;
        }
        public static void Move(string[] parkingPlace, string registrationNumber, int newPosition)
        {
            
        }
        public static void Move(string[] parkingPlace, string registrationNumber, int oldPosition,int newPosition)
        {

        }
        public static int Find(string[] parkingPlace, string registrationNumber)
        {
            return -1;
        }
        public static int FindFreePlace(string[] parkingPlace, string registrationNumber, VehicleType vehicleType)
        {
            return -1;
        }

        public static void Remove(string[] parkingPlace, string registrationNumber)
        {

        }
    }
}
