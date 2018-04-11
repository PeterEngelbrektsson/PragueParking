using System;
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

            //Act
            Parking.FindFreePlace(park, "bcd987", VehicleType.Car); // Should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceFullException))]
        public void FindFreePlaceAllFullCarMcTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[1];
            park[0] = "abc123";

            //Act
            Parking.FindFreePlace(park, "bcd987", VehicleType.Mc); // Should throw exception
        }
        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceFullException))]
        public void FindFreePlaceAllFull2McMcTest()
        {
            // Should throw exception

            // Setup
            string[] park = new string[1];
            park[0] = "abc123:uyt345";

            //Act
            Parking.FindFreePlace(park, "bcd987:1poi43", VehicleType.Mc); // Should throw exception
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

            //Act
            Parking.FindFreePlace(park, "1poi43", VehicleType.Mc); // Should throw exception
        }
        [TestMethod]
        public void FindFreePlace3McMcTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123:uyt345";
            park[1] = "dbc423:";
            string[] expected = new string[2];
            expected[0] = "abc123:uyt345";
            expected[1] = "dbc423:1poi43";

            //Act
            Parking.FindFreePlace(park, "1poi43", VehicleType.Mc); 
            MyAssert.AreEqual(expected, park);
        }
        [TestMethod]
        public void FindFreePlace1Mc1CarMcTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "abc123";
            park[1] = "dbc423:";
            string[] expected = new string[2];
            expected[0] = "abc123:uyt345";
            expected[1] = "dbc423:8toi43";

            //Act
            Parking.FindFreePlace(park, "8toi43", VehicleType.Mc);
            MyAssert.AreEqual(expected, park);
        }
        [TestMethod]
        public void FindFreePlace1McCarTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = "";
            park[1] = "dbc423:";
            string[] expected = new string[2];
            expected[0] = "8toi43";
            expected[1] = "dbc423:";

            //Act
            Parking.FindFreePlace(park, "8toi43", VehicleType.Mc);
            MyAssert.AreEqual(expected, park);
        }

        [TestMethod]
        public void FindFreePlace1McCarNullTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = null;
            park[1] = "dbc423:";

            string[] expected = new string[2];
            expected[0] = "8toi43";
            expected[1] = "dbc423:lk433";

            //Act
            Parking.Park(park, "8toi43", VehicleType.Mc);

            //Verify
            MyAssert.AreEqual(expected, park);
        }
    }
}
