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
            int pos = FindFreePlace(parkingPlace, registrationNumber, vehicleType);

            if ((parkingPlace[pos] != null) && vehicleType == VehicleType.Mc)
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
        public static VehicleType GetVehicleTypeOfParkedVehicle(string[] parkingPlace, int position, string registrationNumber)
        {
            VehicleType type;
            if (parkingPlace[position] == null)
            {
                throw new ParkingSpaceIsEmptyException();
            }
            else
            {
                int positionOfColon = parkingPlace[position].IndexOf(':');
                // : means it's one or two Mc
                if (positionOfColon >-1)
                {
                    type = VehicleType.Mc;
                }
                else
                {
                    // All strings are considered cars.
                    type = VehicleType.Car;
                }
            }
            return type;
        }

        public static void Move(string[] parkingPlace, string registrationNumber, int oldPosition,int newPosition)
        {
            VehicleType vehicleType = GetVehicleTypeOfParkedVehicle(parkingPlace, oldPosition, registrationNumber);
            Remove(parkingPlace, registrationNumber);
            Park(parkingPlace, registrationNumber, vehicleType);

        }
        public static int Find(string[] parkingPlace, string registrationNumber)
        {
            
            for (int i=0; i < parkingPlace.Length; i++) {
                if (parkingPlace[i] == registrationNumber)
                {
                    Console.WriteLine("Your Vehicle is found with registration number " + parkingPlace[i]);
                    return i;
                }
            }
            Console.WriteLine("Your Vehicle is not found with registration number " + registrationNumber);
            

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
