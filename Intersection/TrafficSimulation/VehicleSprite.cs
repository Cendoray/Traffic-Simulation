using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrafficIntersection;
using System.Collections;

namespace TrafficSimulation
{   /// <summary>
    /// Responsible for drawing the vehicles on the intersection 
    /// </summary>
    public class VehicleSprite : DrawableGameComponent
    {
        private Intersection intersection;
        private TrafficControl trafficControl;
        private Grid grid;

        private Texture2D carLeft;
        private Texture2D carRight;
        private Texture2D carUp;
        private Texture2D carDown;

        private Texture2D motorcycleLeft;
        private Texture2D motorcycleRight;
        private Texture2D motorcycleUp;
        private Texture2D motorcycleDown;

        private Game game;
        private SpriteBatch spriteBatch;
        private int counter;
        private int threshold;

        /// <summary>
        /// Constructor for a VehicleSprite object, initializes the Intersection and TrafficControl objects
        /// </summary>
        /// <param name="i">A reference to the Intersection class</param>
        /// <param name="t">A reference to the TrafficControl class</param>
        /// <param name="game">An object representing the game</param>
        public VehicleSprite(Game game, string file) : base(game)
        {
            this.trafficControl = new TrafficControl();
            this.trafficControl.Parse(file);
            this.grid = this.trafficControl.Grid;
            this.game = game;
        }

        /// <summary>
        /// Method that draws vehicles onto the intersection at the right location
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            IVehicle v;
            IEnumerator vehicleEnumerator = this.intersection.GetEumerator();

            while(vehicleEnumerator.MoveNext())
            {
                spriteBatch.Begin();
                v = (IVehicle)vehicleEnumerator.Current;
                this.intersection.Add(v);
                if(vehicleEnumerator.Current is Car)
                {
                    if (v.Direction == Direction.Up)
                        spriteBatch.Draw(carUp, new Vector2(v.X, v.Y), Color.White);
                    else if (v.Direction == Direction.Down)
                        spriteBatch.Draw(carDown, new Vector2(v.X, v.Y), Color.White);
                    else if (v.Direction == Direction.Right)
                        spriteBatch.Draw(carRight, new Vector2(v.X, v.Y), Color.White);
                    else if (v.Direction == Direction.Left)
                        spriteBatch.Draw(carLeft, new Vector2(v.X, v.Y), Color.White);
                }
                else if (vehicleEnumerator.Current is Motorcycle)
                {
                    if (v.Direction == Direction.Up)
                        spriteBatch.Draw(motorcycleUp, new Vector2(v.X, v.Y), Color.White);
                    else if (v.Direction == Direction.Down)
                        spriteBatch.Draw(motorcycleDown, new Vector2(v.X, v.Y), Color.White);
                    else if (v.Direction == Direction.Right)
                        spriteBatch.Draw(motorcycleRight, new Vector2(v.X, v.Y), Color.White);
                    else if (v.Direction == Direction.Left)
                        spriteBatch.Draw(motorcycleLeft, new Vector2(v.X, v.Y), Color.White);
                }
                else
                {
                    throw new Exception("This type of vehicle does not exist");
                }
                spriteBatch.End();
            }
        }

        /// <summary>
        /// Method that calls the update methods of the Intersection and TrafficControl at a certain speed
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            counter++;
            if (counter > 100)
            {
                this.trafficControl.Update();
                this.intersection.Update();
                counter = 0;
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Loads the required vehicle images into the project
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            carUp = game.Content.Load<Texture2D>("carUp");
            carDown = game.Content.Load<Texture2D>("carDown");
            carRight = game.Content.Load<Texture2D>("carRight");
            carLeft = game.Content.Load<Texture2D>("carLeft");

            motorcycleUp = game.Content.Load<Texture2D>("motorcycleUp");
            motorcycleDown = game.Content.Load<Texture2D>("motorcycleDown");
            motorcycleRight = game.Content.Load<Texture2D>("motorcycleRight");
            motorcycleLeft = game.Content.Load<Texture2D>("motorcycleLeft");

            base.LoadContent();
        }
    }
}
