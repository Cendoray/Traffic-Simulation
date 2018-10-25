using System;
using TrafficIntersection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCases
{
    [TestClass]
    public class GridTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            //Arrange
            Direction direction = Direction.None;
            Tile[,] tile2DArray = new Tile[4, 4] {
                { new Grass(), new Road(direction), new Grass() , new Grass()},                                    
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                 { new Grass(), new Road(direction), new Grass() , new Grass()}
            };

            //Act
            Grid g = new Grid(tile2DArray);

            //Assert
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestConstructorFail()
        {
            //Arrange
            Direction direction = Direction.None;
            Tile[,] tile2DArray = new Tile[2, 3] {
                { new Grass(), new Road(direction), new Grass() },
                { new Grass(), new Road(direction), new Grass() }
            };

            //Act
            Grid g = new Grid(tile2DArray);
            
            //Assert
        }

        
        [TestMethod]
        public void TestIsOccupiedTrue()
        {
            //Arrange
            Direction direction = Direction.None;
            Tile[,] tile2DArray = new Tile[4, 4] {
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                 { new Grass(), new Road(direction), new Grass() , new Grass()}
            };
            Grid g = new Grid(tile2DArray);
            g[0, 0].Occupied = true;

            //Act
            bool result = g.IsOccupied(0, 0);

            //Assert
            Assert.IsTrue(result == true);
        }

        [TestMethod]
        public void TestIsOccupiedFalse()
        {
            //Arrange
            Direction direction = Direction.None;
            Tile[,] tile2DArray = new Tile[4, 4] {
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                 { new Grass(), new Road(direction), new Grass() , new Grass()}
            };
            Grid g = new Grid(tile2DArray);
            g[1, 1].Occupied = false;

            //Act
            bool result = g.IsOccupied(1, 1);

            //Assert
            Assert.IsTrue(result == false);
        }

        [TestMethod]
        public void TestInBoundsTrue()
        {
            //Arrange
            Direction direction = Direction.None;
            Tile[,] tile2DArray = new Tile[4, 4] {
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                 { new Grass(), new Road(direction), new Grass() , new Grass()}
            };
            Grid g = new Grid(tile2DArray);

            //Act
            bool result = g.InBounds(2, 1);

            //Assert
            Assert.IsTrue(result == true);
        }

        [TestMethod]
        public void TestInBoundsFalse()
        {
            //Arrange
            Direction direction = Direction.None;
            Tile[,] tile2DArray = new Tile[4, 4] {
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                { new Grass(), new Road(direction), new Grass() , new Grass()},
                 { new Grass(), new Road(direction), new Grass() , new Grass()}
            };
            Grid g = new Grid(tile2DArray);

            //Act
            bool result = g.InBounds(4, 3);

            //Assert
            Assert.IsTrue(result == false);
        }
    }
}
