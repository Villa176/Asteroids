using Asteroids.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids
{
    public class Asteroids : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        BasicEffect basicEffect;
        private SpriteBatch _spriteBatch;

        private const int WIN_WIDTH = 1200;//1024;
        private const int WIN_HEIGHT = 900;//768;

        private Color SPACE_BLACK = new Color(0, 10, 35);
        private Color SPACE_WHITE = new Color(235, 255, 250);

        Matrix world = Matrix.CreateTranslation(0, 0, 0);
        Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 30), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), WIN_WIDTH / WIN_HEIGHT, 0.01f, 100f);

        private Ship ship;
        private Asteroid asteroid;

        public Asteroids()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = WIN_WIDTH;
            _graphics.PreferredBackBufferHeight = WIN_HEIGHT;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            basicEffect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true,
                World = world,
                View = view,
                Projection = projection
            };

            ship = new Ship(10.5f, 0, 0, SPACE_WHITE);
            ship.Initialize(_graphics, basicEffect);

            asteroid = new Asteroid(0, 0, 0, SPACE_WHITE);
            asteroid.Initialize(8, _graphics, basicEffect);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ship.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(SPACE_BLACK);

/*            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;*/

            ship.Draw();
            asteroid.Draw();

            base.Draw(gameTime);
        }
    }
}
