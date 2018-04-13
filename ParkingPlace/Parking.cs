using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParkingPlace
{
    public partial class Parking // Parking place
    {

        public static void DisplayMenu(string[] parkingPlace, VehicleType vehicleType)
        {
            // Console.Clear(); -- Do we want to clear screen between repeat displays of the menu or not ? 

            bool keepLoop = true;
            int choice = 0;

            while (keepLoop) // Perpetual loop
            {
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
                Console.WriteLine("8. Display all parked vehicles");
                Console.WriteLine("0. EXIT");
                Console.WriteLine();
                Console.Write("Please input number : ");

                String Str = Console.ReadLine(); // Store user choice
                choice = 0;
                //int choice = int.Parse(Console.ReadLine()); // Store user choice                
                if (!int.TryParse(Str, out choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input, Please enter number only");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {


                    int position = 0; // Position in array of vehicles                

                    string registrationNumber = "ABC123"; // pseudo registration number

                    string isCarOrMc = "";

                    switch (choice) // Check user choice
                    {
                        case 0: // Leave menu permanently.

                            keepLoop = false;
                            break;

                        case 1: // Add a car

                            Console.WriteLine("Please enter the registration number of the vehicle : ");
                            registrationNumber = Console.ReadLine().ToUpper();

                            vehicleType = VehicleType.Car; // Set vehicle type to car                       

                            try
                            {
                                position = Add(parkingPlace, registrationNumber, vehicleType); // Park at suitable position (if any)
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Your vehicle has been parked at place number {0}.", position + 1);
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            catch (RegistrationNumberAlreadyExistException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Registration number already exist. Cannot have two vehicles with same.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            break;

                        case 2: // Add a motorcycle

                            Console.WriteLine("Please enter the registration number of the vehicle : ");
                            registrationNumber = Console.ReadLine().ToUpper();

                            vehicleType = VehicleType.Mc; // Set vehicle type to motorcycle

                            try
                            {
                                position = Add(parkingPlace, registrationNumber, vehicleType); // Park at suitable position (if any)
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Your vehicle has been parked at place number {0}.", position + 1);
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            catch (RegistrationNumberAlreadyExistException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Registration number already exist. Cannot hav two vehicles with same.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            break;

                        case 3: // Move a vehicle

                            int newPosition = FindFreePlace(parkingPlace, vehicleType); // Original position of the vehicle
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Suggest parking position for your vehicle will be {0}", newPosition+1); // zero to one based index
                            Console.Write("Do you accept this ? Please choose YES or NO. : ");
                            Console.ForegroundColor = ConsoleColor.White;

                            string yesOrNo = Console.ReadLine().ToUpper();

                        if (yesOrNo == "YES")
                        {
                            // Move vehicle to new position
                            try
                            {
                                Move(parkingPlace, registrationNumber.ToUpper(), newPosition);// convert form one based to zerop based index
                            }
                            catch (VehicleNotFoundException)
                            {
                                Console.WriteLine("The vehicle could not be found.");
                            }

                        }

                        else if (yesOrNo == "NO")
                        {
                            Console.WriteLine("OK, lets try finding another parking place that is suitable for you");
                            Console.Write("Please choose a parking place and we shall see if it is available : ");
                            int userPosition = int.Parse(Console.ReadLine());
                            try
                            {
                                Move(parkingPlace, registrationNumber.ToUpper(), userPosition-1);// convert form one based to zerop based index
                            }
                            catch (VehicleNotFoundException)
                            {
                                Console.WriteLine("The vehicle could not be found.");
                            }
                            catch (ParkingPlaceOccupiedException)
                            { 
                                Console.WriteLine("The selected new position is already full.");
                            }
                        }
                        else
                        {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("You have to make a proper choice.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            break;

                        case 4: // Find a vehicle

                            Console.WriteLine("Please enter the registration number of the vehicle : ");
                            registrationNumber = Console.ReadLine().ToUpper();

                            position = Find(parkingPlace, registrationNumber); // Position where vehicle is located (if any)

                            if (position != -1)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Your vehicle is parked at spot number {0}.", position + 1); // Parking spots numbered 1 - 100 !
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("I am sorry to say you vehicle does not exist in our parking lot.");
                                Console.WriteLine("Perhaps someone has taken it for a joyride. Our apologies.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            break;

                        case 5: // Remove a vehicle

                            Console.WriteLine("Please enter the registration number of the vehicle : ");
                            registrationNumber = Console.ReadLine().ToUpper();

                            Remove(parkingPlace, registrationNumber); // Remove the vehicle with the specificed registration number (if it exists in the parking lot)
                            break;

                        case 6: // Find free parking spot
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please specify if your vehicle is a car or an mc : ");
                            Console.ForegroundColor = ConsoleColor.White;

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
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Choose either car or mc. Other vehicles not allowed in the parking lot."); // Neither car nor mc, throw exception !
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            }

                            position = FindFreePlace(parkingPlace, vehicleType); // Find a free position for car or mc, depending on user choice
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("There is a free place for your vehicle at {0}.", position + 1);
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case 7: // Optimize parking spot

                            Optimize(parkingPlace); // Optimize the parking place
                            break;

                        case 8: // List all vehicles in parking lot

                            DisplayParkedVehicels(parkingPlace);
                            break;

                        default: // None of the above

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("That number does not exist. Please enter a correct number.");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
            }
        }
        public static void DisplayParkedVehicels(string[] parkingPlace)
        {
            Dictionary<int,string> parkedVehicles;
            parkedVehicles = ListParkedVehicels(parkingPlace);
            if (parkedVehicles != null && parkedVehicles.Count > 0)
            {
                foreach (KeyValuePair<int, string> slot in parkedVehicles)
                {
                    Console.WriteLine("{0} {1} ", slot.Key+1, slot.Value); // Display should be 1 based
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The parkingplace is empty.");
                Console.ForegroundColor = ConsoleColor.White;
            }
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
            VehicleType type = GetVehicleTypeOfParkedVehicle(parkingPlace, oldPosition, registrationNumber);
            Move(parkingPlace, registrationNumber, type, oldPosition, newPosition);
        }    
    
        public static void Optimize(string[] parkingPlace)
        {
            string[] messages;
            messages = doOptimize(parkingPlace);

            foreach (string message in messages)
            {
                Console.WriteLine(message);
            }
            if (messages.Length < 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The parkingplace is alreadey optimized.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static string[] doOptimize(string[] parkingPlace)
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
                   // Console.WriteLine("Your Vehicle is found with registration number " + parkingPlace[i]);
                    return i;
                }else 
                {
                    // Try to find motorcycle
                    if (ParkingSlot.ContainsMc(parkingPlace[i], registrationNumber)) { 
                           // Console.WriteLine("Your Vehicle is found with registration number " + parkingPlace[i]);
                            return i;
                        
                    }
                }
            }
            //Console.WriteLine("Your Vehicle is not found with registration number " + registrationNumber);

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
            
            try
            {
                int pos=doRemove(parkingPlace, registrationNumber);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The Vehicle with registration number " + registrationNumber + " successfully removed from position " + (pos + 1)); // Display of parking number should be one based
                Console.ForegroundColor = ConsoleColor.White;

            }
            catch (VehicleNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The Vehicle with this number " + registrationNumber + " Not found. ");
                Console.WriteLine("The vehicle " + registrationNumber + " you are trying to remove can not be found in the parkingplace");
                Console.ForegroundColor = ConsoleColor.White;

            }
        }
        public static int doRemove(string[] parkingPlace, string registrationNumber)
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
