using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Remoting.Contexts;
using TrafficIntersection;

namespace TrafficSimulation
{
    /// <summary>
    /// Responsible for drawing the grid on the screen
    /// </summary>
    public class TotalSprite : DrawableGameComponent
    {
        private Game game;
        private SpriteFont fontEmission;
        private double emissionRate;
        private SpriteFont fontPassengers;
        private int passengers;
        private SpriteBatch spriteBatch;
        private Grid grid;
          /// <summary>
        /// Class contructor
        /// </summary>
        public TotalSprite(Game game, string str) : base(game)
        {
            this.game = game;
            TrafficControl tf = new TrafficControl();
            tf.Parse(str);
            this.grid = tf.Grid;
            this.emissionRate = tf.Total.Emissions;
            this.passengers = tf.Total.Passengers;
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Create a new SpriteBatch, which can be used to draw textures.
            fontEmission = game.Content.Load<SpriteFont>("EmissionTotal");
            fontPassengers = game.Content.Load<SpriteFont>("PassengersTotal");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Method responsible for drawing each grid tile
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(fontEmission, "Total Emission Rate : " + emissionRate.ToString(), new Vector2(grid.Size * 30 + 20, grid.Size * 30 + 20), Color.White);
            spriteBatch.DrawString(fontPassengers, "Total Passengers : " + passengers.ToString(), new Vector2(grid.Size * 30 + 60, grid.Size * 30 + 60), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
