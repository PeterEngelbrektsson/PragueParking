using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace ParkingPlaceFunctionTest
{
    [TestClass]
    public class OptimizeTests
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

        // All Mcs should be doubleparked except the last odd one.
        public void AssertOptimized(string[] parkingPlaces)
        {
            int foundHalfFreePlace = 0;
            foreach (string parkingPlace in parkingPlaces)
            {
                if (CountMc(parkingPlace) == 1)
                {
                    foundHalfFreePlace++;
                }
            }
            Assert.IsTrue(foundHalfFreePlace <= 1);
        }

        public int CountEmptyCarPlaces(string[] parkingPlaces)
        {
            int emptyPlaces = 0;
            foreach (string parkingPlace in parkingPlaces)
            {

                if (parkingPlace == null)
                {
                    emptyPlaces++;
                }
            }
            return emptyPlaces;
        }

        [TestMethod]
        public void OptimizeMc100ParkingPlacesTest()
        {
            //Setup
            string[] parkingPlaces = PopulateParkingPlace(100);
            parkingPlaces[0] = ":abc123";
            parkingPlaces[10] = "abc123:lkj987";
            parkingPlaces[20] = ":abc124";
            parkingPlaces[30] = ":abc125";

            //Act
            Parking.doOptimize(parkingPlaces);

            //Verify
            AssertOptimized(parkingPlaces);
            Assert.AreEqual(1, CountEmptyCarPlaces(parkingPlaces));
        }
        [TestMethod]
        public void OptimizeMcTest()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = ":abc123";
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = ":abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = ":abc127";
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = ":abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = ":abc121";

            //Act
            Parking.doOptimize(parkingPlaces);


            //Verify
            AssertOptimized(parkingPlaces);
            Assert.AreEqual(4, CountEmptyCarPlaces(parkingPlaces));
        }
        [TestMethod]
        public void OptimizeMcTest2()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = ":abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = "abc121";

            //Act
            Parking.doOptimize(parkingPlaces);

            //Verify
            AssertOptimized(parkingPlaces);
            Assert.AreEqual(4, CountEmptyCarPlaces(parkingPlaces));
        }
        [TestMethod]
        public void FindFirstSingleParkedMc100PlacesTest()
        {
            //Setup
            string[] parkingPlaces = PopulateParkingPlace(100);
            parkingPlaces[0] = null;
            parkingPlaces[10] = "abc123:lkj987";
            parkingPlaces[12] = ":abc124";
            parkingPlaces[13] = ":abc125";
            parkingPlaces[14] = ":abc126";
            parkingPlaces[15] = null;
            parkingPlaces[16] = ":abc128";
            parkingPlaces[17] = "abc129";
            parkingPlaces[18] = ":abc120";
            parkingPlaces[19] = "abc121";
            int expectedPosition = 12;
            int actualPosition;
            //Act
            actualPosition = Parking.FindFirstSingleParkedMc(parkingPlaces, 0);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindFirstSingleParkedMcCustomPlatesTest()
        {
            //Setup
            string[] parkingPlaces = PopulateParkingPlace(100);
            parkingPlaces[0] = null;
            parkingPlaces[10] = "abc123:lkj987";
            parkingPlaces[12] = ":abc124";
            parkingPlaces[13] = ":CustomPlate";
            parkingPlaces[14] = ":abc126";
            parkingPlaces[15] = null;
            parkingPlaces[16] = ":abc128";
            parkingPlaces[17] = "ab12b9";       // Foreign plate
            parkingPlaces[18] = ":123abc";      // Foreign plate
            parkingPlaces[19] = "abcg121";      // Foreign plate
            int expectedPosition = 12;
            int actualPosition;
            //Act
            actualPosition = Parking.FindFirstSingleParkedMc(parkingPlaces, 0);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindFirstSingleParkedMcTest()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = ":abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = "abc121";
            int expectedPosition = 2;
            int actualPosition;
            //Act
            actualPosition = Parking.FindFirstSingleParkedMc(parkingPlaces, 0);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindFirstSingleParkedMcTest2()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = ":abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = "abc121";
            int expectedPosition = 3;
            int actualPosition;
            //Act
            actualPosition = Parking.FindFirstSingleParkedMc(parkingPlaces, 3);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindFirstSingleParkedMcTest3()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = "abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = "abc121";
            int expectedPosition = 4;
            int actualPosition;
            //Act
            actualPosition = Parking.FindFirstSingleParkedMc(parkingPlaces, 3);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindFirstSingleParkedMcEndOfArrayTest()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = "abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = "abc121";
            int expectedPosition = -1;
            int actualPosition;
            //Act
            actualPosition = Parking.FindFirstSingleParkedMc(parkingPlaces, 9);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindFirstSingleParkedMcZeroTest()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = ":abc124";
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = "abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = "abc121";
            int expectedPosition = 0;
            int actualPosition;
            //Act
            actualPosition = Parking.FindFirstSingleParkedMc(parkingPlaces, 0);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindLastSingleParkedMcTest()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = "abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = "abc121";
            int expectedPosition = 8;
            int actualPosition;
            //Act
            actualPosition = Parking.FindLastSingleParkedMc(parkingPlaces, parkingPlaces.Length - 1);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindLastSingleParkedMcTest2()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = "abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = "uyt675:abc120";
            parkingPlaces[9] = "abc121";
            int expectedPosition = 6;
            int actualPosition;
            //Act
            actualPosition = Parking.FindLastSingleParkedMc(parkingPlaces, parkingPlaces.Length - 1);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindLastSingleParkedMcTest3()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = "abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = "uyt675:abc120";
            parkingPlaces[9] = "abc121";
            int expectedPosition = 6;
            int actualPosition;
            //Act
            actualPosition = Parking.FindLastSingleParkedMc(parkingPlaces, 7);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindLastSingleParkedMcStartOfArrayTest()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = "abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = "uyt675:abc120";
            parkingPlaces[9] = "abc121";
            int expectedPosition = -1;
            int actualPosition;
            //Act
            actualPosition = Parking.FindLastSingleParkedMc(parkingPlaces, 1);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
        }
        [TestMethod]
        public void FindLastSingleParkedMcZeroTest()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = ":abc124";
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = "abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = "uyt675:abc120";
            parkingPlaces[9] = "abc121";
            int expectedPosition = 0;
            int actualPosition;

            //Act
            actualPosition = Parking.FindLastSingleParkedMc(parkingPlaces, 1);

            //Verify
            Assert.AreEqual(expectedPosition, actualPosition);
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
        public void AddMcAtPosition1stTest()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = ":abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = "abc121";

            string[] expectedParkingPlaces = new string[10];
            expectedParkingPlaces[0] = ":mnb543";
            expectedParkingPlaces[1] = "abc123:lkj987";
            expectedParkingPlaces[2] = ":abc124";
            expectedParkingPlaces[3] = ":abc125";
            expectedParkingPlaces[4] = ":abc126";
            expectedParkingPlaces[5] = null;
            expectedParkingPlaces[6] = ":abc128";
            expectedParkingPlaces[7] = "abc129";
            expectedParkingPlaces[8] = ":abc120";
            expectedParkingPlaces[9] = "abc121";

            //Act
            Parking.AddMcAtPosition(parkingPlaces, "mnb543", 0);

            //Verify
            MyAssert.AreEqual(expectedParkingPlaces, parkingPlaces);
        }
        [TestMethod]
        public void AddMcAtPosition2stTest()
        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = ":abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = "abc121";

            string[] expectedParkingPlaces = new string[10];
            expectedParkingPlaces[0] = null;
            expectedParkingPlaces[1] = "abc123:lkj987";
            expectedParkingPlaces[2] = ":abc124";
            expectedParkingPlaces[3] = "mnb543:abc125";
            expectedParkingPlaces[4] = ":abc126";
            expectedParkingPlaces[5] = null;
            expectedParkingPlaces[6] = ":abc128";
            expectedParkingPlaces[7] = "abc129";
            expectedParkingPlaces[8] = ":abc120";
            expectedParkingPlaces[9] = "abc121";

            //Act
            Parking.AddMcAtPosition(parkingPlaces, "mnb543", 3);

            //Verify
            MyAssert.AreEqual(expectedParkingPlaces, parkingPlaces);
        }

        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceOccupiedException))]
        public void AddMcAtPositionFilledByCarTest()

        {
            //Setup
            string[] parkingPlaces = new string[10];
            parkingPlaces[0] = null;
            parkingPlaces[1] = "abc123:lkj987";
            parkingPlaces[2] = ":abc124";
            parkingPlaces[3] = ":abc125";
            parkingPlaces[4] = ":abc126";
            parkingPlaces[5] = null;
            parkingPlaces[6] = ":abc128";
            parkingPlaces[7] = "abc129";
            parkingPlaces[8] = ":abc120";
            parkingPlaces[9] = "abc121";

            //Act
            // Occupied by car => should throw exception
            Parking.AddMcAtPosition(parkingPlaces, "mnb543", 7);
        }
        [TestMethod]
        [ExpectedException(typeof(ParkingPlaceOccupiedException))]
        public void AddMcAtPositionFille100CarsTest()

        {
            //Setup
            string[] parkingPlaces = PopulateParkingPlace(100);

            //Act
            // Occupied by car => should throw exception
            Parking.AddMcAtPosition(parkingPlaces, "mnb543", 7);
        }

    }
}
