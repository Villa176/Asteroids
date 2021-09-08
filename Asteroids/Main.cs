using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        private Game world;

        Matrix m_world = Matrix.CreateTranslation(0, 0, 0);
        Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 50), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), Globals.WINDOW_WIDTH / Globals.WINDOW_HEIGHT, 1f, 1000f);

        public Main()
        {
            Globals.graphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.graphicsDevice.PreferredBackBufferWidth = Globals.WINDOW_WIDTH;
            Globals.graphicsDevice.PreferredBackBufferHeight = Globals.WINDOW_HEIGHT;
            Globals.graphicsDevice.GraphicsProfile = GraphicsProfile.HiDef;
            Globals.graphicsDevice.SynchronizeWithVerticalRetrace = false;
            Globals.graphicsDevice.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.basicEffect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true,
                World = m_world,
                View = view,
                Projection = projection
            };
            Globals.keyboard = new CKeyboard();
            
            world = new Game();

            System.Diagnostics.Debug.WriteLine("[CONTENT LOADED]");
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.gameTime = gameTime;
            Globals.keyboard.Update();

            if (Globals.keyboard.IsKeyPressed(Keys.Escape))
                Exit();

            world.Update();

            Globals.keyboard.UpdatePrev();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Globals.SPACE_BLACK);

            world.Draw();

            base.Draw(gameTime);
        }
    }

    public static class Program
    {
        static void Main()
        {
            using var game = new Main();
            game.Run();
        }
    }
}
