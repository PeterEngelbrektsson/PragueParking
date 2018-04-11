﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace PaarkingPlaceFunctionTest
{
    [TestClass]
  
    public class ParkingFindFreePlaceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceFullException))]
        public void FindFreePlaceAllFullCarCarTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[1];
            park[0] = "abc123";
            int actualPlace = -1;

            //Act
            actualPlace = Parking.FindFreePlace(park, "bcd987", VehicleType.Car); // Should throw exception


        }

        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceFullException))]
        public void FindFreePlaceAllFullCarMcTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[1];
            park[0] = "abc123";
            int actualPlace = -1;

            //Act
            actualPlace = Parking.FindFreePlace(park, "bcd987", VehicleType.Mc); // Should throw exception
        }
        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceFullException))]
        public void FindFreePlaceAllFull2McMcTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[1];
            park[0] = "abc123:uyt345";
            int actualPlace = -1;

            //Act
            actualPlace = Parking.FindFreePlace(park, "bcd987:1poi43", VehicleType.Mc); // Should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceFullException))]
        public void FindFreePlaceAllFull4McMcTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = "dbc423:uto765";
            int actualPlace = -1;

            //Act
            actualPlace = Parking.FindFreePlace(park, "1poi43", VehicleType.Mc); // Should throw exception
        }
        [TestMethod]
        public void FindFreePlace3McMcTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = ":dbc423";
            int actualPlace = -1;
            int expected = 1;

            //Act
            actualPlace = Parking.FindFreePlace(park, "1poi43", VehicleType.Mc);

            //Verify
            Assert.AreEqual(actualPlace, expected);


        }
        [TestMethod]
        public void FindFreePlace1Mc1CarMcTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123";
            park[1] = "dbc423:";
            int actualPlace = -1;
            int expected = 1;

            //Act
            actualPlace = Parking.FindFreePlace(park, "8toi43", VehicleType.Mc);

            //Verify
            Assert.AreEqual(actualPlace, expected);

        }
        [TestMethod]
        public void FindFreePlace1McCarTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "";
            park[1] = "dbc423:";
            int actualPlace = -1;
            int expected = 0;

            //Act
            actualPlace = Parking.FindFreePlace(park, "8toi43", VehicleType.Mc);
            
            //Verify
            Assert.AreEqual(actualPlace, expected);
        }

        [TestMethod]
        public void FindFreePlace1McCarNullTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = null;
            park[1] = "dbc423:";

            int actualPlace = -1;
            int expected = 0;

            //Act
            actualPlace = Parking.FindFreePlace(park, "8toi43", VehicleType.Car);

            //Verify
            Assert.AreEqual(actualPlace,expected);
        }
    }
}
