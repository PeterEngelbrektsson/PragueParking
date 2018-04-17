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
        public const string ParkingPlaceFileName = "ParkinPlace1_1.bin";
        
        /// <summary>
        /// Writes the main menu 
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void WriteMenu(string[] parkingPlace)
        {
            Console.WriteLine();
            Console.WriteLine("  Prague Parking v1.0");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1. Add a car");
            Console.WriteLine("2. Add a motorcycle");
            Console.WriteLine("3. Move a vehicle");
            Console.WriteLine("4. Find a vehicle");
            Console.WriteLine("5. Remove a vehicle");
            Console.WriteLine("6. Find free place");
            Console.WriteLine("7. Optimize parking lot");
            Console.WriteLine("8. Display all parked vehicles");
            Console.WriteLine("9. Display statistics");
            Console.WriteLine("10. Save");
            Console.WriteLine("11. Load");
            Console.WriteLine("0. EXIT");
            DisplayIfCanBeOptimized(parkingPlace);
            Console.WriteLine();
            Console.Write("Please input number : ");

        }

        /// <summary>
        /// Display a message if the park can be optimized.
        /// </summary>
        /// <param name="parkingPlace">The parking place</param>
        public static void DisplayIfCanBeOptimized(string[] parkingPlace)
        {
            int singleMcs = Parking.NumberOfSingleParkedMcs(parkingPlace);
            if (singleMcs > 1)
            {
                Console.WriteLine();
                Messenger.WriteInformationMessage(String.Format("The parkingspace can be optimized. There are {0} single parked motorcycles.", singleMcs));
            }
        }
        /// <summary>
        /// Displays statistics about the parking place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void DisplayStatistics(string[] parkingPlace)
        {
            int singleMcs = Parking.NumberOfSingleParkedMcs(parkingPlace);
            int fullParkingPlaces = Parking.NumberOfFullParkingPlaces(parkingPlace);
            int freeParkingPlacesCar = Parking.NumberOfFreeParkingPlaces(parkingPlace,VehicleType.Car);
            int freeParkingPlacesMc = Parking.NumberOfFreeParkingPlaces(parkingPlace, VehicleType.Mc);
            Console.WriteLine();
            Messenger.WriteInformationMessage(String.Format("The number of free parking places for cars {0}.", freeParkingPlacesCar));
            Messenger.WriteInformationMessage(String.Format("The number of free parking places for motorcycles {0}.", freeParkingPlacesMc));
            Messenger.WriteInformationMessage(String.Format("The number of full parking places {0}.", fullParkingPlaces));
            Messenger.WriteInformationMessage(String.Format("The number of single parked motorcycles {0}.", singleMcs));
        }
        /// <summary>
        /// Display the menu bar.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void DisplayMenu(ref string[] parkingPlace)
        {
            // Console.Clear(); -- Do we want to clear screen between repeat displays of the menu or not ? 
            bool keepLoop = true;
            int choice = 0;

            while (keepLoop) // Perpetual loop
            {
                WriteMenu(parkingPlace);

                String Str = Console.ReadLine(); // Store user choice
                choice = 0;
                //int choice = int.Parse(Console.ReadLine()); // Store user choice                
                if (!int.TryParse(Str, out choice))
                {
                    Messenger.WriteErrorMessage("Invalid Input, Please enter number only");
                }
                else
                {

                    switch (choice) // Check user choice
                    {
                        case 0: // Leave menu permanently.
                            keepLoop = false;
                            break;

                        case 1: // Add a car
                            AddCar(parkingPlace);
                            break;

                        case 2: // Add a motorcycle
                            AddMc(parkingPlace);
                            break;

                        case 3: // Move a vehicle
                            MoveVehicle(parkingPlace);
                            break;

                        case 4: // Find a vehicle
                            FindVehicle(parkingPlace);
                            break;

                        case 5: // Remove a vehicle
                            RemoveVehicle(parkingPlace);
                            break;

                        case 6: // Find free parking spot
                            FindFreeSpot(parkingPlace);
                            break;

                        case 7: // Optimize parking spot
                            Optimize(parkingPlace); // Optimize the parking place
                            break;

                        case 8: // List all vehicles in parking lot
                            DisplayParkedVehicels(parkingPlace);
                            break;
                        case 9: //Display statistics
                            DisplayStatistics(parkingPlace);
                            break;
                        case 10: //Save
                            Parking.SaveToFile(parkingPlace,ParkingPlaceFileName);
                            Messenger.WriteInformationMessage("Database saved to file.");
                            break;
                        case 11: //Load
                            parkingPlace=Parking.LoadFromFile(ParkingPlaceFileName);
                            Messenger.WriteInformationMessage("Database loaded from file.");
                            break;
                        default: // None of the above

                            Console.WriteLine();
                            Messenger.WriteErrorMessage("That number does not exist. Please enter a correct number.");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Displays a list of all parked vehicles in the parking place.
        /// </summary>
        /// <param name="parkingPlace"></param>
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
                Messenger.WriteInformationMessage("The parkingplace is empty.");
            }
        }
        /// <summary>
        /// Optimizes the parking place. Moves single parked motorcycles together in the same slots.
        /// Displays a list of movements to be performed by the employees.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void Optimize(string[] parkingPlace)
        {
            string[] messages;
            messages = Parking.Optimize(parkingPlace);

            foreach (string message in messages)
            {
                Messenger.WriteInformationMessage(message);
            }
            if (messages.Length < 1)
            {
                Messenger.WriteInformationMessage("The parkingplace is alreadey optimized.");
            }
        }
        /// <summary>
        /// Removes a vehicle from the parking place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <param name="registrationNumber"></param>
        public static void Remove(string[] parkingPlace, string registrationNumber)
        {

            try
            {
                KeyValuePair<int, string> result;
                result = Parking.Remove(parkingPlace, registrationNumber);
                int pos = result.Key;
                string checkinTimeStamp = result.Value;
                if (result.Value != "")
                {
                    Messenger.WriteInformationMessage(String.Format("The Vehicle with registration number {0} successfully removed from position {1}. Cheked in {2}", registrationNumber, pos + 1,checkinTimeStamp)); // Display of parking number should be one based
                }
                else
                {
                    Messenger.WriteInformationMessage(String.Format("The Vehicle with registration number {0} successfully removed from position {1}", registrationNumber, pos + 1)); // Display of parking number should be one based
                }
                
            }
            catch (VehicleNotFoundException)
            {

                Messenger.WriteErrorMessage("The Vehicle with this number " + registrationNumber + " Not found. ");
                Messenger.WriteErrorMessage("The vehicle " + registrationNumber + " you are trying to remove can not be found in the parkingplace");

            }
        }
        /// <summary>
        /// Finding free parking place. 
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void FindFreeSpot(string[] parkingPlace)
        {
            Console.WriteLine("Please specify if your vehicle is a car or an mc : ");
            string isCarOrMc;
            VehicleType vehicleType;
            int position;

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
                Messenger.WriteErrorMessage("Choose either car or mc. Other vehicles not allowed in the parking lot."); // Neither car nor mc, throw exception !
                return;
            }

            position = Parking.FindFreePlace(parkingPlace, vehicleType); // Find a free position for car or mc, depending on user choice
            Messenger.WriteInformationMessage(String.Format("There is a free place for your vehicle at {0}.", position + 1));

        }
        /// <summary>
        /// Finding Vehicle
        /// </summary>
        /// <param name="parkingPlace"></param>
        static void FindVehicle(string[] parkingPlace)
        {
            Console.WriteLine("Please enter the registration number of the vehicle : ");
            string registrationNumber = Console.ReadLine().ToUpper();

            int position = Parking.FindDistinct(parkingPlace, registrationNumber); // Position where vehicle is located (if any)

            if (position != -1)
            {
                // The exact match found
                Messenger.WriteInformationMessage(String.Format("Your vehicle is parked at spot number {0}.", position + 1)); // Parking spots numbered 1 - 100 !
            }
            else
            {
                // No exact match found
                Dictionary<int,string> searchResult = Parking.FindSearchString(parkingPlace, registrationNumber);
                if (searchResult.Count > 0)
                {
                    foreach(KeyValuePair<int,string> vehicle in searchResult)
                    {
                        Console.WriteLine("{0} {1}", vehicle.Key + 1, vehicle.Value);
                    }
                }
                else { 
                    Messenger.WriteErrorMessage("I am sorry to say you vehicle does not exist in our parking lot.");
                    Messenger.WriteErrorMessage("Perhaps someone has taken it for a joyride. Our apologies.");
                }
            }
        }
        /// <summary>
        /// Moving Vehicle one place to another place.
        /// </summary>
        /// <param name="parkingPlace"></param>
        public static void MoveVehicle(string[] parkingPlace)
        {
            Console.Write("Enter the registration number: ");
            string registrationNumber = Console.ReadLine().ToUpper();
            int oldPosition = Parking.FindDistinct(parkingPlace, registrationNumber);
            if (oldPosition < 0)
            {
                Messenger.WriteErrorMessage("The vehicle could not be found.");
                return;
            }
            VehicleType vehicleType = Parking.GetVehicleTypeOfParkedVehicle(parkingPlace, oldPosition, registrationNumber);

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
                    Messenger.WriteInformationMessage("The vehicle has been moved.");
                }
                catch (VehicleNotFoundException)
                {
                    Messenger.WriteInformationMessage("The vehicle could not be found.");
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
                    Messenger.WriteInformationMessage("The vehicle has been moved.");
                }
                catch (VehicleNotFoundException)
                {
                    Messenger.WriteErrorMessage("The vehicle could not be found.");
                }
                catch (ParkingPlaceOccupiedException ex)
                {
                    Messenger.WriteErrorMessage("The selected new position is already full.");
                    Messenger.WriteErrorMessage(ex.Message);
                }
                catch (VehicleAlreadyAtThatPlaceException)
                {
                    Messenger.WriteErrorMessage("The vehicle is already parked at that position.");
                }
            }
            else
            {
                Messenger.WriteErrorMessage("You have to make a proper choice.");
            }

        }
        /// <summary>
        /// Prompts the user for a valid registration number or exit code 0
        /// </summary>
        /// <returns></returns>
        public static string PromptForRegistrationNumber()
        {
            bool loop = true;
            string registrationNumber=null;
            do
            {
                Console.WriteLine("Please enter the registration number of the vehicle or 0 to bort: ");
                registrationNumber = Console.ReadLine().ToUpper();
                int inputNumber = 0;
                if(int.TryParse(registrationNumber,out inputNumber)&& inputNumber == 0)
                {
                    registrationNumber = null;
                    loop = false;
                }
                else if (registrationNumber.Length > Parking.MaxLengthOfRegistrationNumber)
                {
                    Messenger.WriteErrorMessage("The registration number is too long.");
                }
                else if (!Parking.ValidRegistrationNumber(registrationNumber))
                {
                    Messenger.WriteErrorMessage("The registration number is not valid. Use A-Z 0-9");
                }
                else
                {
                    //valid registration number
                    loop = false;
                }
            }while(loop);
            return registrationNumber;
        }

        /// <summary>
        /// Park Vehicle
        /// </summary>
        /// <param name="parkingPlace"></param>
        /// <param name="registrationNumber"></param>
        /// <param name="vehicleType"></param>
        public static void ParkVehicle(string[] parkingPlace, string registrationNumber, VehicleType vehicleType)
        {
            try
            {
                int position = Parking.Add(parkingPlace, registrationNumber, vehicleType); // Park at suitable position (if any)
                Messenger.WriteInformationMessage(String.Format("Your vehicle has been parked at place number {0}.", position + 1));
            }
            catch (RegistrationNumberAlreadyExistException)
            {
                Messenger.WriteErrorMessage("Registration number already exist. Cannot have two vehicles with same.");
            }
            catch (ParkingPlaceFullException)
            {
                Messenger.WriteErrorMessage("The parking place has no room for the vehicel.");
            }
        }
            /// <summary>
            /// Add motocycle
            /// </summary>
            /// <param name="parkingPlace"></param>
        public static void AddMc(string[] parkingPlace)
        {
            string registrationNumber = PromptForRegistrationNumber();
            if (registrationNumber != null)
            {
                ParkVehicle(parkingPlace, registrationNumber, VehicleType.Mc);
            }
        }

        /// <summary>
        /// Add Car
        /// </summary>
        /// <param name="parkingPlace"></param>
        static void AddCar(string[] parkingPlace)
        {
            string registrationNumber = PromptForRegistrationNumber();
            if (registrationNumber != null) { 
                ParkVehicle(parkingPlace, registrationNumber, VehicleType.Car);
            }
        }
        /// <summary>
        /// Revome Vehicle
        /// </summary>
        /// <param name="parkingPlace"></param>
        static void RemoveVehicle(string[] parkingPlace)
        {
            string registrationNumber = PromptForRegistrationNumber();
            if (registrationNumber != null)
            {
                Remove(parkingPlace, registrationNumber); // Remove the vehicle with the specificed registration number (if it exists in the parking lot)
            }
        }
        /// <summary>
        /// Adding some test data
        /// </summary>
        /// <returns></returns>
        public static string[] PopulateTestData()
        {
            string[] parkingPlace = new string[100];
            // Testdata
            parkingPlace[10] = "ABC123" + "," + DateTime.Now;
            parkingPlace[12] = "CAR432"+","+DateTime.Now;
            parkingPlace[15] = "CUSTOMNME" + "," + DateTime.Now;
            parkingPlace[18] = "MYNAME" + "," + DateTime.Now;
            parkingPlace[21] = ":MC3";
            parkingPlace[22] = ":OIU988";
            parkingPlace[24] = ":MC1";
            parkingPlace[45] = "MC4:MC2";
            parkingPlace[54] = ":MC5";
            parkingPlace[55] = ":MC6";
            parkingPlace[85] = "CAR987" + "," + DateTime.Now;
            parkingPlace[86] = "CAR123" + "," + DateTime.Now;
            parkingPlace[88] = ":MC7";
            parkingPlace[99] = ":MC8";

            return parkingPlace;
        }
        /// <summary>
        /// Main 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //Main file

            // String array with elements of parking 
             string[] parkingPlace = new string[NumberOfParkinPlaces];

            // Setup demo with testdata.  FIXME remove this in production code.
            parkingPlace = PopulateTestData();

            DisplayMenu(ref parkingPlace);
            Console.ReadLine();
        }
    }
}
