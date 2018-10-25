using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrafficIntersection;

namespace TrafficSimulation
{
    /// <summary>
    /// Responsible for drawing the grid on the screen
    /// </summary>
    public class GridSprite : DrawableGameComponent
    {
        private Grid grid;
        private SpriteBatch spriteBatch;
        private Texture2D grass;
        private Texture2D road;
        private Texture2D intersectionTile;
        private Texture2D light;
        private GraphicsDevice graphics;
        private PresentationParameters presentation;
        Game game;
        string direction;
        string color;

        /// <summary>
        /// Class contructor
        /// </summary>
        public GridSprite(Game game, string str) : base(game)
        {
            this.game = game;
            TrafficControl tf = new TrafficControl();
            tf.Parse(str);
            this.grid = tf.Grid;
            this.presentation = new PresentationParameters();


        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grass = game.Content.Load<Texture2D>("grass");
            road = game.Content.Load<Texture2D>("road" + direction);
            intersectionTile = game.Content.Load<Texture2D>("intersection");
            light = game.Content.Load<Texture2D>(color);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Method responsible for drawing each grid tile
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            for (int i = 0; i < grid.Size; i++)
            {
                for (int j = 0; j < grid.Size; j++)
                {
                    if (grid[i, j] is Grass)
                    {
                        spriteBatch.Draw(grass, new Rectangle(j * 30, i * 30, 30, 30), Color.Black);
                    }
                    else if (grid[i, j] is Road)
                    {
                        spriteBatch.Draw(road, new Rectangle(j * 30, i * 30, 30, 30), Color.Black);
                        if (grid[i, j].Direction == Direction.Down)
                            direction = "down";
                        else if (grid[i, j].Direction == Direction.Up)
                            direction = "up";
                        else if (grid[i, j].Direction == Direction.Right)
                            direction = "right";
                        else if (grid[i, j].Direction == Direction.Left)
                            direction = "left";
                    }
                    else if (grid[i, j] is Light)
                    {
                        spriteBatch.Draw(light, new Rectangle(j * 30, i * 30, 30, 30), Color.Black);
                        if ((grid[i, j] as Light).Colour == Colour.Red)
                            color = "red";
                        else if ((grid[i, j] as Light).Colour == Colour.Green)
                            color = "green";
                        else if ((grid[i, j] as Light).Colour == Colour.Amber)
                            color = "yellow";
                    }
                    else if (grid[i, j] is IntersectionTile)
                    {
                        spriteBatch.Draw(intersectionTile, new Rectangle(j * 30, i * 30, 30, 30), Color.Black);
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
