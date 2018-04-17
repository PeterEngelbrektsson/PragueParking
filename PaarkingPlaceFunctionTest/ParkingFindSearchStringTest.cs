using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAsserts;
using ParkingPlace;

namespace ParkingPlaceFunctionTest
{
    [TestClass]
    public class ParkingFindSearchStringTest
    {
        [TestMethod]
        public void FindSearchStringTest()
        {
            // Setup
            string[] park = new string[4];
            park[0] = "abc123,1999-01-02 13:34";
            park[1] = "aer123,1999-01-02 13:34";
            park[2] = "htr863,1999-01-02 13:34";
            park[3] = "gfa863,1999-01-02 13:34";

            Dictionary<int, string> expected = new Dictionary<int, string>();
            expected.Add(0, "abc123,1999-01-02 13:34");
            expected.Add(1, "aer123,1999-01-02 13:34");
            expected.Add(3, "gfa863,1999-01-02 13:34");

            Dictionary<int, string> actual;

            //Act
            actual = Parking.FindSearchString(park, "a");

            //Verify
            MyAssert.AreEqual(expected, actual);
        }
    }
}
