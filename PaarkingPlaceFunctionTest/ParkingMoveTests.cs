﻿using System;
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
            park[1] = "8toi43,1999-01-02 13:34";

            string[] expected = new string[2];
            expected[0] = "8toi43,1999-01-02 13:34";
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
            park[1] = "8toi43,1999-01-02 13:34";
            park[2] = "abc213,1999-01-02 13:34";
            park[3] = null;

            string[] expected = new string[4];
            expected[0] = null;
            expected[1] = "8toi43,1999-01-02 13:34";
            expected[2] = null;
            expected[3] = "abc213,1999-01-02 13:34";


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

            string registrationNumberToMove = "abc213";

            string[] expected = new string[4];
            expected[0] = null;
            expected[1] = "8toi43";
            expected[2] = ":tre765";
            expected[3] = ":abc213";

            //Act
            Parking.Move(park, registrationNumberToMove, 3);

            //Verify
            MyAssert.AreEqual(expected, park);
        }

        [TestMethod]
        public void MoveMc2Test()
        {
            // Setup
            string[] park = new string[4];
            park[0] = null;
            park[1] = ":8toi43";
            park[2] = "abc213:tre765";
            park[3] = null;

            string registrationNumberToMove = "abc213";

            string[] expected = new string[4];
            expected[0] = null;
            expected[1] = "8toi43:abc213";
            expected[2] = ":tre765";
            expected[3] = null;

            //Act
            Parking.Move(park, registrationNumberToMove, 1);

            //Verify
            Assert.AreEqual(expected[0], park[0]);
            Assert.AreEqual(expected[2], park[2]);
            Assert.AreEqual(expected[3], park[3]);
            Assert.IsTrue( park[1]== "8toi43:abc213" | park[1]== "abc213:8toi43");
        }
        [TestMethod]
        [ExpectedException(typeof(VehicleAlreadyAtThatPlaceException))]
        public void MoveMcToSamePositionTest()
        {
            // Setup
            string[] park = new string[4];
            park[0] = null;
            park[1] = ":8toi43";
            park[2] = "abc213:tre765";
            park[3] = null;

            string registrationNumberToMove = "abc213";

            //Act
            Parking.Move(park, registrationNumberToMove, 2);
        }
        [TestMethod]
        [ExpectedException(typeof(VehicleAlreadyAtThatPlaceException))]
        public void MoveCarToSamePositionTest()
        {
            // Setup
            string[] park = new string[4];
            park[0] = null;
            park[1] = "8toi43";
            park[2] = "abc213:tre765";
            park[3] = null;

            string registrationNumberToMove = "8toi43";


            //Act
            Parking.Move(park, registrationNumberToMove, 1);
        }
    }
}
