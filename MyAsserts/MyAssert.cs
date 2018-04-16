using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MyAsserts
{
    [TestClass]
    public class MyAssert
    {
        public static void AreEqual(string[] arr1, string[] arr2)
        {
            if(!(arr1==null && arr2 == null)){

                Assert.AreEqual(arr1.Length, arr2.Length);

                for (int i = 0; i < arr1.Length; i++)
                {
                    Assert.AreEqual(arr1[i], arr2[i], " Missmatch at array element " + i);
                }
            }
        }
        public static void AreEqual(List<String> expected, List<string> actual)
        {

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i], " Missmatch at list element " + i);
            }
        }
        public static void AreEqual(Dictionary<int,String> expected, Dictionary<int, String> actual)
        {

            Assert.AreEqual(expected.Count, actual.Count);

            foreach(KeyValuePair<int,string> post in expected)
            {
                Assert.IsTrue(actual.ContainsKey(post.Key));
                Assert.IsTrue(actual[post.Key] == post.Value);
            }
        }
        public static void AreEqual(int[] arr1, int[] arr2)
        {

            Assert.AreEqual(arr1.Length, arr2.Length);

            for (int i = 0; i < arr1.Length; i++)
            {
                Assert.AreEqual(arr1[i], arr2[i]," Missmatch at array element " + i);
            }
        }
        public static void AreEqual(int[] arr1, int[] arr2, string msg)
        {

            Assert.AreEqual(arr1.Length, arr2.Length);

            for (int i = 0; i < arr1.Length; i++)
            {
                Assert.AreEqual(arr1[i], arr2[i], msg);
            }
        }
        public static void AreNotEqual(int[] arr1, int[] arr2, string msg)
        {
            if (arr1.Length != arr2.Length)
            {
                // not equal
                return;
            }

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    // not equal
                    return;
                };
            }

            // Fell through means equeal => assert fail
            Assert.Fail(msg);
        }
        public static void AreNotEqual(int[] arr1, int[] arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                // not equal
                return;
            }

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    // not equal
                    return;
                };
            }

            // Fell through means equeal => assert fail
            Assert.Fail();
        }
    }
}
