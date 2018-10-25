using System;
using Microsoft.Xna.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficIntersection;
using System.Collections.Generic;
using System.Collections;

namespace TestCases
{
    [TestClass]
    public class IntersectionTest
    {
        ISignalStrategy issFail;
        int[] issInt;
        ISignalStrategy iss;
        Tile[,] tile;
        Tile[,] tile2;
        Grid grid;
        Grid grid2;
        Grid gridFail;
        List<Vector2> vectors;
        List<Vector2> vectorsFail;

        private void CreateAll() {
            this.issFail = null;
            this.issInt = new int[] { 3, 3, 3, 3 };
            this.iss = new FixedSignal(issInt);
            tile = new Tile[,] { { new Grass(), new Road(Direction.Down), new Road(Direction.Down), new Grass() },
                                            { new Road(Direction.Right), new IntersectionTile(),  new IntersectionTile(), new Road(Direction.Right) },
                                            { new Road(Direction.Right), new Road(Direction.Down), new Road(Direction.Down), new Road(Direction.Right) },
                                             {new Grass(), new Road(Direction.Down), new Road(Direction.Down), new Grass() } };
            tile2 = new Tile[,] { { new IntersectionTile(), new IntersectionTile(), new IntersectionTile(), new Road(Direction.Down) },
                                             { new IntersectionTile(), new IntersectionTile(), new IntersectionTile(), new Road(Direction.Right) },
                                             {  new IntersectionTile(), new IntersectionTile(), new IntersectionTile(), new  Road(Direction.Right) },
                                             { new Road(Direction.Down), new Road(Direction.Down), new Road(Direction.Down), new Grass() } }; this.grid = new Grid(tile);
            this.grid = new Grid(tile);
            this.grid2 = new Grid(tile2);
            this.gridFail = null;
            this.vectors = new List<Vector2>();
            this.vectorsFail = null;
            this.vectors.Add(new Vector2(1,2));
            this.vectors.Add(new Vector2(0, 1));
            this.vectors.Add(new Vector2(0, 2));
            this.vectors.Add(new Vector2(1, 0));
            this.vectors.Add(new Vector2(2, 0));
        }
        [TestMethod]
        public void ConstrutorTestNoParam()
        {
            Intersection t = new Intersection();

        }
        [TestMethod]

        public void ConstructorTest() {
            CreateAll();
            Intersection t = new Intersection(iss, vectors, grid);

        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]

        public void ConstructorFailGrid()
        {
            CreateAll();
            Intersection t = new Intersection(iss, vectors, gridFail);

        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]

        public void ConstructorFailSignal()
        {
            CreateAll();
            Intersection t = new Intersection(issFail, vectors, grid);

        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]

        public void ConstructorFailList()
        {
            CreateAll();
            Intersection t = new Intersection(iss, vectorsFail, grid);

        }
        [TestMethod]

        public void UpdateTest()
        {
            CreateAll();
            Intersection t = new Intersection(iss, vectors, grid);
            IVehicle car = new Car(grid2);
            car.Direction = Direction.Down;
            t.Add(car);
            IEnumerator ie = t.GetEumerator();
            ie.MoveNext();
            IVehicle iv = (IVehicle)ie.Current;
            int x = iv.X;
            int y = iv.Y;
            t.Update();
            t.Update();
            IVehicle iv2 = (IVehicle)ie.Current;
            if (iv2.Direction == Direction.Left || iv2.Direction == Direction.Right)
                Assert.AreNotEqual(x, iv2.X);
            else;
                Assert.AreNotEqual(y, iv2.Y);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddTestFail() {
            CreateAll();
            Intersection t = new Intersection(iss, vectors, grid);
            t.Add(null);
        }
        [TestMethod]

        public void AddTest() {
            CreateAll();
            Intersection t = new Intersection(iss, vectors, grid);
            t.Add(new Car(grid));
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddTestFailCar()
        {
            CreateAll();
            Intersection t = new Intersection(iss, vectors, grid);
            t.Add(new Car(gridFail));
        }
        [TestMethod]

        public void EnumeratorTest() {
            CreateAll();
            Intersection t = new Intersection(iss, vectors, grid);
            IVehicle car = new Car(grid2);
            t.Add(car);
            IEnumerator ie = t.GetEumerator();
            ie.MoveNext();
            IVehicle iv = (IVehicle)ie.Current;
            Assert.AreEqual(iv.EmissionIdle, car.EmissionIdle);
            Assert.AreEqual(iv.EmissionMoving, car.EmissionMoving);
            Assert.AreEqual(iv.X, car.X);
            Assert.AreEqual(iv.Y, car.Y);
            Assert.AreEqual(iv.Direction, car.Direction);
            Assert.AreEqual(iv.Passengers, car.Passengers);
        }
    }
}
