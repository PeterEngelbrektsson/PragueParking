using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace ParkingPlaceTests
{
    [TestClass]
    public class ParkinSlotTests
    {


        public static int CountMc(string parkingPlace)
        {
            if (parkingPlace == null)
                return 0;

            int numberOfMcs = 0;
            int pos = parkingPlace.IndexOf(":");
            if (pos == 0)
                numberOfMcs = 1;
            else if (pos > 0)
            {
                numberOfMcs = 2;
            }
            return numberOfMcs;
        }

    

        [TestMethod]
        public void CountMc1McTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = ":abc124";
            int expectedCount = 1;
            int actualCount;

            //Act
            actualCount = ParkingSlot.CountMc(parkingPlace);

            //Verify
            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void CountMc2McTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "poi987:abc124";
            int expectedCount = 2;
            int actualCount;

            //Act
            actualCount = ParkingSlot.CountMc(parkingPlace);

            //Verify
            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void CountMc1CarTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "poi987";
            int expectedCount = 0;
            int actualCount;

            //Act
            actualCount = ParkingSlot.CountMc(parkingPlace);

            //Verify
            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void CountMcNullTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = null;
            int expectedCount = 0;
            int actualCount;

            //Act
            actualCount = ParkingSlot.CountMc(parkingPlace);

            //Verify
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void SplitVehicles2McsTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "abvc988:po6ttr";
            string[] expected = new string[2];
            expected[0] = "abvc988";
            expected[1] = "po6ttr";
            string[] actual;

            //Act
            actual = ParkingSlot.SplitVehicle(parkingPlace);

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SplitVehicles2McsCustomPlateTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "OneCustomPlate:po6ttr";
            string[] expected = new string[2];
            expected[0] = "OneCustomPlate";
            expected[1] = "po6ttr";
            string[] actual;

            //Act
            actual = ParkingSlot.SplitVehicle(parkingPlace);

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SplitVehicles1McsTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = ":po6ttr";
            string[] expected = new string[1];
            expected[0] = "po6ttr";
            string[] actual;

            //Act
            actual = ParkingSlot.SplitVehicle(parkingPlace);

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SplitVehicles1CarTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "po6ttr";
            string[] expected = new string[1];
            expected[0] = "po6ttr";
            string[] actual;

            //Act
            actual = ParkingSlot.SplitVehicle(parkingPlace);

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SplitVehiclesNullTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = null;
            string[] expected = null;
            string[] actual;

            //Act
            actual = ParkingSlot.SplitVehicle(parkingPlace);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void ContainsMcNullTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = null;
            string registrationNumber = "gfd765";
            bool expected = false;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsMc(parkingPlace,registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsMcCarTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "gfd765";
            string registrationNumber = "gfd765";
            bool expected = false;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsMc(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsMc1McTrueTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = ":gfd765";
            string registrationNumber = "gfd765";
            bool expected = true;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsMc(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsMc1McFalseTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = ":gfd765";
            string registrationNumber = "ifd765";
            bool expected = false;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsMc(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsMc2McFalseTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "098iuy:gfd765";
            string registrationNumber = "ifd765";
            bool expected = false;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsMc(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsMc2McTrueRightTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "098iuy:gfd765";
            string registrationNumber = "gfd765";
            bool expected = true;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsMc(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsMc2McTrueleftTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "098iuy:gfd765";
            string registrationNumber = "098iuy";
            bool expected = true;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsMc(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [ExpectedException(typeof(ParkingSpaceIsEmptyException))]
        public void GetVehicleTypeOfParkedVehicleNullTest()
        {
            // Setup
            VehicleType expectedVehicleType = VehicleType.Car;
            VehicleType actualVehicleType;
            string registrationNumber = "abc123";

            string parkingSlot = null;

            // Act
            actualVehicleType = ParkingSlot.GetVehicleTypeOfParkedVehicle(parkingSlot, registrationNumber);
            // Should throw exception

        }

        [TestMethod]
        public void GetVehicleTypeOfParkedVehicleCarTest()
        {
            // Setup
            VehicleType expectedVehicleType = VehicleType.Car;
            VehicleType actualVehicleType;
            string registrationNumber = "abc123";

            string parkingSlot = "abc123";

            // Act
            actualVehicleType = ParkingSlot.GetVehicleTypeOfParkedVehicle(parkingSlot, registrationNumber);

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

            string parkingSlot = ":abc123";

            // Act
            actualVehicleType = ParkingSlot.GetVehicleTypeOfParkedVehicle(parkingSlot, registrationNumber);

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

            string parkingSlot = "lkj987:abc123";

            // Act
            actualVehicleType = ParkingSlot.GetVehicleTypeOfParkedVehicle(parkingSlot, registrationNumber);

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

            string parkingSlot = "lkj987:abc123";

            // Act
            actualVehicleType = ParkingSlot.GetVehicleTypeOfParkedVehicle(parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedVehicleType, actualVehicleType);
        }
        [TestMethod]
        public void ParkingSlotRemoveMcLeftTest()
        {
            // Setup
            string registrationNumber = "lkj987";
            string parkingSlot = "lkj987:abc123";
            string expectedParkingSlot = ":abc123";

            // Act
             ParkingSlot.RemoveMc(ref parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedParkingSlot, parkingSlot);
        }
        [TestMethod]
        public void ParkingSlotRemoveMcRightTest()
        {
            // Setup
            string registrationNumber = "abc123";
            string parkingSlot = "lkj987:abc123";
            string expectedParkingSlot = ":lkj987";

            // Act
            ParkingSlot.RemoveMc(ref parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedParkingSlot, parkingSlot);
        }
        [TestMethod]
        public void ParkingSlotRemoveMcOneMcTest()
        {
            // Setup
            string registrationNumber = "abc123";
            string parkingSlot = ":abc123";
            string expectedParkingSlot = null;

            // Act
            ParkingSlot.RemoveMc(ref parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedParkingSlot, parkingSlot);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParkingSlotRemoveMcNullTest()
        {
            // Setup
            string registrationNumber = "abc123";
            string parkingSlot = null;
            string expectedParkingSlot = null;

            // Act
            ParkingSlot.RemoveMc(ref parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedParkingSlot, parkingSlot);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParkingSlotRemoveMcCarTest()
        {
            // Setup
            string registrationNumber = "abc123";
            string parkingSlot = "poi654";
            string expectedParkingSlot = null;

            // Act
            ParkingSlot.RemoveMc(ref parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedParkingSlot, parkingSlot);
        }
        [TestMethod]
        public void ParkingSlotRemoveVehicleLeftTest()
        {
            // Setup
            string registrationNumber = "lkj987";
            string parkingSlot = "lkj987:abc123";
            string expectedParkingSlot = ":abc123";

            // Act
            ParkingSlot.RemoveVehicle(ref parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedParkingSlot, parkingSlot);
        }
        [TestMethod]
        public void ParkingSlotRemoveVehicleRightTest()
        {
            // Setup
            string registrationNumber = "abc123";
            string parkingSlot = "lkj987:abc123";
            string expectedParkingSlot = ":lkj987";

            // Act
            ParkingSlot.RemoveVehicle(ref parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedParkingSlot, parkingSlot);
        }
        [TestMethod]
        public void ParkingSlotRemoveVehicleOneMcTest()
        {
            // Setup
            string registrationNumber = "abc123";
            string parkingSlot = ":abc123";
            string expectedParkingSlot = null;

            // Act
            ParkingSlot.RemoveVehicle(ref parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedParkingSlot, parkingSlot);
        }
        [TestMethod]
        [ExpectedException(typeof(ParkingSpaceIsEmptyException))]
        public void ParkingSlotRemoveVehicleNullTest()
        {
            // Setup
            string registrationNumber = "abc123";
            string parkingSlot = null;
            string expectedParkingSlot = null;

            // Act
            ParkingSlot.RemoveVehicle(ref parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedParkingSlot, parkingSlot);
        }
        [TestMethod]
        [ExpectedException(typeof(VehicleNotFoundException))]
        public void ParkingSlotRemoveVehicleCarNotFoundTest()
        {
            // Setup
            string registrationNumber = "abc123";
            string parkingSlot = "poi654";

            // Act
            ParkingSlot.RemoveVehicle(ref parkingSlot, registrationNumber);
        }
        [TestMethod]
        public void ParkingSlotRemoveVehicleCarFoundTest()
        {
            // Setup
            string registrationNumber = "poi654";
            string parkingSlot = "poi654";
            string expectedParkingSlot = null;

            // Act
            ParkingSlot.RemoveVehicle(ref parkingSlot, registrationNumber);

            // Verify
            Assert.AreEqual(expectedParkingSlot, parkingSlot);
        }
        [TestMethod]
        public void ContainsVehicleNullTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = null;
            string registrationNumber = "gfd765";
            bool expected = false;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsVehicle(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsVehicleCarTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "gfd765";
            string registrationNumber = "gfd765";
            bool expected = true;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsVehicle(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsVehicle1McTrueTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = ":gfd765";
            string registrationNumber = "gfd765";
            bool expected = true;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsVehicle(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsVehicle1McFalseTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = ":gfd765";
            string registrationNumber = "ifd765";
            bool expected = false;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsVehicle(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsVehicle2McFalseTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "098iuy:gfd765";
            string registrationNumber = "ifd765";
            bool expected = false;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsVehicle(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsVehicle2McTrueRightTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "098iuy:gfd765";
            string registrationNumber = "gfd765";
            bool expected = true;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsVehicle(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ContainsVehicle2McTrueleftTest()
        {
            //Setup
            string parkingPlace;
            parkingPlace = "098iuy:gfd765";
            string registrationNumber = "098iuy";
            bool expected = true;
            bool actual;

            //Act
            actual = ParkingSlot.ContainsVehicle(parkingPlace, registrationNumber);

            //Verify
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FindSearchString2MatchingMcsTest()
        {
            // Setup
            string slot = "abc123:lka987";

            string[] expected = new string[2];
            expected[0]= ":abc123";
            expected[1] = ":lka987";

            string[] actual;

            //Act
            actual = ParkingSlot.SearchVehicle(slot, "a");

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FindSearchStringOneMatchingMcTest()
        {
            // Setup
            string slot = "abc123:lka987";

            string[] expected = new string[1];
            expected[0] = ":lka987";

            string[] actual;

            //Act
            actual = ParkingSlot.SearchVehicle(slot, "k");

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FindSearchStringOneMatchingCarTest()
        {
            // Setup
            string slot = "abc123";

            string[] expected = new string[1];
            expected[0] = "abc123";

            string[] actual;

            //Act
            actual = ParkingSlot.SearchVehicle(slot, "b");

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FindSearchStringNoMatchingCarTest()
        {
            // Setup
            string slot = "abc123";

            string[] expected = null;

            string[] actual;

            //Act
            actual = ParkingSlot.SearchVehicle(slot, "z");

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FindSearchStringNoMatchingOneMcTest()
        {
            // Setup
            string slot = ":abc123";

            string[] expected = null;

            string[] actual;

            //Act
            actual = ParkingSlot.SearchVehicle(slot, "zaa");

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FindSearchStringNoMatchingTwoMcTest()
        {
            // Setup
            string slot = "tre4321:abc123";

            string[] expected = null;

            string[] actual;

            //Act
            actual = ParkingSlot.SearchVehicle(slot, "zaa");

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
    }
}
