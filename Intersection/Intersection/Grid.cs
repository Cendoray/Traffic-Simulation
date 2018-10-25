using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficIntersection
{
    public class Grid
    {
        private Tile[,] grid;

        /// <summary>
        /// Constructor for a Grid object, makes a deep copy of the Tile[,]
        /// </summary>
        /// <param name="grid">2d Tile array that represents a grid</param>
        public Grid(Tile[,] grid)
        {
            if (grid.GetLength(0) != grid.GetLength(1))
            {
                throw new ArgumentException("The grid must be a square! \nGrid height: " + grid.GetLength(0) + "\nGrid width: " + grid.GetLength(1));
            }

            if (grid.GetLength(0) < 4)
            {
                throw new ArgumentException("Grid is too small! Grid cannot be " + grid.GetLength(0) + "x" + grid.GetLength(1));
            }

            this.grid = new Tile[grid.GetLength(0), grid.GetLength(1)];

            for(int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    this.grid[i, j] = grid[i, j];
                }
            }
            Size = grid.GetLength(0) - 1;
        }

        /// <summary>
        /// Indexer that allows elements in a Grid to be accessed using array syntax
        /// </summary>
        /// <param name="x">row value</param>
        /// <param name="y"> column value</param>
        /// <returns>A Tile</returns>
        public Tile this[int x, int y]
        {
            get
            {
                if(this.InBounds(x,y))
                    return grid[y, x];
                throw new ArgumentException("Location out of the grid");
            }
            private set
            {
                grid[y, x] = value;
            }
        }

        /// <summary>
        /// Automatic property for Size, can get or set the value
        /// </summary>
        public int Size
        {
            get {return this.grid.GetLength(0); }
            private set { Size = value ; }
        }

        /// <summary>
        /// Method that takes two ints representing a coordinate and 
        /// checks if there is anything on the grid at that coordinate
        /// </summary>
        /// <param name="x">row value</param>
        /// <param name="y">column value</param>
        /// <returns>A bool value</returns>
        public bool IsOccupied(int x, int y)
        {
            return grid[x, y].Occupied;
        }

        /// <summary>
        /// Method that takes to ints representing a coordinate and
        /// checks if it is within the boundaries of the grid
        /// </summary>
        /// <param name="x">row value</param>
        /// <param name="y">column value</param>
        /// <returns>A bool value</returns>
        public bool InBounds(int x, int y)
        {
            if(y < this.grid.GetLength(0) && x < this.grid.GetLength(1) && x >= 0 && y >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
