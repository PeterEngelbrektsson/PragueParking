using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace ParkingPlaceFunctionTest
{
    [TestClass]
    public class ParkingRemoveTests
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
        [ExpectedException(typeof(VehicleNotFoundException))]
        public void RemoveCarNotFound1CarThrowsExeptionTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[1];
            park[0] = "abc123";

            //Act
            Parking.doRemove(park, "bcd987"); // Should throw exception
        }


        [TestMethod]
        [ExpectedException(typeof(VehicleNotFoundException))]
        public void RemoveCarNotFound2McThrowsExeptionTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[1];
            park[0] = "abc123:uyt345";

            //Act
            Parking.doRemove(park, "bcd987"); // Should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(VehicleNotFoundException))]
        public void RemoveNotFound4McThrowsExeptionTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = "dbc423:uto765";

            //Act
            Parking.doRemove(park, "1poi43"); // Should throw exception
        }
        [TestMethod]
        public void RemoveMcRight3mcTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = "dbc423:1poi43";
            string[] expected = new string[2];
            expected[0] = "abc123:uyt345";
            expected[1] = ":dbc423";
            int expectedPos=1;
            int actualPos;

            //Act
            actualPos=Parking.doRemove(park, "1poi43");

            // Verify
            MyAssert.AreEqual(expected, park);
            Assert.AreEqual(expectedPos, actualPos);
        }
        [TestMethod]
        public void RemoveMcLeft3mcTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = "dbc423:8toi43";
            string[] expected = new string[2];
            expected[0] = "abc123:uyt345";
            expected[1] = ":8toi43";
            int expectedPos = 1;
            int actualPos;

            //Act
            actualPos= Parking.doRemove(park, "dbc423");

            // Verify
            MyAssert.AreEqual(expected, park);
            Assert.AreEqual(expectedPos, actualPos);
        }
        [TestMethod]
        public void RemoveCarTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = "dbc423";
            string[] expected = new string[2];
            expected[0] = "abc123:uyt345";
            expected[1] = null;
            string carToRemove = "dbc423";
            int expectedPos = 1;
            int actualPos;

            //Act
            actualPos = Parking.doRemove(park, carToRemove);

            // Verify
            MyAssert.AreEqual(expected, park);
            Assert.AreEqual(expectedPos, actualPos);
        }
        [TestMethod]
        public void RemoveMc100VehiclesTest()
        {
            // Setup
            string[] park = PopulateParkingPlace(100);
            park[0] = "abc123:uyt345";
            park[1] = "dbc423:8toi43";
            string[] expected = PopulateParkingPlace(100);
            expected[0] = "abc123:uyt345";
            expected[1] = ":dbc423";
            int expectedPos = 1;
            int actualPos;

            //Act
            actualPos=Parking.doRemove(park, "8toi43");

            // Verify
            MyAssert.AreEqual(expected, park);
            Assert.AreEqual(expectedPos, actualPos);
        }

        [TestMethod]
        public void RemoveCar100VehiclesTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "8toi43";
            park[1] = "dbc423:lk433";

            string[] expected = new string[2];
            expected[0] = null;
            expected[1] = "dbc423:lk433";
            int expectedPos = 0;
            int actualPos;

            //Act
            actualPos=Parking.doRemove(park, "8toi43");

            //Verify
            MyAssert.AreEqual(expected, park);
            Assert.AreEqual(expectedPos, actualPos);
        }

    }

}
