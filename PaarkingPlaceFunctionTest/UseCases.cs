﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace ParkingPlaceFunctionTest
{
    [TestClass]
    public class UseCases
    {
        public string[] PopulateParkingPlace(int size)
        {
            string[] parkingPlaces = new string[size];
            for (int i = 0; i < parkingPlaces.Length; i++)
            {
                parkingPlaces[i] = "abc" + 100 + i;
            }
            return parkingPlaces;
        }

        [TestMethod]
        public void UseCase1Tests()
        {
            // Setup
            string[] park = new string[100];
            int actualFindPosition, actualFindPositionMc;
            int actualFindFreePositionMc;

            // Act
            Parking.Add(park, "bcd987", VehicleType.Car);           // Park a car
            actualFindPosition=Parking.FindDistinct(park, "bcd987");        // Find the car
            Parking.Remove(park,"bcd987");                          // remove the car
            Parking.Add(park, "afgd987", VehicleType.Car);          // park car 2
            Parking.Add(park, "ytrd987", VehicleType.Mc);           // park Mc 1
            Parking.Add(park, "aoi987", VehicleType.Car);           // park car 3
            Parking.Add(park, "ykad987", VehicleType.Mc);           // park Mc 2

            actualFindPositionMc = Parking.FindDistinct(park, "ykad987");                   // find  mc 2
            actualFindFreePositionMc = Parking.FindFreePlace(park, VehicleType.Mc);   // find free position
            Parking.Move(park, "ykad987", actualFindFreePositionMc);                    // move mc 2 to free position
            Parking.Remove(park, "ytrd987");                                            // remove mc 1
            Parking.Add(park, "947tef", VehicleType.Mc);                // park Mc 3
            Parking.Add(park, "37hjd", VehicleType.Mc);                 // park Mc 4

            Parking.Optimize(park);                                         // Optimize

            // Verify
            Assert.IsTrue(actualFindPosition >= 0 && actualFindPosition < park.Length);
            Assert.IsTrue(actualFindPositionMc >= 0 && actualFindPositionMc < park.Length);
            Assert.IsTrue(actualFindFreePositionMc >= 0 && actualFindFreePositionMc < park.Length);
        }
    }
}
