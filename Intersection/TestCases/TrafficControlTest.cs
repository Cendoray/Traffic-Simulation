using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficIntersection;

namespace TestCases
{
    [TestClass]
    public class TrafficControlTest
    {

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestParseInvalidInput()
        {
            TrafficControl.Parse("");
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestParseNull()
        {
            TrafficControl.Parse(null);
        }

        [TestMethod]
        public void TestParseValidInput()
        {
            string input = File.ReadAllText("./test_valid.txt");
            TrafficControl.Parse(input);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestParseGridNotSquare()
        {
            string input = File.ReadAllText("./test_notsquare.txt");
            TrafficControl.Parse(input);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestParseGridTooSmall()
        {
            string input = File.ReadAllText("./test_toosmall.txt");
            TrafficControl.Parse(input);
        }
    }
}
