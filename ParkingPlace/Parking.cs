using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParkingPlace
{
    public class Parking
    {
        public static int Park(string regnr)
        {
            return -1;
        }
        public static void Move(string[] parkingPlace, string registrationNumber, int newPosition)
        {
            
        }
        public static void Move(string[] parkingPlace, string registrationNumber, int oldPosition,int newPosition)
        {

        }
        public static int Find(string[] parkingPlace, string registrationNumber)
        {
            
            for (int i=0; i < parkingPlace.Length; i++) {
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
            return -1;
        }

        public static void Remove(string[] parkingPlace, string registrationNumber)
        {

        }
    }
}
