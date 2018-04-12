using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingPlace;
using MyAsserts;

namespace PaarkingPlaceFunctionTest
{
    [TestClass]
    public class ParkingFindTests
    {
            [TestMethod]
            public void FindCarAllFullCarTest()
            {
                // Setup
                string[] park = new string[1];
                park[0] = "abc123";
                int actualPlace = -1;
                int expectedPlace = -1;

                //Act
                actualPlace = Parking.Find(park, "bcd987");

                //Verify
                Assert.AreEqual(expectedPlace, actualPlace);
            }

            [TestMethod]
            public void FindAllFullCarCarTest()
            {
                // Setup
                string[] park = new string[3];
                park[0] = "abc123";
                park[1] = "der123";
                park[2] = "htr863";
                int actualPlace = -1;
                int expectedPlace = 2;

                //Act
                actualPlace = Parking.Find(park, "htr863");

                //Verify
                Assert.AreEqual(expectedPlace, actualPlace);
            }
            [TestMethod]
            public void FindMCTest()
            {
                // Setup
                string[] park = new string[4];
                park[0] = ":abc123";
                park[1] = "der123:pou456";
                park[2] = ":htr863";
                park[3] = "ytr654";
                int actualPlace = -1;
                int expectedPlace = 2;

                //Act
                actualPlace = Parking.Find(park, "htr863");

                //Verify
                Assert.AreEqual(expectedPlace, actualPlace);
            }
            [TestMethod]
            public void FindMC2McsRightTest()
            {
                // Setup
                string[] park = new string[4];
                park[0] = ":abc123";
                park[1] = "der123:pou456";
                park[2] = ":htr863";
                park[3] = "ytr654";
                int actualPlace = -1;
                int expectedPlace = 1;

                //Act
                actualPlace = Parking.Find(park, "pou456");

                //Verify
                Assert.AreEqual(expectedPlace, actualPlace);
            }
            [TestMethod]
            public void FindMC2McsLeftTest()
            {
                // Setup
                string[] park = new string[4];
                park[0] = ":abc123";
                park[1] = "der123:pou456";
                park[2] = ":htr863";
                park[3] = "ytr654";
                int actualPlace = -1;
                int expectedPlace = 1;

                //Act
                actualPlace = Parking.Find(park, "der123");

                //Verify
                Assert.AreEqual(expectedPlace, actualPlace);
            }
            [TestMethod]
            public void FindMC2Test()
            {
                // Setup
                string[] park = new string[4];
                park[0] = ":abc123";
                park[1] = "der123:pou456";
                park[2] = ":htr863";
                park[3] = "ytr654";
                int actualPlace = -1;
                int expectedPlace = 1;

                //Act
                actualPlace = Parking.Find(park, "pou456");

                //Verify
                Assert.AreEqual(expectedPlace, actualPlace);
            }
            [TestMethod]
            public void FindMC3Test()
            {
                // Setup
                string[] park = new string[4];
                park[0] = ":abc123";
                park[1] = "der123:pou456";
                park[2] = ":htr863";
                park[3] = "ytr654";
                int actualPlace = -1;
                int expectedPlace = 0;

                //Act
                actualPlace = Parking.Find(park, "abc123");

                //Verify
                Assert.AreEqual(expectedPlace, actualPlace);
            }

        
    }
}