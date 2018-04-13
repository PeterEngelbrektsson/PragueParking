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

            Dictionary<int, string> expected = new Dictionary<int, string>();
            expected.Add(0,":lk433");
            expected.Add(1,"dbc423:lk433");

            Dictionary<int,string> actual;

            //Act
            actual = Parking.ListParkedVehicels(park);

            //Verify
            MyAssert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void ListParkedVehiclesNullaTest()
        {
            // Setup
            string[] park = new string[5];
            park[0] = ":lk433";
            park[1] = "dbc423:lk433";
            park[2] = null;
            park[3] = null;
            park[4] = "dbt423:ltt33";

            Dictionary<int, string> expected = new Dictionary<int, string>();
            expected.Add(0, ":lk433");
            expected.Add(1, "dbc423:lk433");
            expected.Add(4, "dbt423:ltt33");

            Dictionary<int, string> actual;

            //Act
            actual = Parking.ListParkedVehicels(park);

            //Verify
            MyAssert.AreEqual(expected, actual);

        }
    }
}
