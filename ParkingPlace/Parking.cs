using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParkingPlace
{
    public partial class Parking
    {

        public static void DisplayMenu(string[] parkingPlace, VehicleType vehicleType)
        {
            // Console.Clear(); -- Do we want to clear screen between repeat displays of the menu or not ? 

            Console.WriteLine();
            Console.WriteLine("  Prague Parking v1.0");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1. Add a car"); 
            Console.WriteLine("2. Add a motorcyle");
            Console.WriteLine("3. Move a vehicle");
            Console.WriteLine("4. Find a vehicle");
            Console.WriteLine("5. Remove a vehicle");
            Console.WriteLine("6. Find free place");
            Console.WriteLine("7. Optimize parking lot");

            int choice = int.Parse(Console.ReadLine()); // Store user choice

            string registrationNumber = "ABC123"; // pseudo registration number

            switch (choice) // Check user choice
            {             
                case 1: // Add a car

                    Console.WriteLine("Please enter the registration number of the vehicle : ");
                    registrationNumber = Console.ReadLine();

                    vehicleType = VehicleType.Car; // Set vehicle type to car

                    int position = Add(parkingPlace, registrationNumber, vehicleType); // Park at suitable position (if any)

                    Console.WriteLine("Your vehicle has been parked at place number {0}.", position + 1); 
                    break;

                case 2: // Add a motorcycle

                    Console.WriteLine("Please enter the registration number of the vehicle : ");
                    registrationNumber = Console.ReadLine();
                    
                    vehicleType = VehicleType.Mc; // Set vehicle type to motorcycle

                    position = Add(parkingPlace, registrationNumber, vehicleType); // Park at suitable position (if any)

                    Console.WriteLine("Your vehicle has been parked at place number {0}.", position + 1);
                    break;

                case 3: // Move a vehicle

                    int newPosition = FindFreePlace(parkingPlace, vehicleType ); // Original position of the vehicle

                    Move(parkingPlace, registrationNumber, newPosition);  // Move vehicle to new position
                    break;

                case 4: // Find a vehicle

                    Console.WriteLine("Please enter the registration number of the vehicle : ");
                    registrationNumber = Console.ReadLine();

                    position = Find(parkingPlace, registrationNumber); // Position where vehicle is located (if any)

                    Console.WriteLine("Your vehicle is parked at spot number {0}.", position + 1); // Parking spots numbered 1 - 100 !
                    break;

                case 5: // Remove a vehicle

                    Console.WriteLine("Please enter the registration number of the vehicle : ");
                    registrationNumber = Console.ReadLine();

                    Console.WriteLine("Please specify if your vehicle is a car or an mc : ");

                    string isCarOrMc = Console.ReadLine();

                    if (isCarOrMc == "mc") // User input is mc ?
                    {
                        vehicleType = VehicleType.Mc; // It's a motorcycle
                    }

                    else if (isCarOrMc == "car") // User input is car ?
                    {
                        vehicleType = VehicleType.Car; // It's a car
                    }

                    else
                    {
                        throw new ArgumentException(); // Neither nor, then throw exception !
                    }

                    Remove(parkingPlace, registrationNumber); // Remove the vehicle with the specificed registration number (if it exists in the parking lot)
                    break;

                case 6: // Find free parking spot

                    Console.WriteLine("Please specify if your vehicle is a car or an mc : ");

                    isCarOrMc = Console.ReadLine(); // get user input

                    if (isCarOrMc == "mc")
                    {
                        vehicleType = VehicleType.Mc; // It's a motorcycle
                    }

                    else if (isCarOrMc == "car")
                    {
                        vehicleType = VehicleType.Car; // It's a car
                    }

                    else
                    {
                        throw new ArgumentException(); // Neither car nor mc, throw exception !

                        break;
                    }

                    position = FindFreePlace(parkingPlace, vehicleType); // Find a free position for car or mc, depending on user choice

                    Console.WriteLine("There is a free place for your vehicle at {0}.", position + 1);
                    break;

                case 7: // Optimize parking spot

                    Optimize(parkingPlace); // Optimize the parking place
                    break;

                default: // None of the above, throw exception !

                    throw new ArgumentException();
                    break;
            }

            return;
        }
        
        public static int Add(string [] parkingPlace, string registrationNumber, VehicleType vehicleType)
        {
            int pos = FindFreePlace(parkingPlace, vehicleType);

            if ((parkingPlace[pos] != null) && vehicleType == VehicleType.Mc) // If parking place not empty and vehicle is motorcyle
            {
                parkingPlace[pos] = string.Concat(registrationNumber,parkingPlace[pos] ); // then add the motorcycle after the ':' char after first motorcycle
            }

            else
            {
                if (vehicleType == VehicleType.Mc) // if parking place empty and the vehicle is a motorcycle ?
                {
                    parkingPlace[pos] = string.Concat(':',registrationNumber); // add a char of ':' at end of registration number string to mark it as a motorcycle
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
            VehicleType type = GetVehicleTypeOfParkedVehicle(parkingPlace, oldPosition, registrationNumber);
            Move(parkingPlace, registrationNumber, type, oldPosition, newPosition);
        }
    
    
        public static void Optimize(string[] parkingPlace)
        {
            bool found;
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
                    Console.WriteLine("Move motorcycle {0} from parkingplace {1} to place {2}.",registrationNumber, lastSingleMcPosition, firstSingleMcPosition);
                    Move(parkingPlace, registrationNumber, VehicleType.Mc, lastSingleMcPosition, firstSingleMcPosition);
                    found = true;
                }

                //  return -1 from search functions means that the search has reached the end or start of the array => exit optimize loop
            } while (found && (firstSingleMcPosition != -1 && lastSingleMcPosition != -1));
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
                    Console.WriteLine("Your Vehicle is found with registration number " + parkingPlace[i]);
                    return i;
                }else 
                {
                    // Try to find motorcycle
                    if (ParkingSlot.ContainsMc(parkingPlace[i], registrationNumber)) { 
                            Console.WriteLine("Your Vehicle is found with registration number " + parkingPlace[i]);
                            return i;
                        
                    }
                }
            }
            Console.WriteLine("Your Vehicle is not found with registration number " + registrationNumber);

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

        public static void Remove(string[] parkingPlace, string registrationNumber)
        {
            bool found = false;
            for (int i = 0; i < parkingPlace.Length; i++)
            {
                if (ParkingSlot.ContainsVehicle(parkingPlace[i],registrationNumber))
                {
                    found = true;
                    ParkingSlot.RemoveVehicle(ref parkingPlace[i], registrationNumber);
                    Console.WriteLine("The Vehicle with registration number " + registrationNumber + " successfully removed.");
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("The Vehicle with this number " + registrationNumber + " Not found. ");
                throw new VehicleNotFoundException("The vehicle " + registrationNumber + " you are trying to remove can not be found in the parkingplace");
            }
        }
    }
}
