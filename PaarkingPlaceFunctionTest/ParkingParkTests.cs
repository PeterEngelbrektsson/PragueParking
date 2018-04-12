using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace PaarkingPlaceFunctionTest
{
    [TestClass]
    public class ParkingParkTests
    {
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
            Parking.Add(park, "bcd987:1poi43", VehicleType.Mc); // Should throw exception
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
            park[1] = "dbc423:";
            string[] expected = new string[2];
            expected[0] = "abc123:uyt345";
            expected[1] = "dbc423:1poi43";

            //Act
            Parking.Add(park, "1poi43", VehicleType.Mc);
            MyAssert.AreEqual(expected, park);
        }
        [TestMethod]
        public void Park1Mc1CarMcTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = "dbc423:";
            string[] expected = new string[2];
            expected[0] = "abc123:uyt345";
            expected[1] = "dbc423:8toi43";

            //Act
            Parking.Add(park, "8toi43", VehicleType.Mc);
            MyAssert.AreEqual(expected, park);
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

            //Act
            Parking.Add(park, "8toi43", VehicleType.Car);

            //Verify
            MyAssert.AreEqual(expected, park);
        }

    }
}
