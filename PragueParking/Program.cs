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

        static void DisplayMenu()
        {

        }

        public static void Main(string[] args)
        {
            //Main file

            // String array with 100 elements of parking Temp
            string[] parkingPlace = new string[100];

            //add vehicle temporary
            String vehicle = "asb123";
            // Calling the find method 
            //parkingPlace[10] = "asb123";
            int pos;
            //pos=Parking.Find(parkingPlace, vehicle);
            //pos = Parking.Find(parkingPlace, vehicle);
            // Remove 
            Parking.Remove(parkingPlace, vehicle);

            //Console.WriteLine("Your pos is :- " + pos);
        }
    }
}
