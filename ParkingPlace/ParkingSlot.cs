using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
    public class ParkingSlot
    {
        /// <summary>
        /// cout the motocycle
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <returns></returns>
        public static int CountMc(string parkingSlot)
        {
            if (parkingSlot == null)
            {
                return 0;
            }

            if (string.IsNullOrWhiteSpace(parkingSlot))
            {
                throw new DataMisalignedException();
            }

            int numberOfMcs = 0;
            int pos = parkingSlot.IndexOf(":");
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
        /// count car
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <returns></returns>
        public static int CountCar(string parkingSlot)
        {
            if (parkingSlot == null)
            {
                return 0;
            }

            if (string.IsNullOrWhiteSpace(parkingSlot))
            {
                throw new DataMisalignedException();
            }
            int numberOfCars = 0;
            int pos = parkingSlot.IndexOf(",");
            if (pos > 0)
            {
                numberOfCars=1;
            }
            return numberOfCars;
        }
        /// <summary>
        /// check for free space for motocycle
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
        /// check free space for car
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
        /// spliting the vehicles
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
        /// 
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
            else if(parkingSlot.Contains(','))
            {
                // Its a car not an MC.
                found = false;
            }
            else
            {
                // its a mc
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
            return found;
        }
        /// <summary>
        /// 
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
            else if (parkingSlot.Contains(','))
            {
                // Its a car 
                string carRegistrationNumber = GetRegistrationNumber(parkingSlot);
                if (carRegistrationNumber == registrationNumber)
                {
                    found = true;
                }
            }
            else if (parkingSlot.Contains(':'))
            {
                // its a motorcycle
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
     
            return found;
        }
        /// <summary>
        /// Search the parking slot for a registraion number with a search string.
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <param name="registrationNumberSearchString"></param>
        /// <returns></returns>
        public static string[] SearchVehicle(string parkingSlot, string registrationNumberSearchString)
        {
            List<string> searchResult=new List<string>();
            if (parkingSlot == null)
            {
                return null;
            }
            else if (parkingSlot.Contains(','))
           {
                // Its a car 
                if (parkingSlot.IndexOf(registrationNumberSearchString) != -1)
                {
                    searchResult.Add(parkingSlot);
                }

            }
            else
            {
                // its a motorcycle
                string[] vehilces = SplitVehicle(parkingSlot);
                foreach (string vehicle in vehilces)
                {
                    if (vehicle.IndexOf(registrationNumberSearchString) != -1)
                    {
                        searchResult.Add(":" + vehicle);
                    }
                }

            }
            if (searchResult.Count == 0)
            {
                return null;
            }

            return searchResult.ToArray();
        }
        /// <summary>
        /// get vehicle type of parked vehicles
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
                int positionOfRegistrationNumberTimeStampSeparatior= parkingSlot.IndexOf(',');
                
                if (positionOfRegistrationNumberTimeStampSeparatior > -1)
                {
                    type = VehicleType.Car;
                }
                else
                {
                    // : means it's one or two Mc
                    
                    type = VehicleType.Mc;
                }
            }
            return type;
        }
        /// <summary>
        /// remove the motocycle
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <param name="registrationNumber"></param>
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
        /// <summary>
        /// remove the vehicle
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <param name="registrationNumber"></param>
        public static void RemoveVehicle(ref string parkingSlot, string registrationNumber)
        {

            VehicleType type = ParkingSlot.GetVehicleTypeOfParkedVehicle(parkingSlot, registrationNumber);
            if (type == VehicleType.Car)
            {
                if (ParkingSlot.GetRegistrationNumber(parkingSlot)== registrationNumber)
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
        /// <summary>
        /// Gets the checkin timestamp
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <returns></returns>
        public static string GetCheckInTimeStamp(string parkingSlot)
        {

            string checkedInTimeStamp = null;
            int indexOfRegNrDateSeparator = parkingSlot.IndexOf(',');
            if (indexOfRegNrDateSeparator > -1)
            {
                checkedInTimeStamp = parkingSlot.Substring(indexOfRegNrDateSeparator + 1, parkingSlot.Length - indexOfRegNrDateSeparator-1);
            }

            return checkedInTimeStamp;
        }
        /// <summary>
        /// get registration numberof parking vehicle
        /// </summary>
        /// <param name="parkingSlot"></param>
        /// <returns></returns>
        public static string GetRegistrationNumber(string parkingSlot)
        {

            string registrationNumber = null;
            int indexOfRegNrDateSeparator = parkingSlot.IndexOf(',');
            if (indexOfRegNrDateSeparator > -1)
            {
                registrationNumber = parkingSlot.Substring(0,indexOfRegNrDateSeparator);
            }

            return registrationNumber;
        }
    }
}
