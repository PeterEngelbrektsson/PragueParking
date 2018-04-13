using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingPlace;


namespace PragueParking
{
    public class Program
    {
        public const int NumberOfParkinPlaces = 100;
        public const int MaxLengthOfRegistrationNumber = 10;


        public static void WriteMenu()
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

        }
        public static void DisplayMenu(string[] parkingPlace, VehicleType vehicleType)
        {
            // Console.Clear(); -- Do we want to clear screen between repeat displays of the menu or not ? 

            bool keepLoop = true;
            int choice = 0;

            while (keepLoop) // Perpetual loop
            {
                WriteMenu();

                String Str = Console.ReadLine(); // Store user choice
                choice = 0;
                //int choice = int.Parse(Console.ReadLine()); // Store user choice                
                if (!int.TryParse(Str, out choice))
                {
                    Messager.WriteErrorMessage("Invalid Input, Please enter number only");
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

                            if (registrationNumber.Length > MaxLengthOfRegistrationNumber)
                            {
                                Messager.WriteErrorMessage("The registration number is too long.");
                                break;
                            }
                            vehicleType = VehicleType.Car; // Set vehicle type to car                       

                            try
                            {
                                position = Parking.Add(parkingPlace, registrationNumber, vehicleType); // Park at suitable position (if any)
                                Messager.WriteInformationMessage(String.Format("Your vehicle has been parked at place number {0}.", position + 1));
                            }

                            catch (RegistrationNumberAlreadyExistException)
                            {
                                Messager.WriteErrorMessage("Registration number already exist. Cannot have two vehicles with same.");
                            }

                            break;

                        case 2: // Add a motorcycle

                            Console.WriteLine("Please enter the registration number of the vehicle : ");
                            registrationNumber = Console.ReadLine().ToUpper();

                            if (registrationNumber.Length > MaxLengthOfRegistrationNumber)
                            {
                                Messager.WriteErrorMessage("The registration number is too long.");
                                break;
                            }

                            vehicleType = VehicleType.Mc; // Set vehicle type to motorcycle

                            try
                            {
                                position = Parking.Add(parkingPlace, registrationNumber, vehicleType); // Park at suitable position (if any)
                                Messager.WriteInformationMessage(String.Format("Your vehicle has been parked at place number {0}.", position + 1));
                            }

                            catch (RegistrationNumberAlreadyExistException)
                            {
                                Messager.WriteErrorMessage("Registration number already exist. Cannot hav two vehicles with same.");
                            }

                            break;

                        case 3: // Move a vehicle

                            int newPosition = Parking.FindFreePlace(parkingPlace, vehicleType); // Original position of the vehicle
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Suggest parking position for your vehicle will be {0}", newPosition + 1); // zero to one based index
                            Console.Write("Do you accept this ? Please choose YES or NO. : ");
                            Console.ForegroundColor = ConsoleColor.White;

                            string yesOrNo = Console.ReadLine().ToUpper();

                            if (yesOrNo == "YES")
                            {
                                // Move vehicle to new position
                                try
                                {
                                    Parking.Move(parkingPlace, registrationNumber.ToUpper(), newPosition);// convert form one based to zerop based index
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
                                    Parking.Move(parkingPlace, registrationNumber.ToUpper(), userPosition - 1);// convert form one based to zerop based index
                                }
                                catch (VehicleNotFoundException)
                                {
                                    Messager.WriteErrorMessage("The vehicle could not be found.");
                                }
                                catch (ParkingPlaceOccupiedException)
                                {
                                    Messager.WriteErrorMessage("The selected new position is already full.");
                                }
                            }
                            else
                            {
                                Messager.WriteErrorMessage("You have to make a proper choice.");
                            }

                            break;

                        case 4: // Find a vehicle

                            Console.WriteLine("Please enter the registration number of the vehicle : ");
                            registrationNumber = Console.ReadLine().ToUpper();

                            position = Parking.Find(parkingPlace, registrationNumber); // Position where vehicle is located (if any)

                            if (position != -1)
                            {
                                Messager.WriteInformationMessage(String.Format("Your vehicle is parked at spot number {0}.", position + 1)); // Parking spots numbered 1 - 100 !
                            }

                            else
                            {
                                Messager.WriteErrorMessage("I am sorry to say you vehicle does not exist in our parking lot.");
                                Messager.WriteErrorMessage("Perhaps someone has taken it for a joyride. Our apologies.");
                            }

                            break;

                        case 5: // Remove a vehicle

                            Console.WriteLine("Please enter the registration number of the vehicle : ");
                            registrationNumber = Console.ReadLine().ToUpper();

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
                                Messager.WriteErrorMessage("Choose either car or mc. Other vehicles not allowed in the parking lot."); // Neither car nor mc, throw exception !
                                break;
                            }

                            position = Parking.FindFreePlace(parkingPlace, vehicleType); // Find a free position for car or mc, depending on user choice
                            Messager.WriteInformationMessage(String.Format("There is a free place for your vehicle at {0}.", position + 1));
                            break;

                        case 7: // Optimize parking spot

                            Optimize(parkingPlace); // Optimize the parking place
                            break;

                        case 8: // List all vehicles in parking lot

                            DisplayParkedVehicels(parkingPlace);
                            break;

                        default: // None of the above

                            Console.WriteLine();
                            Messager.WriteErrorMessage("That number does not exist. Please enter a correct number.");
                            break;
                    }
                }
            }
        }
        public static void DisplayParkedVehicels(string[] parkingPlace)
        {
            Dictionary<int, string> parkedVehicles;
            parkedVehicles = Parking.ListParkedVehicels(parkingPlace);
            if (parkedVehicles != null && parkedVehicles.Count > 0)
            {
                foreach (KeyValuePair<int, string> slot in parkedVehicles)
                {
                    Console.WriteLine("{0} {1} ", slot.Key + 1, slot.Value); // Display should be 1 based
                }
            }
            else
            {
                Messager.WriteInformationMessage("The parkingplace is empty.");
            }
        }
        public static void Optimize(string[] parkingPlace)
        {
            string[] messages;
            messages = Parking.Optimize(parkingPlace);

            foreach (string message in messages)
            {
                Messager.WriteInformationMessage(message);
            }
            if (messages.Length < 1)
            {
                Messager.WriteInformationMessage("The parkingplace is alreadey optimized.");
            }
        }
        public static void Remove(string[] parkingPlace, string registrationNumber)
        {

            try
            {
                int pos = Parking.Remove(parkingPlace, registrationNumber);
                Messager.WriteInformationMessage(String.Format("The Vehicle with registration number {0} successfully removed from position ", registrationNumber, pos + 1)); // Display of parking number should be one based
            }
            catch (VehicleNotFoundException)
            {

                Messager.WriteErrorMessage("The Vehicle with this number " + registrationNumber + " Not found. ");
                Messager.WriteErrorMessage("The vehicle " + registrationNumber + " you are trying to remove can not be found in the parkingplace");

            }
        }
        public static void Main(string[] args)
        {
            //Main file

            // String array with 100 elements of parking Temp
             string[] parkingPlace = new string[100];
  
            // Testdata
            parkingPlace[10] = "ABC123";
            parkingPlace[21] = ":MC3";
            parkingPlace[22] = ":OIU988";
            parkingPlace[24] = ":MC1";
            parkingPlace[45] = "MC4:MC2";
            parkingPlace[54] = ":MC5";
            parkingPlace[55] = ":MC6";


            DisplayMenu(parkingPlace,VehicleType.Car);
            Console.ReadLine();
        }
    }
}
