using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
    public enum VehicleType
    {
        Car,
        Mc
    }
    public const int NumberOfParkingPlaces= 100;
    string[] parkingarr = new string[NumberOfParkingPlaces];
    string regnr = "abc123";
    VehicleType type = VehicleType.Car;
    //place= Park(parkingarr,regnr,type)

    public int Park(string[] parkingPlaces,string registrationNumber, VehicleType vehicleType)
    {

    }
}
