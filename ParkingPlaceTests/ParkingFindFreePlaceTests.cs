using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace ParkingPlaceTests
{
    [TestClass]
    public class ParkingFindFreePlaceTests
    {
        [TestMethod]
        public void ParkingIsFreeFor_Car1Test()
        {
            // Setup
            VehicleType vehicle = VehicleType.Car;
            string parkingSlot = "abc123";
            bool expected = false;
            bool actual;

            // Act
            actual = ParkingSlot.IsFreeFor(parkingSlot, vehicle);

            // Verify
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParkingIsFreeFor_CarFreeTest()
        {
            // Setup
            VehicleType vehicle = VehicleType.Car;
            string parkingSlot = null;
            bool expected = true;
            bool actual;

            // Act
            actual = ParkingSlot.IsFreeFor(parkingSlot, vehicle);

            // Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParkingIsFreeForMc_FreeTest()
        {
            // Setup
            VehicleType vehicle = VehicleType.Mc;
            string parkingSlot = null;
            bool expected = true;
            bool actual;

            // Act
            actual = ParkingSlot.IsFreeFor(parkingSlot, vehicle);

            // Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParkingIsFreeFor_Mc1McTest()
        {
            // Setup
            VehicleType vehicle = VehicleType.Mc;
            string parkingSlot = ":abc987";
            bool expected = true;
            bool actual;

            // Act
            actual = ParkingSlot.IsFreeFor(parkingSlot, vehicle);

            // Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParkingIsFreeFor_Mc1CarTest()
        {
            // Setup
            VehicleType vehicle = VehicleType.Mc;
            string parkingSlot = "abc987";
            bool expected = false;
            bool actual;

            // Act
            actual = ParkingSlot.IsFreeFor(parkingSlot, vehicle);

            // Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParkingIsFreeFor_Mc2McTest()
        {
            // Setup
            VehicleType vehicle = VehicleType.Mc;
            string parkingSlot = "abc987:iuy654";
            bool expected = false;
            bool actual;

            // Act
            actual = ParkingSlot.IsFreeFor(parkingSlot, vehicle);

            // Verify
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParkingIsFreeForMc_2McTest()
        {
            // Setup
            VehicleType vehicle = VehicleType.Mc;
            string parkingSlot = "abc987:iuy654";
            bool expected = false;
            bool actual;

            // Act
            actual = ParkingSlot.IsFreeForMc(parkingSlot, vehicle);

            // Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParkingIsFreeForMc_1McTest()
        {
            // Setup
            VehicleType vehicle = VehicleType.Mc;
            string parkingSlot = ":iuy654";
            bool expected = true;
            bool actual;

            // Act
            actual = ParkingSlot.IsFreeForMc(parkingSlot, vehicle);

            // Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ParkingIsFreeForMc_TestNull()
        {
            // Setup
            VehicleType vehicle = VehicleType.Mc;
            string parkingSlot = null;
            bool expected = true;
            bool actual;

            // Act
            actual = ParkingSlot.IsFreeForMc(parkingSlot, vehicle);

            // Verify
            Assert.AreEqual(expected, actual);
        }

    }
}
