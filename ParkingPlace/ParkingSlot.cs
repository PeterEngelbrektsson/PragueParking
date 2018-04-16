using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
    /// <summary>
    /// count motor cycles 
    /// </summary>
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
        /// <summary>
        /// is parking free for moto cycle
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Check place is free for vehicle.
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Splite the two diffrent vehicles 
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Checking for contaning the Motocycle.
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Cheking the Vehicle is Containing the Vehicle
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public static bool ContainsVehicle(string parkingSlot, string registrationNumber)
        {
            bool found = false;
            if (parkingSlot == null)
            {
                found = false;
            }
            else if (parkingSlot.Contains(':'))
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
                // Its a car 
                if (parkingSlot == registrationNumber)
                {
                    found = true;
                }
                
            }
            return found;
        }
        /// <summary>
        //
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <param name="registrationNumber"></param>
        /// <returns></returns>
        public static VehicleType GetVehicleTypeOfParkedVehicle(string parkingSlot, string registrationNumber)
        {
            VehicleType type;
            if (parkingSlot== null)
            {
                throw new ParkingSpaceIsEmptyException();
            }
            else
            {
                int positionOfColon = parkingSlot.IndexOf(':');
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
        public static void RemoveMc(ref string parkingSlot, string registrationNumber)
        {
            int countMc = CountMc(parkingSlot);
            if (countMc==2)
            {
                string[] mcs = parkingSlot.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (mcs.Length == 2)
                {
                    if (mcs[0] == registrationNumber)
                    {
                        parkingSlot = ":" + mcs[1];
                    } else if(mcs[1] == registrationNumber)
                    {
                        parkingSlot = ":" + mcs[0];
                    }
                    else
                    {
                        throw new ArgumentException("Expected to find the mcs registration number in parkingslot.");
                    }
                }
            }
            else if(countMc==1)
            {
                // only one Mc
                parkingSlot = null;
            }
            else
            {
                throw new ArgumentException("Expected to find the 1 or 2 mcs in the parkingslot.");
            }
        }
        public static void RemoveVehicle(ref string parkingSlot, string registrationNumber)
        {

            VehicleType type = ParkingSlot.GetVehicleTypeOfParkedVehicle(parkingSlot, registrationNumber);
            if (type == VehicleType.Car)
            {
                if (parkingSlot == registrationNumber)
                {
                    // Setting to null tells its empty
                    parkingSlot = null;
                }
                else
                {
                    throw new VehicleNotFoundException();
                }
            }
            else if (type == VehicleType.Mc)
            {
                // Remove Mc
                RemoveMc(ref parkingSlot, registrationNumber);
            }

        }
    }
}
