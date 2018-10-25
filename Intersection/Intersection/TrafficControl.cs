using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrafficIntersection
{
    public class TrafficControl
    {
        //Vehicle v = new Vehicle(bla bla bla);
        //lots of vehicles vroom vroom
        //v.Done += t.UpdatePassengers;

        private Random random;

        public Intersection Intersection { get; private set; }
        public Grid Grid { get; private set; }
        public Total Total { get; private set; }
        private int counter;
        private int delay;
        private int totalVehicles;
        private int percentCars;
        private int percentElectric;

        public TrafficControl()
        {
            this.random = new Random();
            counter = 0;
        }

        /// <summary>
        /// Instantiates new vehicles and adds them to the intersection.
        /// </summary>
        /// Authored by Shuey
        public void Update()
        {
            int randomInt;
            Vehicle v;
            counter++;

            for (int i = 0; i < totalVehicles; i++)
            {
                if (counter % delay == 0)
                {
                    randomInt = random.Next(0, 100);

                    if (randomInt >= 0 && randomInt <= percentCars)
                    {
                        v = new Car(Grid);
                        Intersection.Add(v);
                    }
                    else if (randomInt >= percentCars + 1 && randomInt <= percentCars + percentElectric)
                    {
                        v = new Car(Grid);
                        Electric e = new Electric(v);
                        Intersection.Add(e);
                    } else
                    {
                        v = new Motorcycle(Grid);
                        Intersection.Add(v);
                    }
                    v.Moving += Total.updateEmission;
                    v.Waiting += Total.updateEmission;
                    v.Done += Total.updateEmission;
                    v.Moving += Total.updatePassengers;
                    v.Waiting += Total.updatePassengers;
                    v.Done += Total.updatePassengers;
                }
            }
        }

        /// <summary>
        /// Takes a string that was read from a file and parses it.
        /// Assumes file was read as a whole string and then split on new lines (\n)
        /// Proper file format:
        ///     [Total number of vehicles]
        ///     [delay variable]
        ///     [percent cars]
        ///     [percent electric]
        ///     [timing of lights: [[green left/right] [amber left/right] [green up/down] [amber up/down]]]
        ///     [square grid 4x4 or larger]
        /// </summary>
        /// Authored by Thomas
        /// <param name="fileContent">File content given to the method</param>
        public void Parse(string fileContent)
        {
            int count = 0;
            int numRows = 5;
            if (fileContent == null)
            {
                throw new ArgumentException("String passed to this method was null");
            }
            string[] lines = File.ReadAllLines(fileContent);
            // Exception thrown if file is too short
            if (lines.Length < 5)
            {
                throw new ArgumentException("Not enough lines in file.\nMake sure it is formatted correctly");
            }
            String[] fileLines = new String[lines.GetLength(0) - 5];
            for (int i = 5; i < lines.GetLength(0); i++) {
                fileLines[count] = lines[i];
                count++;
            }


            // Variable to keep track of which line is being interpreted.
            // Allows error message to specify which line in the input file string is causing a problem.
            // Incremented right before each line is used
            int fileLinePointer = 0;
            // Tries interpreting individual lines in the file
            try
            {
                fileLinePointer++;
                totalVehicles = Convert.ToInt32(fileLines[0]);
                Total = new Total(totalVehicles);
                fileLinePointer++;
                delay = Convert.ToInt32(fileLines[1]);
                if (delay < 2 || delay > 10)
                {
                    throw new ArgumentException("Delay is too large (" + delay + "); must be a value from 2 to 10");
                }
                fileLinePointer++;
                percentCars = Convert.ToInt32(fileLines[2]);
                fileLinePointer++;
                percentElectric = Convert.ToInt32(fileLines[3]);
                if (percentCars + percentElectric > 100)
                {
                    throw new ArgumentException("Percentages too large (combined they shouldn't be more than 100%)");
                }
                fileLinePointer++;
                string[] timing = fileLines[4].Split(' ');
                ISignalStrategy st = new FixedSignal(Convert.ToInt32(timing[0]), Convert.ToInt32(timing[1]), Convert.ToInt32(timing[2]), Convert.ToInt32(timing[3]));

                fileLinePointer++;
                Tile[,] tiles = new Tile[fileLines.Length - 5, fileLines[5].Split(' ').Length];
                List<Vector2> startCoords = new List<Vector2>();
                // Interprets grid
                for (int i = 5; i < fileLines.Length; i++)
                {
                    string[] t = fileLines[i].Split();
                    for (int j = 0; j < t.Length; j++)
                    {
                        // Ignores extra spaces or extra new lines or empty characters
                        if (t[j].CompareTo("") == 0 || t[j].CompareTo(" ") == 0 || t[j].CompareTo(" \n") == 0)
                        {
                            continue;
                        }
                        else if (t[j].ToLower().CompareTo("g") == 0)
                        {
                            tiles[i - numRows, j] = new Grass();
                        }
                        else if (t[j].ToLower().CompareTo("d") == 0)
                        {
                            tiles[i - numRows, j] = new Road(Direction.Down);

                            if (i == 0)
                            {
                                startCoords.Add(new Vector2((float)(j), (float)(i - 5)));
                            }
                        }
                        else if (t[j].ToLower().CompareTo("u") == 0)
                        {
                            tiles[i - numRows, j] = new Road(Direction.Up);

                            if (i == tiles.GetLength(0) - 1)
                            {
                                startCoords.Add(new Vector2((float)(j), (float)(i - 5)));
                            }
                        }
                        else if (t[j].ToLower().CompareTo("r") == 0)
                        {
                            tiles[i - numRows, j] = new Road(Direction.Right);

                            if (j == 0)
                            {
                                startCoords.Add(new Vector2((float)(j), (float)(i - 5)));
                            }
                        }
                        else if (t[j].ToLower().CompareTo("l") == 0)
                        {
                            tiles[i - numRows, j] = new Road(Direction.Left);
                            if (j == t.Length - 1)
                            {
                                startCoords.Add(new Vector2((float)(j), (float)(i - 5)));
                            }
                        }
                        else if (t[j].ToLower().CompareTo("i") == 0)
                        {
                            tiles[i - numRows, j] = new IntersectionTile();
                        }
                        else if (t[j].CompareTo("1") == 0)
                        {
                            tiles[i - numRows, j] = new Light(st, Direction.Down);
                        }
                        else if (t[j].CompareTo("2") == 0)
                        {
                            tiles[i - numRows, j] = new Light(st, Direction.Left);
                        }
                        else if (t[j].CompareTo("3") == 0)
                        {
                            tiles[i - numRows, j] = new Light(st, Direction.Right);
                        }
                        else if (t[j].CompareTo("4") == 0)
                        {
                            tiles[i - numRows, j] = new Light(st, Direction.Up);
                        }
                        else
                        {
                            throw new ArgumentException("Encountered unexpected value in input string on line " + fileLinePointer
                                + "\nValue: " + t[j]);
                        }
                    }

                    fileLinePointer++;
                }
                Grid = new Grid(tiles);
                Intersection = new Intersection(st, startCoords, Grid);
            }
            catch (FormatException e)
            {
                throw new ArgumentException("Encountered unexpected value in input string on line " + fileLinePointer);
            }
        }
    }
}
