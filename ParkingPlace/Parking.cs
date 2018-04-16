using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ParkingPlace
{
    public partial class Parking // Parking place
    {
        public const int MaxLengthOfRegistrationNumber = 10;


        /// <summary>
        /// Validates the registration number
        /// </summary>
        /// <param name="registrationNumber"></param>
        /// <returns>Boolean if the registraion number is valid or not</returns>
        public static bool ValidRegistrationNumber(string registrationNumber)
        {
            bool valid = true;
            if (registrationNumber.IndexOf(':') > -1)
            {
                valid = false;
            }
            if (!Regex.IsMatch(registrationNumber, @"^[A-Z0-9]*$"))
            {
                valid = false;
            }
            if (registrationNumber.Length > MaxLengthOfRegistrationNumber)
            {
                valid = false;
            }
            return valid;
        }

        public static int Add(string [] parkingPlace, string registrationNumber, VehicleType vehicleType)
        {
            int pos = Find(parkingPlace, registrationNumber);
            if(pos != -1)
            {
                // The registration number already exists
                throw new RegistrationNumberAlreadyExistException();
            }

            pos = FindFreePlace(parkingPlace, vehicleType);
            bool containsOneMc = (ParkingSlot.CountMc(parkingPlace[pos])==1);
            // contains one mc and adding one mc
            if ((parkingPlace[pos] != null) && containsOneMc && vehicleType == VehicleType.Mc) // If parking place not empty and vehicle is motorcyle
            {
                parkingPlace[pos] = string.Concat(registrationNumber,parkingPlace[pos] ); // then add the motorcycle before  the ':' char before first motorcycle
            }

            else // adding to empty place
            {
                if (vehicleType == VehicleType.Mc) // if parking place empty and the vehicle is a motorcycle ?
                {
                    parkingPlace[pos] = string.Concat(':',registrationNumber); // add a char of ':' at beginning of registration number string to mark it as a motorcycle
                }

                else
                {
                    parkingPlace[pos] = registrationNumber+","+DateTime.Now; // else, add it. Add timestamp
                }
            }

            return pos;
        }
              
        public static void Move(string[] parkingPlace, string registrationNumber, int newPosition)
        {
            int oldPosition = Find(parkingPlace, registrationNumber);
            if (oldPosition < 0)
            {
                throw new VehicleNotFoundException("The vehicle "+registrationNumber+" can not be found ");
            }
            if (oldPosition == newPosition)
            {
                throw new VehicleAlreadyAtThatPlaceException();
            }
            VehicleType type = GetVehicleTypeOfParkedVehicle(parkingPlace, oldPosition, registrationNumber);
            Move(parkingPlace, registrationNumber, type, oldPosition, newPosition);
        }

        /// <summary>
        /// Optimizes the parkin place. Moves single parked motorcycles together in the same slot.
        /// Returns an array of messages with parking movements to be performed by employees.
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <returns>An array of messages with parking movements to be performed by employees.</returns>
        public static string[] Optimize(string[] parkingPlace)
        {
            bool found;
            List<string> messages = new List<string>();
            int firstSingleMcPosition = 0;
            int lastSingleMcPosition = parkingPlace.Length - 1;
            do
            {
                found = false;
                firstSingleMcPosition = FindFirstSingleParkedMc(parkingPlace, firstSingleMcPosition);
                lastSingleMcPosition = FindLastSingleParkedMc(parkingPlace, lastSingleMcPosition);
                // Can have found different Mcs.
                if (firstSingleMcPosition != lastSingleMcPosition && (firstSingleMcPosition != -1 && lastSingleMcPosition != -1))
                {
                    string registrationNumber = (parkingPlace[lastSingleMcPosition]).Trim(':');
                    messages.Add(String.Format("Move motorcycle {0} from parkingplace {1} to place {2}.", registrationNumber, lastSingleMcPosition + 1, firstSingleMcPosition + 1)); // Display should be one based
                    Move(parkingPlace, registrationNumber, VehicleType.Mc, lastSingleMcPosition, firstSingleMcPosition);
                    found = true;
                }

                //  return -1 from search functions means that the search has reached the end or start of the array => exit optimize loop
            } while (found && (firstSingleMcPosition != -1 && lastSingleMcPosition != -1));

            return messages.ToArray();
        }
        /// <summary>
        /// Counts the number of single parked motorcycles in the parking place
        /// </summary>
        /// <param name="parkingPlace">The parking place</param>
        /// <returns>Number of single parked motorcycles</returns>
        public static int NumberOfSingleParkedMcs(string[] parkingPlace)
        {
            bool found;
            int numnberOfSingles = 0;
            int firstSingleMcPosition = -1;
            do
            {
                found = false;
                firstSingleMcPosition = FindFirstSingleParkedMc(parkingPlace, firstSingleMcPosition+1);
                if ((firstSingleMcPosition > -1 )&& (firstSingleMcPosition < parkingPlace.Length-1))
                {
                    found = true;
                    numnberOfSingles++;
                }

                //  return -1 from search functions means that the search has reached the end or start of the array => exit optimize loop
            } while ((found && (firstSingleMcPosition != -1)) && (firstSingleMcPosition < parkingPlace.Length-1));

            return numnberOfSingles;
        }
        /// <summary>
        /// Moves a vehicel from a position to a new position.
        /// Should be used if the old position is known.
        /// </summary>
        /// <param name="parkingPlaces"></param>
        /// <param name="registrationNumber"></param>
        /// <param name="vehicleType"></param>
        /// <param name="oldPosition"></param>
        /// <param name="newPosition"></param>
        public static void Move(string[] parkingPlaces, string registrationNumber, VehicleType vehicleType, int oldPosition, int newPosition)
        {
            if (oldPosition < 0)
            {
                throw new ArgumentException();
            }
            if (oldPosition > (parkingPlaces.Length - 1))
            {
                throw new ArgumentException();
            }
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

            if (vehicleType == VehicleType.Car)
            {
                if (parkingPlaces[newPosition] != null)
                {
                    throw new ParkingPlaceOccupiedException();
                }
                parkingPlaces[newPosition] = parkingPlaces[oldPosition];
                parkingPlaces[oldPosition] = null;

            }
            else if (vehicleType == VehicleType.Mc)
            {
                int numberOfMcAtOldPosition = ParkingSlot.CountMc(parkingPlaces[oldPosition]);
                if (numberOfMcAtOldPosition < 0)
                {
                    throw new ArgumentException("No Mc found to move at that position.");
                }

                if (numberOfMcAtOldPosition == 2)
                {
                    Remove(parkingPlaces, registrationNumber); 
                    AddMcAtPosition(parkingPlaces, registrationNumber, newPosition);
                }
                if (numberOfMcAtOldPosition == 1)
                {
                    AddMcAtPosition(parkingPlaces, registrationNumber, newPosition);
                    parkingPlaces[oldPosition] = null;
                }
            }

        }
    
        /// <summary>
        /// Finds a vehicle with a distinct registration number.
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <param name="registrationNumber"></param>
        /// <returns>Position of the found vehicle. -1 if not found</returns>
        public static int Find(string[] parkingPlace, string registrationNumber)
        {

            for (int i = 0; i < parkingPlace.Length; i++)
            {

                 // Try to find motorcycle
                if (ParkingSlot.ContainsMc(parkingPlace[i], registrationNumber))
                {
                    // Mc found 
                    return i;
                }else if(parkingPlace[i]==null)
                {
                    // empty parking place. Do nothing.
                }
                else
                {
                    // try to find a car
                    string CarRegNr= null;
                    int indexOfRegNrDateSeparator = parkingPlace[i].IndexOf(',');
                    if (indexOfRegNrDateSeparator > -1)
                    {
                        CarRegNr = ParkingSlot.GetRegistrationNumber(parkingPlace[i]);
                    }
                    // Try to find car
                    if (registrationNumber == CarRegNr)
                    {
                        // Car found
                        return i;
                    }
                }

            }
            //Your Vehicle is not found 

            return -1;
        }
        /// <summary>
        /// Searches the parking place for vehicles with registration number mathing the search string
        /// </summary>
        /// <param name="parkingPlace">The parking place</param>
        /// <param name="searchString">Search string for registraion number</param>
        /// <returns></returns>
        public static Dictionary<int,string> FindSearchString(string[] parkingPlace, string searchString)
        {
            Dictionary<int, string> matchingVehicles= new Dictionary<int, string>();
            for(int i = 0;i < parkingPlace.Length; i++){
                string[] slotSearchResult = ParkingSlot.SearchVehicle(parkingPlace[i],searchString);
                if (slotSearchResult != null && slotSearchResult.Length > 0)
                {
                    foreach (string match in slotSearchResult)
                    {
                        matchingVehicles.Add(i, match);
                    }
                }
            }
            return matchingVehicles;
        }
        /// <summary>
        /// Searches the parking place for a free place for a specific vehicle type.
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <param name="vehicleType">The type of vehicle</param>
        /// <returns></returns>
        public static int FindFreePlace(string[] parkingPlace, VehicleType vehicleType)
        {
            
            bool found = false;
            int position = 0;

            do
            {
                if (ParkingSlot.IsFreeFor(parkingPlace[position], vehicleType))
                {
                    found = true;
                }
                else
                {
                    position++;
                }
            } while (!found & (position < parkingPlace.Length));

            // Not found => throw exception
            if (!found && position >= parkingPlace.Length)
            {
                throw new ParkingPlaceFullException();
            }
            return position;
        }
        
        /// <summary>
        /// Removes a vehicle.
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <param name="registrationNumber"></param>
        /// <returns>parking place number, check in timestamp</returns>
        public static KeyValuePair<int,string> Remove(string[] parkingPlace, string registrationNumber)
        {
            string checkInTimeStamp="";
            KeyValuePair<int, string> result=new KeyValuePair<int, string>();
            int found = -1;
            for (int i = 0; i < parkingPlace.Length; i++)
            {
                if (ParkingSlot.ContainsVehicle(parkingPlace[i], registrationNumber))
                {
                    found = i;
                    VehicleType vehicleType = GetVehicleTypeOfParkedVehicle(parkingPlace, found, registrationNumber);
                    if (vehicleType == VehicleType.Car)
                    {
                        checkInTimeStamp = ParkingSlot.GetCheckInTimeStamp(parkingPlace[found]);

                    }
                    result = new KeyValuePair<int, string>(found, checkInTimeStamp);
                    ParkingSlot.RemoveVehicle(ref parkingPlace[i], registrationNumber);
                    break;
                }
            }
            if (found<0)
            {
                throw new VehicleNotFoundException();
            }

 
            return result;
        }
        /// <summary>
        /// Finds all parked vehicles in the parkingplace.
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <returns>A dictionary with all found vehicles and their parking places. Key is parking place number. Value is registration number.</returns>
        public static Dictionary<int,string> ListParkedVehicels(string[] parkingPlace)
        {
            Dictionary<int, string> parkedVehicles= new Dictionary<int, string>();
            for(int i=0;i<parkingPlace.Length;i++)
            {
                if (parkingPlace[i]!= null)
                {
                    parkedVehicles.Add(i, parkingPlace[i]);
                }
            }
            return parkedVehicles;
        }
    }
}
