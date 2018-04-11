using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace ParkingPlaceFunctionTest
{
    [TestClass]
    public class ParkingMoveTests
    {
        [TestMethod]
        public void MoveMcNullTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = null;
            park[1] = "dbc423:8toi43";

            string[] expected = new string[2];
            expected[0] = ":8toi43";
            expected[1] = ":dbc423";


            //Act
            Parking.Move(park, "8toi43", 0 );

            //Verify
            MyAssert.AreEqual(expected, park);
        }
        [TestMethod]
        public void MoveCarNullTest()
        {
            // Setup
            string[] park = new string[2];
            park[0] = null;
            park[1] = "8toi43";

            string[] expected = new string[2];
            expected[0] = "8toi43";
            expected[1] = null;


            //Act
            Parking.Move(park, "8toi43", 0);

            //Verify
            MyAssert.AreEqual(expected, park);
        }
        [TestMethod]
        public void MoveCar2Cars4arrayTest()
        {
            // Setup
            string[] park = new string[4];
            park[0] = null;
            park[1] = "8toi43";
            park[2] = "abc213";
            park[3] = null;

            string[] expected = new string[4];
            park[0] = null;
            park[1] = "8toi43";
            park[2] = null;
            park[3] = "abc213";


            //Act
            Parking.Move(park, "abc213", 3);

            //Verify
            MyAssert.AreEqual(expected, park);
        }
        [TestMethod]
        public void MoveMc1Test()
        {
            // Setup
            string[] park = new string[4];
            park[0] = null;
            park[1] = "8toi43";
            park[2] = "abc213:tre765";
            park[3] = null;

            string[] expected = new string[4];
            park[0] = null;
            park[1] = "8toi43";
            park[2] = "abc213:";

            //Act
            Parking.Move(park, "abc213", 3);

            //Verify
            Assert.AreEqual(expected[0], park[0]);
            Assert.AreEqual(expected[1], park[1]);
            Assert.AreEqual(expected[2], park[2]);
            if (!(park[3].Equals(":abc213") | park[3].Equals("abc213:")))
                Assert.Fail("Expected :abc213 or abc213: Was "+park[3]);
        }

        [TestMethod]
        public void MoveMc2Test()
        {
            // Setup
            string[] park = new string[4];
            park[0] = null;
            park[1] = "8toi43:";
            park[2] = "abc213:tre765";
            park[3] = null;

            string[] expected = new string[4];
            park[0] = null;
            park[1] = "8toi43:abc213";
            park[2] = ":tre765";
            park[3] = null;

            //Act
            Parking.Move(park, "abc213", 1);

            //Verify
            MyAssert.AreEqual(expected, park);
        }
    }
}
