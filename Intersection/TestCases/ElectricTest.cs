using System;
using TrafficIntersection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCases
{
    [TestClass]
    public class EletricTest
    {
        ISignalStrategy issFail;
        int[] issInt;
        ISignalStrategy iss;
        Tile[,] tile;
        Tile[,] tile2;
        Grid grid;
        Grid grid2;
        Grid gridFail;

        private void CreateAll()
        {
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
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestConstructorFail()
        {
            CreateAll();
            Vehicle vehicle = new Car(gridFail);
        }

        [TestMethod]
        public void TestConstructorCar()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Vehicle vehicle2 = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            Electric eVehicle2 = new Electric(vehicle2);
            Assert.AreEqual(eVehicle.Passengers, eVehicle2.Passengers);
            Assert.AreEqual(eVehicle.Direction, eVehicle2.Direction);
            Assert.AreEqual(eVehicle.X, eVehicle2.Y);
            Assert.AreEqual(eVehicle.EmissionMoving, eVehicle2.EmissionMoving);
            Assert.AreEqual(eVehicle.EmissionIdle, eVehicle2.EmissionIdle);
        }


        [TestMethod]
        public void TestConstructorCarFail()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Vehicle vehicle2 = new Car(grid2);
            Electric eVehicle = new Electric(vehicle);
            Electric eVehicle2 = new Electric(vehicle2);
            Assert.AreEqual(eVehicle.Passengers, eVehicle2.Passengers);
            Assert.AreEqual(eVehicle.Direction, eVehicle2.Direction);
            Assert.AreEqual(eVehicle.X, eVehicle2.Y);
            Assert.AreEqual(eVehicle.EmissionMoving, eVehicle2.EmissionMoving);
            Assert.AreEqual(eVehicle.EmissionIdle, eVehicle2.EmissionIdle);
        }


        [TestMethod]
        public void TestConstructorMotorcycle()
        {
            CreateAll();
            Vehicle vehicle = new Motorcycle(grid);
            Vehicle vehicle2 = new Motorcycle(grid);
            Electric eVehicle = new Electric(vehicle);
            Electric eVehicle2 = new Electric(vehicle2);
            Assert.AreEqual(eVehicle.Passengers, eVehicle2.Passengers);
            Assert.AreEqual(eVehicle.Direction, eVehicle2.Direction);
            Assert.AreEqual(eVehicle.X, eVehicle2.Y);
            Assert.AreEqual(eVehicle.EmissionMoving, eVehicle2.EmissionMoving);
            Assert.AreEqual(eVehicle.EmissionIdle, eVehicle2.EmissionIdle);
        }

        [TestMethod]
        public void TestConstructorMotorcycleFail()
        {
            CreateAll();
            Vehicle vehicle = new Motorcycle(grid);
            Vehicle vehicle2 = new Motorcycle(grid2);
            Electric eVehicle = new Electric(vehicle);
            Electric eVehicle2 = new Electric(vehicle2);
            Assert.AreEqual(eVehicle.Passengers, eVehicle2.Passengers);
            Assert.AreEqual(eVehicle.Direction, eVehicle2.Direction);
            Assert.AreEqual(eVehicle.X, eVehicle2.Y);
            Assert.AreEqual(eVehicle.EmissionMoving, eVehicle2.EmissionMoving);
            Assert.AreEqual(eVehicle.EmissionIdle, eVehicle2.EmissionIdle);
        }


        [TestMethod]
        public void TestIntersectionFalse()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            Assert.AreEqual(eVehicle.InIntersection(), false);
        }

        [TestMethod]
        public void TestIntersectionTrue()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid2);
            Electric eVehicle = new Electric(vehicle);
            Assert.AreEqual(eVehicle.InIntersection(), true);
        }


        [TestMethod]
        public void NextIsIntersectionFalse()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            Assert.AreEqual(eVehicle.InIntersection(), false);
        }

        [TestMethod]
        public void NextIsIntersectionTrue()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid2);
            Electric eVehicle = new Electric(vehicle);
            Assert.AreEqual(eVehicle.InIntersection(), true);
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void MoveTestFail()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            eVehicle.Move(issFail);
        }

        [TestMethod]
        public void MoveTest()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            eVehicle.X = 0;
            eVehicle.Y = 1;
            eVehicle.Direction = Direction.Down;
            eVehicle.Move(iss);
            Assert.AreEqual(eVehicle.X, 0);
            Assert.AreEqual(2, eVehicle.Y);
        }

        [TestMethod]
        public void MoveTestDone()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            eVehicle.X = 0;
            eVehicle.Y = 1;
            eVehicle.Direction = Direction.Down;
            eVehicle.Move(iss);
            eVehicle.Move(iss);
            eVehicle.Move(iss);
            eVehicle.Move(iss);
            eVehicle.Move(iss);
            eVehicle.Move(iss);
            Assert.AreEqual(eVehicle.Direction, Direction.None);

        }

        [TestMethod]
        public void TestXWin()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            eVehicle.X = 2;
            int x = eVehicle.X;
            Assert.AreEqual(x, eVehicle.X);
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestXFail()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            eVehicle.X = -5;
            int x = eVehicle.X;
            Assert.AreEqual(x, eVehicle.X);
        }

        [TestMethod]
        public void TestYWin()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            eVehicle.Y = 2;
            int x = eVehicle.Y;
            Assert.AreEqual(x, eVehicle.Y);
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestYFail()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            eVehicle.Y = -5;
            int x = eVehicle.Y;
            Assert.AreEqual(x, eVehicle.Y);
        }

        [TestMethod]
        public void TestMoveInIntersection()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid2);
            Electric eVehicle = new Electric(vehicle);
            eVehicle.X = 0;
            eVehicle.Y = 1;
            int x = eVehicle.X;
            int y = eVehicle.Y;
            eVehicle.Direction = Direction.Down;
            eVehicle.Move(iss);
            eVehicle.Move(iss);
            Assert.AreEqual(y + 2, eVehicle.Y);
            Assert.AreEqual(x, eVehicle.X);
        }

        [TestMethod]

        public void TestMoveCarInfront()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Vehicle vehicle2 = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            Electric eVehicle2 = new Electric(vehicle2);
            eVehicle.X = 0;
            eVehicle.Y = 1;
            int x = eVehicle.X;
            int y = eVehicle.Y;
            eVehicle2.X = 0;
            eVehicle2.Y = 2;
            eVehicle.Direction = Direction.Down;
            eVehicle2.Direction = Direction.Down;
            ISignalStrategy iss2 = new FixedSignal(issInt);
            for (int i = 0; i < issInt[0]; i++)
                iss2.Update();
            eVehicle.Move(iss);
            eVehicle.Move(iss);
            Assert.AreEqual(y, eVehicle.Y);
            Assert.AreEqual(x, eVehicle.X);
        }

        [TestMethod]

        public void TestMoveRedLight()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            Electric eVehicle = new Electric(v);
            eVehicle.X = 1;
            eVehicle.Y = 0;
            int x = eVehicle.X;
            int y = eVehicle.Y;
            eVehicle.Direction = Direction.Down;
            ISignalStrategy iss2 = new FixedSignal(issInt);
            for (int i = 0; i < issInt[0]; i++)
                iss2.Update();
            eVehicle.Move(iss);
            eVehicle.Move(iss);
            Assert.AreEqual(y, eVehicle.Y);
            Assert.AreEqual(x, eVehicle.X);
        }

        [TestMethod]

        public void TestMoveInIntersectionCarInfront()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Vehicle vehicle2 = new Car(grid);
            Electric eVehicle = new Electric(vehicle);
            Electric eVehicle2 = new Electric(vehicle2);
            eVehicle.X = 1;
            eVehicle.Y = 1;
            int x = eVehicle.X;
            int y = eVehicle.Y;
            eVehicle2.X = 1;
            eVehicle2.Y = 2;
            eVehicle.Direction = Direction.Down;
            eVehicle2.Direction = Direction.Down;
            eVehicle.Direction = Direction.Down;
            ISignalStrategy iss2 = new FixedSignal(issInt);
            for (int i = 0; i < issInt[0]; i++)
                iss2.Update();
            eVehicle.Move(iss);
            eVehicle.Move(iss);
            Assert.AreEqual(y, eVehicle.Y);
            Assert.AreEqual(x, eVehicle.X);
        }

        [TestMethod]

        public void TestMoveBeforeLimit()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            Electric ev = new Electric(v);
            ev.X = 1;
            ev.Y = 1;
            int x = ev.X;
            int y = ev.Y + 1;
            ev.Direction = Direction.Down;
            ev.Move(iss);
            Assert.AreEqual(y, ev.Y);
            Assert.AreEqual(x, ev.X);
        }
        [TestMethod]

        public void TestMoveLimit()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            Electric ev = new Electric(v);
            ev.X = 1;
            ev.Y = 3;
            int x = ev.X;
            int y = ev.Y;
            ev.Direction = Direction.Down;
            ev.Move(iss);
            Assert.AreEqual(y, ev.Y);
            Assert.AreEqual(x, ev.X);
        }
    }
}