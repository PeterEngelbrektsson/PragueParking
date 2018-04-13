using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParkingPlace
{
    public partial class Parking // Parking place
    {
        public const int MaxLengthOfRegistrationNumber = 10;

        public static bool ValidRegistrationNumber(string registrationNumber)
        {
            bool valid = true;
            if (registrationNumber.IndexOf(':') > -1)
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

            if ((parkingPlace[pos] != null) && vehicleType == VehicleType.Mc) // If parking place not empty and vehicle is motorcyle
            {
                parkingPlace[pos] = string.Concat(registrationNumber,parkingPlace[pos] ); // then add the motorcycle before  the ':' char before first motorcycle
            }

            else
            {
                if (vehicleType == VehicleType.Mc) // if parking place empty and the vehicle is a motorcycle ?
                {
                    parkingPlace[pos] = string.Concat(':',registrationNumber); // add a char of ':' at beginning of registration number string to mark it as a motorcycle
                }

                else
                {
                    parkingPlace[pos] = registrationNumber; // else, add it
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
    
        public static int Find(string[] parkingPlace, string registrationNumber)
        {

            for (int i = 0; i < parkingPlace.Length; i++)
            {
                // Try to find car
                if (parkingPlace[i] == registrationNumber)
                {
                    // Car found
                    return i;
                }else 
                {
                    // Try to find motorcycle
                    if (ParkingSlot.ContainsMc(parkingPlace[i], registrationNumber)) { 
                           // Mc found 
                            return i;
                    }
                }
            }
            //Your Vehicle is not found 

            return -1;
        }

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


        public static int Remove(string[] parkingPlace, string registrationNumber)
        {
            int found = -1;
            for (int i = 0; i < parkingPlace.Length; i++)
            {
                if (ParkingSlot.ContainsVehicle(parkingPlace[i], registrationNumber))
                {
                    found = i;
                    ParkingSlot.RemoveVehicle(ref parkingPlace[i], registrationNumber);
                    break;
                }
            }
            if (found<0)
            {
                throw new VehicleNotFoundException();
            }
            return found;
        }
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
