using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace ParkingPlaceFunctionTest
{

    [TestClass]
    public class ParkingParkTests
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
        [ExpectedException(typeof(ParkingPlaceFullException))]
        public void ParkAllFullCarCarTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[1];
            park[0] = "abc123";

            //Act
            Parking.Add(park, "bcd987", VehicleType.Car); // Should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceFullException))]
        public void ParkAllFullCarMcTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[1];
            park[0] = "abc123";

            //Act
            Parking.Add(park, "bcd987", VehicleType.Mc); // Should throw exception
        }
        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceFullException))]
        public void ParkAllFull2McMcTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[1];
            park[0] = "abc123:uyt345";

            //Act
            Parking.Add(park, "bcd987", VehicleType.Mc); // Should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceFullException))]
        public void ParkAllFull4McMcTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = "dbc423:uto765";

            //Act
            Parking.Add(park, "1poi43", VehicleType.Mc); // Should throw exception
        }
        [TestMethod]
        public void Park3McMcTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = ":dbc423";
            string[] expected = new string[2];
            expected[0] = "abc123:uyt345";
            expected[1] = "1poi43:dbc423";
            int expectedPosition = 1;
            int actualPosition;

            //Act
            actualPosition= Parking.Add(park, "1poi43", VehicleType.Mc);

            // Verify
            MyAssert.AreEqual(expected, park);
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void Park1Mc2McTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = null;
            string[] expected = new string[2];
            expected[0] = "abc123:uyt345";
            expected[1] = ":1poi43";
            int expectedPosition = 1;
            int actualPosition;

            //Act
            actualPosition = Parking.Add(park, "1poi43", VehicleType.Mc);

            // Verify
            MyAssert.AreEqual(expected, park);
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void Park1Mc1CarMcTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = ":dbc423";
            string[] expected = new string[2];
            expected[0] = "abc123:uyt345";
            expected[1] = "8toi43:dbc423";
            int expectedPosition = 1;
            int actualPosition;

            //Act
            actualPosition = Parking.Add(park, "8toi43", VehicleType.Mc);
            MyAssert.AreEqual(expected, park);
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void Park1Mc100VehiclesTest()
        {
            // Setup
            string[] park = PopulateParkingPlace(100);
            park[0] = "abc123:uyt345";
            park[1] = ":dbc423";
            string[] expected = PopulateParkingPlace(100);
            expected[0] = "abc123:uyt345";
            expected[1] = "8toi43:dbc423";
            int expectedPosition=1;
            int actualPosition;
            //Act
            actualPosition = Parking.Add(park, "8toi43", VehicleType.Mc);

            // Verify
            MyAssert.AreEqual(expected, park);
            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [TestMethod]
        public void Park1McCarNullTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = null;
            park[1] = "dbc423:lk433";

            string[] expected = new string[2];
            expected[0] = "8toi43";
            expected[1] = "dbc423:lk433";

            int expectedPosition = 0;
            int actualPosition;

            //Act
            actualPosition=Parking.Add(park, "8toi43", VehicleType.Car);

            //Verify
            MyAssert.AreEqual(expected, park);
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        [ExpectedException(typeof(RegistrationNumberAlreadyExistException))]
        public void ParkDuplicateRegistrationNumberThrowsExceptionTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = "dbc423:uto765";

            //Act
            Parking.Add(park, "uto765", VehicleType.Mc); // Should throw exception
        }
    }
}
