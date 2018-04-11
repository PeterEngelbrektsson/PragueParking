using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParkingPlace
{
    public class Parking
    {
        public static int Park(string[] parkingPlace,string regnr, VehicleType vehicleType)
        {
            return -1;
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
        public static bool IsFreeForMc(string parkingSlot, VehicleType vehicleType)
        {
            bool isFree = false;

            
            if (parkingSlot==null)
            {
                isFree = true;
            }
            else
            {
                int positionOfColon = parkingSlot.IndexOf(':');

                // : on first or last position means one available place.
                if (positionOfColon == 0 | (positionOfColon == (parkingSlot.Length - 1)))
                {
                    isFree = true;
                }
            }

            return isFree;

        }
        public static bool IsFreeFor(string parkingSlot, VehicleType vehicleType)
        {
            bool isFree = false;

            if (vehicleType == VehicleType.Car)
            {
                isFree = (parkingSlot == null);
            }else if (vehicleType == VehicleType.Mc)
            {
                isFree = IsFreeForMc(parkingSlot, vehicleType);
            }

            return isFree;
        }

        public static int FindFreePlace(string[] parkingPlace, string registrationNumber, VehicleType vehicleType)
        {
            bool found = false;
            int position = 0;

            do
            {
                if (IsFreeFor(parkingPlace[position],vehicleType))
                {
                    found = true;
                }
                else
                {
                    position++;
                }
            } while (!found & (position < parkingPlace.Length));

            // Not found => throw exception
            if(!found && position >= parkingPlace.Length)
            {
                throw new ParkingPlaceFullException();
            }
            return position;
        }

        public static void Remove(string[] parkingPlace, string registrationNumber)
        {

        }
    }
}
