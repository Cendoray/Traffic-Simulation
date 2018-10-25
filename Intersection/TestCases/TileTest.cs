using System;
using TrafficIntersection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCases
{
    [TestClass]
    public class TileTest
    {
        [TestMethod]
        public void TestConstructorRoad()
        {
            Tile r = new Road(Direction.Up);
            Tile rr = new Road(Direction.Up);
            Assert.AreEqual(r.Direction, rr.Direction);
            Assert.AreEqual(r.Occupied, rr.Occupied);
        }

        [TestMethod]
        public void TestConstructorRoadAndGrassFail()
        {
            Tile r = new Road(Direction.Down);
            Tile g = new Grass();
            Assert.AreNotEqual(r.Direction, g.Direction);
        }

        [TestMethod]
        public void TestConstructorGrass()
        {
            Grass g = new Grass();
            Grass gg = new Grass();

            Assert.AreEqual(g.Direction, gg.Direction);
            Assert.AreEqual(g.Occupied, gg.Occupied);
        }


        [TestMethod]
        public void TestConstructorIntersectionTile()
        {
            Tile it = new IntersectionTile();
            Tile itt = new IntersectionTile();

            Assert.AreEqual(it.Direction, itt.Direction);
            Assert.AreEqual(it.Occupied, itt.Occupied);
        }

        [TestMethod]
        public void TestConstructorIntersectionTileFail()
        {
            Tile it = new IntersectionTile();
            Road r = new Road(Direction.Up);
            Assert.AreNotEqual(it.Direction, r.Direction);
        }


        [TestMethod]
        public void TestConstructorLight()
        {
            int[] timer = new int[] { 3,3,3,3 };
            ISignalStrategy iss = new FixedSignal(timer);
            Tile l = new Light(iss, Direction.Up);
            Tile ll = new Light(iss, Direction.Up);


            Assert.AreEqual(l.Direction, ll.Direction);
            Assert.AreEqual(ll.Occupied, ll.Occupied);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestConstructorLightFailStrategy()
        {
            int[] timer = new int[] { 5, 5, 5, 5 };
            ISignalStrategy iss = null;
            ISignalStrategy iss2 = new FixedSignal(timer);
            Tile l = new Light(iss, Direction.Up);
            Tile ll = new Light(iss2, Direction.Up);


            Assert.AreEqual(l.Direction, ll.Direction);
            Assert.AreEqual(((Light)l).Colour, ((Light)ll).Colour);
        }


        public void TestConstructorLightFailDirection()
        {
            int[] timer = new int[] { 5, 5, 5, 5 };
            ISignalStrategy iss = null;
            ISignalStrategy iss2 = new FixedSignal(timer);
            Tile l = new Light(iss, Direction.None);
            Tile ll = new Light(iss2, Direction.None);


            Assert.AreEqual(l.Direction, ll.Direction);
            Assert.AreEqual(((Light)l).Colour, ((Light)ll).Colour);
        }

    }
}
