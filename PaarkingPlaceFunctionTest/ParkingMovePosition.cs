using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace ParkingPlaceFunctionTest
{
    [TestClass]
    public class ParkingMovePositionTests
    {

        [TestMethod]
    public void MovePositionMcNullTest()
    {
        // Setup
        string[] park = new string[2];
        park[0] = null;
        park[1] = "dbc423:8toi43";

        string[] expected = new string[2];
        expected[0] = ":8toi43";
        expected[1] = ":dbc423";


        //Act
        Parking.Move(park, "8toi43",VehicleType.Mc, 1,0);

        //Verify
        MyAssert.AreEqual(expected, park);
    }
    [TestMethod]
    public void MovePositionCarNullTest()
    {
        // Setup
        string[] park = new string[2];
        park[0] = null;
        park[1] = "8toi43";

        string[] expected = new string[2];
        expected[0] = "8toi43";
        expected[1] = null;


        //Act
        Parking.Move(park, "8toi43", VehicleType.Car, 1, 0);

        //Verify
        MyAssert.AreEqual(expected, park);
    }
    [TestMethod]
    public void MovePositionCar2Cars4arrayTest()
    {
        // Setup
        string[] park = new string[4];
        park[0] = null;
        park[1] = "8toi43";
        park[2] = "abc213";
        park[3] = null;

        string[] expected = new string[4];
            expected[0] = null;
            expected[1] = "8toi43";
            expected[2] = null;
            expected[3] = "abc213";


        //Act
        Parking.Move(park, "abc213", VehicleType.Car, 2,3);

        //Verify
        MyAssert.AreEqual(expected, park);
    }
    [TestMethod]
    public void MovePositionMc1Test()
    {
        // Setup
        string[] park = new string[4];
        park[0] = null;
        park[1] = "8toi43";
        park[2] = "abc213:tre765";
        park[3] = null;

        string[] expected = new string[4];
            expected[0] = null;
            expected[1] = "8toi43";
            expected[2] = ":tre765";
            expected[3] = ":abc213";

            //Act
            Parking.Move(park, "abc213", VehicleType.Mc, 2,3);

            //Verify
            MyAssert.AreEqual(expected, park);
        }

    [TestMethod]
    public void MovePositionMc2Test()
    {
        // Setup
        string[] park = new string[4];
        park[0] = null;
        park[1] = ":8toi43";
        park[2] = "abc213:tre765";
        park[3] = null;

        string registrationNumber = "abc213";

        string[] expected = new string[4];
            expected[0] = null;
            expected[1] = "8toi43:abc213";
            expected[2] = ":tre765";
            expected[3] = null;

        //Act
        Parking.Move(park, registrationNumber, VehicleType.Mc, 2,1);

            //Verify
            Assert.AreEqual(expected[0], park[0]);
            Assert.AreEqual(expected[2], park[2]);
            Assert.AreEqual(expected[3], park[3]);
            Assert.IsTrue(park[1]=="8toi43:abc213" | park[1] == "abc213:8toi43");
        }
}
}
