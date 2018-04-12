using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParkingPlace
{
    public class Parking
    {

        public static void DisplayMenu(string[] parkingPlace, VehicleType vehicleType)
        {
            // Console.Clear(); -- Do we want to clear screen between repeat displays of the menu or not ? 

            Console.WriteLine("  Prague Parking v1.0");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1. Add a car"); 
            Console.WriteLine("2. Add a motorcyle");
            Console.WriteLine("3. Move a vehicle");
            Console.WriteLine("4. Find a vehicle");
            Console.WriteLine("5. Remove a vehicle");
            Console.WriteLine("6. Find free place");
            Console.WriteLine("7. Optimize parking lot");

            int choice = int.Parse(Console.ReadLine());

            string registrationNumber = "ABC123";

            switch (choice)
            {             
                case 1: // Add a car

                    Console.WriteLine("Please enter the registration number of the vehicle : ");

                    registrationNumber = Console.ReadLine();

                    vehicleType = VehicleType.Car;

                    Add(parkingPlace, registrationNumber, vehicleType);

                    break;

                case 2: // Add a motorcycle

                    Console.WriteLine("Please enter the registration number of the vehicle : ");

                    registrationNumber = Console.ReadLine();
                    
                    vehicleType = VehicleType.Mc;

                    Add(parkingPlace, registrationNumber, vehicleType);

                    break;

                case 3: // Move a vehicle

                    int newPosition = FindFreePlace(parkingPlace, registrationNumber, vehicleType );

                    Move(parkingPlace, registrationNumber, newPosition);

                    break;

                case 4: // Find a vehicle

                    Console.WriteLine("Please enter the registration number of the vehicle : ");

                    registrationNumber = Console.ReadLine();

                    int position = Find(parkingPlace, registrationNumber);

                    Console.WriteLine("Your vehicle is parked at spot number {0}, at array value {1}.", position + 1, position); // Parking spots numbered 1 - 100 !

                    break;

                case 5: // Remove a vehicle

                    Console.WriteLine("Please enter the registration number of the vehicle : ");

                    registrationNumber = Console.ReadLine();

                    Console.WriteLine("Please specify if your vehicle is a car or an mc : ");

                    string isCarOrMc = Console.ReadLine();

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
                        throw new ArgumentException(); // Neither nor, then throw exception !
                    }

                    break;

                case 6: // Find free parking spot

                    Console.WriteLine("Please specify if your vehicle is a car or an mc : ");

                    isCarOrMc = Console.ReadLine();

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

                    position = FindFreePlace(parkingPlace, registrationNumber, vehicleType);

                    Console.WriteLine("There is a free place for your vehicle at location {0}.", position + 1);

                    break;

                case 7: // Optimize parking spot

                    Optimize(parkingPlace);

                    break;

                default: // None of the above, throw exception !

                    throw new ArgumentException();

                    break;
            }
        }
        
        public static int Add(string [] parkingPlace, string registrationNumber, VehicleType vehicleType)
        {
                int pos = FindFreePlace(parkingPlace, registrationNumber, vehicleType);

                if ((parkingPlace[pos] != null) && vehicleType == VehicleType.Mc)
                {
                    parkingPlace[pos] = string.Concat(parkingPlace[pos], registrationNumber);
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
                    Move(parkingPlace, registrationNumber, VehicleType.Mc, lastSingleMcPosition, firstSingleMcPosition);
                    found = true;
                }

                //  return -1 from search functions means that the search has reached the end or start of the array => exit optimize loop
            } while (found && (firstSingleMcPosition != -1 && lastSingleMcPosition != -1));
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
                    // Remove(registrationNumner) // Fixme
                    throw new NotImplementedException();

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
            for (int i = 0; i < parkingPlace.Length; i++)
            {
                if (parkingPlace[i] == registrationNumber)
                {

                    parkingPlace.Where(w => w != parkingPlace[i]).ToArray();
                    Console.WriteLine("The Vehicle with registration number " + registrationNumber + " successfully removed.");
                    return;
                }
            }
            Console.WriteLine("The Vehicle with this number " + registrationNumber + " Not found. ");

        }
    }
}
