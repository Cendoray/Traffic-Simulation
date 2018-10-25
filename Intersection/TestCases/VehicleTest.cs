using System;
using TrafficIntersection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCases
{
    [TestClass]
    public class VehicleTest
    {
        ISignalStrategy issFail;
        int[] issInt;
        ISignalStrategy iss;
        Tile[,] tile;
        Tile[,] tile2;
        Grid grid;
        Grid grid2;
        Grid gridFail;

        private void CreateAll() { 
           this.issFail = null;
           this.issInt = new int[] {3,3,3,3};
           this.iss = new FixedSignal(issInt);
            tile = new Tile[,] { { new Grass(), new Road(Direction.Down), new Road(Direction.Down), new Grass() },
                                            { new Road(Direction.Right), new IntersectionTile(),  new IntersectionTile(), new Road(Direction.Right) },
                                            { new Road(Direction.Right), new Road(Direction.Down), new Road(Direction.Down), new Road(Direction.Right) },
                                             {new Grass(), new Road(Direction.Down), new Road(Direction.Down), new Grass() } };
            tile2 = new Tile[,] { { new IntersectionTile(), new IntersectionTile(), new IntersectionTile(), new Road(Direction.Down) },
                                             { new IntersectionTile(), new IntersectionTile(), new IntersectionTile(), new Road(Direction.Right) },
                                             {  new IntersectionTile(), new IntersectionTile(), new IntersectionTile(), new  Road(Direction.Right) },
                                             { new Road(Direction.Down), new Road(Direction.Down), new Road(Direction.Down), new Grass() } }; this.grid = new Grid(tile);
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
            Assert.AreEqual(vehicle.Passengers, vehicle2.Passengers);
            Assert.AreEqual(vehicle.Direction, vehicle2.Direction);
            Assert.AreEqual(vehicle.X, vehicle2.Y);
            Assert.AreEqual(vehicle.EmissionMoving, vehicle2.EmissionMoving);
            Assert.AreEqual(vehicle.EmissionIdle, vehicle2.EmissionIdle);
        }


        [TestMethod]
        public void TestConstructorCarFail()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Vehicle vehicle2 = new Car(grid2);
            Assert.AreEqual(vehicle.Passengers, vehicle2.Passengers);
            Assert.AreEqual(vehicle.Direction, vehicle2.Direction);
            Assert.AreEqual(vehicle.X, vehicle2.Y);
            Assert.AreEqual(vehicle.EmissionMoving, vehicle2.EmissionMoving);
            Assert.AreEqual(vehicle.EmissionIdle, vehicle2.EmissionIdle);
        }


        [TestMethod]
        public void TestConstructorMotorcycle()
        {
            CreateAll();
            Vehicle vehicle = new Motorcycle(grid);
            Vehicle vehicle2 = new Motorcycle(grid);
            Assert.AreEqual(vehicle.Passengers, vehicle2.Passengers);
            Assert.AreEqual(vehicle.Direction, vehicle2.Direction);
            Assert.AreEqual(vehicle.X, vehicle2.Y);
            Assert.AreEqual(vehicle.EmissionMoving, vehicle2.EmissionMoving);
            Assert.AreEqual(vehicle.EmissionIdle, vehicle2.EmissionIdle);
        }

        [TestMethod]
        public void TestConstructorMotorcycleFail()
        {
            CreateAll();
            Vehicle vehicle = new Motorcycle(grid);
            Vehicle vehicle2 = new Motorcycle(grid2);
            Assert.AreEqual(vehicle.Passengers, vehicle2.Passengers);
            Assert.AreEqual(vehicle.Direction, vehicle2.Direction);
            Assert.AreEqual(vehicle.X, vehicle2.Y);
            Assert.AreEqual(vehicle.EmissionMoving, vehicle2.EmissionMoving);
            Assert.AreEqual(vehicle.EmissionIdle, vehicle2.EmissionIdle);
        }


        [TestMethod]
        public void TestIntersectionFalse()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Assert.AreEqual(vehicle.InIntersection(), false);
        }

        [TestMethod]
        public void TestIntersectionTrue()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid2);
            Assert.AreEqual(vehicle.InIntersection(), true);
        }


        [TestMethod]
        public void NextIsIntersectionFalse()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            Assert.AreEqual(vehicle.InIntersection(), false);
        }

        [TestMethod]
        public void NextIsIntersectionTrue()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid2);
            Assert.AreEqual(vehicle.InIntersection(), true);
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void MoveTestFail()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            vehicle.Move(issFail);
        }

        [TestMethod]
        public void MoveTest()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            vehicle.X = 0;
            vehicle.Y = 1;
            vehicle.Direction = Direction.Down;
            vehicle.Move(iss);
            Assert.AreEqual(vehicle.X, 0);
            Assert.AreEqual(vehicle.Y, 2);
        }

        [TestMethod]
        public void MoveTestDone()
        {
            CreateAll();
            Vehicle vehicle = new Car(grid);
            vehicle.X = 0;
            vehicle.Y = 1;
            vehicle.Direction = Direction.Down;
            vehicle.Move(iss);
            vehicle.Move(iss);
            vehicle.Move(iss);
            vehicle.Move(iss);
            vehicle.Move(iss);
            vehicle.Move(iss);
            Assert.AreEqual(vehicle.Direction, Direction.None);

        }

        [TestMethod]
        public void TestXWin()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            v.X = 2;
            int x = v.X;
            Assert.AreEqual(x, v.X);
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestXFail()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            v.X = -2;
            int x = v.X;
            Assert.AreEqual(x, v.X);
        }

        [TestMethod]
        public void TestYWin()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            v.Y = 2;
            int x = v.Y;
            Assert.AreEqual(x, v.Y);
        }


        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestYFail()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            v.Y = -5;
            int x = v.Y;
            Assert.AreEqual(x, v.Y);
        }

        [TestMethod]
        public void TestMoveInIntersection()
        {
            CreateAll();
            Vehicle v = new Car(grid2);
            v.X = 0;
            v.Y = 1;
            int x = v.X;
            int y = v.Y;
            v.Direction = Direction.Down;
            v.Move(iss);
            v.Move(iss);
            Assert.AreEqual(y+2, v.Y);
            Assert.AreEqual(x, v.X);
        }

        [TestMethod]

        public void TestMoveCarInfront()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            Vehicle vv = new Car(grid);
            v.X = 1;
            v.Y = 0;
            int x = v.X;
            int y = v.Y;
            vv.X = 1;
            vv.Y = 1;
            v.Direction = Direction.Down;
            vv.Direction = Direction.Down;
            ISignalStrategy iss2 = new FixedSignal(issInt);
            for (int i = 0; i < issInt[0]; i++)
                iss2.Update();
            v.Move(iss);
            v.Move(iss);
            Assert.AreEqual(y, v.Y);
            Assert.AreEqual(x, v.X);
        }

        [TestMethod]

        public void TestMoveRedLight()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            v.X = 1;
            v.Y = 0;
            int x = v.X;
            int y = v.Y;
            v.Direction = Direction.Down;
            ISignalStrategy iss2 = new FixedSignal(issInt);
            for (int i = 0; i < issInt[0]; i++)
                iss2.Update();
            v.Move(iss);
            v.Move(iss);
            Assert.AreEqual(y, v.Y);
            Assert.AreEqual(x, v.X);
        }

        [TestMethod]

        public void TestMoveInIntersectionCarInfront()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            Vehicle vv = new Car(grid);
            v.X = 1;
            v.Y = 1;
            int x = v.X;
            int y = v.Y;
            vv.X = 1;
            vv.Y = 2;
            v.Direction = Direction.Down;
            vv.Direction = Direction.Down;
            ISignalStrategy iss2 = new FixedSignal(issInt);
            for (int i = 0; i < issInt[0]; i++)
                iss2.Update();
            v.Move(iss);
            v.Move(iss);
            Assert.AreEqual(y, v.Y);
            Assert.AreEqual(x, v.X);
        }
        [TestMethod]

        public void TestMoveBeforeLimit() {
            CreateAll();
            Vehicle v = new Car(grid);
            v.X = 1;
            v.Y = 1;
            int x = v.X;
            int y = v.Y + 1;
            v.Direction = Direction.Down;
            v.Move(iss);
            Assert.AreEqual(y, v.Y);
            Assert.AreEqual(x, v.X);
        }
        [TestMethod]

        public void TestMoveLimit()
        {
            CreateAll();
            Vehicle v = new Car(grid);
            v.X = 1;
            v.Y = 3;
            int x = v.X;
            int y = v.Y;
            v.Direction = Direction.Down;
            v.Move(iss);
            Assert.AreEqual(y, v.Y);
            Assert.AreEqual(x, v.X);
        }
    }
}
