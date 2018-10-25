using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficIntersection;

namespace TestCases
{
    [TestClass]
    public class TotalTest
    {
        static Tile[,] tile = new Tile[,] { { new Grass(), new Road(Direction.Down), new Road(Direction.Down), new Grass() },
                                            { new Road(Direction.Right), new IntersectionTile(),  new IntersectionTile(), new Road(Direction.Right) },
                                            { new Road(Direction.Right), new Road(Direction.Down), new Road(Direction.Down), new Road(Direction.Right) },
                                             {new Grass(), new Road(Direction.Down), new Road(Direction.Down), new Grass() } };
        static Tile[,] tile2 = new Tile[,] { { new IntersectionTile(), new IntersectionTile(), new IntersectionTile(), new Road(Direction.Down) },
                                             { new IntersectionTile(), new IntersectionTile(), new IntersectionTile(), new Road(Direction.Right) },
                                             {  new IntersectionTile(), new IntersectionTile(), new IntersectionTile(), new Road(Direction.Right) },
                                             { new Road(Direction.Down), new Road(Direction.Down), new Road(Direction.Down), new Grass() } }; 
        private Grid grid = new Grid(tile);

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TotalFail()
        {
            Total t = new Total(-5);
        }

        [TestMethod]
        public void TotalWin()
        {
            Total t = new Total(2);
            Total tt = new Total(2);
            Assert.AreEqual(t.Passengers, tt.Passengers);
            Assert.AreEqual(t.Emissions, tt.Emissions);
        }

        [TestMethod]
        public void UpdatePassengersWin()
        {
            Total t = new Total(2);
            IVehicle vehicle = new Car(grid);
            t.Passengers = 5;
            t.updatePassengers(vehicle);
            Assert.AreEqual(8, t.Passengers);
        }

        [TestMethod]
        public void UpdateEmissionWin()
        {
            Total t = new Total(2);
            IVehicle vehicle = new Car(grid);
            t.Emissions = 5;
            t.updateEmission(vehicle);
            Assert.AreEqual(10, t.Emissions);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdateEmissionFail()
        {
            Total t = new Total(2);
            IVehicle vehicle = null;
            t.Emissions = 5;
            t.updateEmission(vehicle);
            Assert.AreEqual(10, t.Emissions);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void UpdatePassengersFail()
        {
            Total t = new Total(2);
            IVehicle vehicle = null;
            t.Passengers = 5;
            t.updatePassengers(vehicle);
            Assert.AreEqual(10, t.Passengers);
        }

        [TestMethod]
        public void TestPassengersWin()
        {
            Total t = new Total(5);
            t.Passengers = 5;
            int x = t.Passengers;
            Assert.AreEqual(x, t.Passengers);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestPassengersFail()
        {
            Total t = new Total(5);
            t.Passengers = -5;
            int x = t.Passengers;
            Assert.AreEqual(x, t.Passengers);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestEmissionsFail()
        {
            Total t = new Total(5);
            t.Emissions = -5;
            double x = t.Emissions;
            Assert.AreEqual(x, t.Emissions);
        }

        [TestMethod]
        public void TestEmissions()
        {
            Total t = new Total(5);
            t.Emissions = 5;
            double x = t.Emissions;
            Assert.AreEqual(x, t.Emissions);
        }
    }
}
