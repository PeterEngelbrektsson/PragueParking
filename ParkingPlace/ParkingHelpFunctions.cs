using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
    public partial class Parking
    {
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
                if (positionOfColon > -1)
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
        public static int FindFirstSingleParkedMc(string[] parkingPlace, int startPosition)
        {
            if (startPosition > (parkingPlace.Length - 1))
            {
                throw new ArgumentException();
            }
            if (startPosition < 0)
            {
                throw new ArgumentException();
            }

            for (int i = startPosition; i < parkingPlace.Length; i++)
            {
                if (ParkingSlot.CountMc(parkingPlace[i]) == 1)
                {
                    return i;
                }
            }
            // No single parked mc found => returning -1
            return -1;
        }
        public static int FindLastSingleParkedMc(string[] parkingPlace, int startPosition)
        {
            if (startPosition > (parkingPlace.Length - 1))
            {
                throw new ArgumentException();
            }
            if (startPosition < 0)
            {
                throw new ArgumentException();
            }

            for (int i = startPosition; i >= 0; i--)
            {
                if (ParkingSlot.CountMc(parkingPlace[i]) == 1)
                {
                    return i;
                }
            }
            // No single parked mc found => returning -1
            return -1;
        }
        public static void AddMcAtPosition(string[] parkingPlaces, string registrationNumber, int newPosition)
        {
            if (newPosition < 0)
            {
                throw new ArgumentException();
            }
            if (newPosition > (parkingPlaces.Length - 1))
            {
                throw new ArgumentException();
            }
            if (string.IsNullOrEmpty(registrationNumber))
            {
                throw new ArgumentException();
            }

            int numberOfMcAtNewPosition = ParkingSlot.CountMc(parkingPlaces[newPosition]);
            if (numberOfMcAtNewPosition == 0 && parkingPlaces[newPosition] != null)
            {
                throw new ParkingPlaceOccupiedException("The new place to move the MC to is occupied by a car.");
            }
            if (numberOfMcAtNewPosition == 2)
            {
                throw new ParkingPlaceOccupiedException("The new place to move the MC to is occupied by two MCs.");
            }
            if (parkingPlaces[newPosition] == null)
            {
                parkingPlaces[newPosition] = ":" + registrationNumber;
            }
            else
            {
                if (ParkingSlot.CountMc(parkingPlaces[newPosition]) != 1)
                {
                    throw new DataMisalignedException("The parkingplace should contain one Mc but doesn't.");
                }
                // Add registration numnber at left side of sign
                parkingPlaces[newPosition] = registrationNumber + parkingPlaces[newPosition];
            }

        }
    }
}
