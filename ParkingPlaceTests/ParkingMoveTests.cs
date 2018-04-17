using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace ParkingPlaceTests
{
    [TestClass]
    public class ParkingMoveTests
    {
        [TestMethod]
        [ExpectedException(typeof(ParkingSpaceIsEmptyException))]
        public void GetVehicleTypeOfParkedVehicleNullTest()
        {
            // Setup
            VehicleType expectedVehicleType = VehicleType.Car;
            VehicleType actualVehicleType;
            string registrationNumber = "abc123";

            string[] parkingPlace = new string[1];
            parkingPlace[0] = null;

            // Act
            actualVehicleType = Parking.GetVehicleTypeOfParkedVehicle(parkingPlace, 0,registrationNumber);

            // Verify
            Assert.AreEqual(expectedVehicleType,actualVehicleType); // Should throw exception
        }

        [TestMethod]
        public void GetVehicleTypeOfParkedVehicleCarTest()
        {
            // Setup
            VehicleType expectedVehicleType = VehicleType.Car;
            VehicleType actualVehicleType;
            string registrationNumber = "abc123";

            string[] parkingPlace = new string[1];
            parkingPlace[0] = "abc123,1999-01-02 13:34";

            // Act
            actualVehicleType = Parking.GetVehicleTypeOfParkedVehicle(parkingPlace, 0,registrationNumber);

            // Verify
            Assert.AreEqual(expectedVehicleType, actualVehicleType);
        }

        [TestMethod]
        public void GetVehicleTypeOfParkedVehicle1McTest()
        {
            // Setup
            VehicleType expectedVehicleType = VehicleType.Mc;
            VehicleType actualVehicleType;
            string registrationNumber = "abc123";

            string[] parkingPlace = new string[1];
            parkingPlace[0] = ":abc123";

            // Act
            actualVehicleType = Parking.GetVehicleTypeOfParkedVehicle(parkingPlace, 0, registrationNumber);

            // Verify
            Assert.AreEqual(expectedVehicleType, actualVehicleType);
        }
        [TestMethod]
        public void GetVehicleTypeOfParkedVehicle2McFirstTest()
        {
            // Setup
            VehicleType expectedVehicleType = VehicleType.Mc;
            VehicleType actualVehicleType;
            string registrationNumber = "lkj987";

            string[] parkingPlace = new string[1];
            parkingPlace[0] = "lkj987:abc123";

            // Act
            actualVehicleType = Parking.GetVehicleTypeOfParkedVehicle(parkingPlace, 0, registrationNumber);

            // Verify
            Assert.AreEqual(expectedVehicleType, actualVehicleType);
        }
        [TestMethod]
        public void GetVehicleTypeOfParkedVehicle2McSecondTest()
        {
            // Setup
            VehicleType expectedVehicleType = VehicleType.Mc;
            VehicleType actualVehicleType;
            string registrationNumber = "abc123";

            string[] parkingPlace = new string[1];
            parkingPlace[0] = "lkj987:abc123";

            // Act
            actualVehicleType = Parking.GetVehicleTypeOfParkedVehicle(parkingPlace, 0, registrationNumber);

            // Verify
            Assert.AreEqual(expectedVehicleType, actualVehicleType);
        }
    }
}
