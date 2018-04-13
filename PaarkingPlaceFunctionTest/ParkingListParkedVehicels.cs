using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAsserts;
using ParkingPlace;

namespace PaarkingPlaceFunctionTest
{
    [TestClass]
    public class ParkingListParkedVehicels
    {
        [TestMethod]
        public void ListParkedVehicles1Test()
        {
            // Setup
            string[] park = new string[2];
            park[0] = ":lk433";
            park[1] = "dbc423:lk433";

            List<string> expected = new List<string>();
            expected.Add(":lk433");
            expected.Add("dbc423:lk433");

            List<string> actual;

            //Act
            actual = Parking.ListParkedVehicels(park);

            //Verify
            MyAssert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void ListParkedVehiclesNullTest()
        {
            // Setup
            string[] park = new string[5];
            park[0] = ":lk433";
            park[1] = "dbc423:lk433";
            park[2] = null;
            park[3] = "dbc423";
            park[4] = null;

            List<string> expected = new List<string>();
            expected.Add(":lk433");
            expected.Add("dbc423:lk433");
            expected.Add("dbc423");

            List<string> actual;

            //Act
            actual = Parking.ListParkedVehicels(park);

            //Verify
            MyAssert.AreEqual(expected, actual);

        }
    }
}
