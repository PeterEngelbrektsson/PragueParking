using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
    public class ParkingSlot
    {
        public static int CountMc(string parkingPlace)
        {
            if (parkingPlace == null)
            {
                return 0;
            }

            if (string.IsNullOrWhiteSpace(parkingPlace))
            {
                throw new DataMisalignedException();
            }

            int numberOfMcs = 0;
            int pos = parkingPlace.IndexOf(":");
            if (pos == 0)
            {
                numberOfMcs = 1;
            }
            else if (pos > 0)
            {
                numberOfMcs = 2;
            }
            return numberOfMcs;
        }
        public static bool IsFreeForMc(string parkingSlot, VehicleType vehicleType)
        {
            bool isFree = false;


            if (parkingSlot == null)
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
            }
            else if (vehicleType == VehicleType.Mc)
            {
                isFree = IsFreeForMc(parkingSlot, vehicleType);
            }

            return isFree;
        }
        public static string[] SplitVehicle(string parkingSlot)
        {
            string[] result = new string[1];
            if (parkingSlot == null)
            {
                result = null;
            }
            else if (parkingSlot.Contains(':'))
            {
                char[] separator =new char[] {':'};
                result = parkingSlot.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                result[0] = parkingSlot;
            }
            return result;
        }
        public static bool ContainsMc(string parkingSlot, string registrationNumber)
        {
            bool found = false;
            if (parkingSlot == null)
            {
                found = false;
            }
            else if(parkingSlot.Contains(':'))
            {
                string[] vehilces = SplitVehicle(parkingSlot);
                foreach (string vehicle in vehilces)
                {
                    if (vehicle == registrationNumber)
                    {
                        found = true;
                        break;
                    }
                }
            }
            else
            {
                // Its a car not an MC.
                found = false;
            }
            return found;
        }
    }
}
