using Asteroids.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Asteroids
{
    public class Asteroids : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        BasicEffect basicEffect;
        private SpriteBatch _spriteBatch;

        private SpriteFont font;

        private const int WIN_WIDTH = 1200;//1024;
        private const int WIN_HEIGHT = 900;//768;

        private Color SPACE_BLACK = new Color(0, 10, 35);
        private Color SPACE_WHITE = new Color(235, 255, 250);

        Matrix world = Matrix.CreateTranslation(0, 0, 0);
        Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 30), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), WIN_WIDTH / WIN_HEIGHT, 0.01f, 100f);

        private Ship ship;
        private Asteroid asteroid;
        private List<Bullet> bullets;
        private bool fire = false;

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
            font = Content.Load<SpriteFont>("Font");

            basicEffect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true,
                World = world,
                View = view,
                Projection = projection
            };

            ship = new Ship(10.5f, 0, 0, SPACE_WHITE);
            ship.Initialize(_graphics, basicEffect);

            bullets = new List<Bullet>();

            asteroid = new Asteroid(5, 0, 0, 0, SPACE_WHITE);
            asteroid.Initialize(9, _graphics, basicEffect);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ship.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                fire = true;
            if (Keyboard.GetState().IsKeyUp(Keys.Space) && fire)
            {
                bullets.Add(new Bullet(ship.Position.X, ship.Position.Y, ship.Angle, SPACE_WHITE));
                bullets[^1].Initialize(_graphics, basicEffect);
                fire = false;
            }

            // TODO: Add a timer to bullets and asteroids if limit is reached they don't wrap around anymore
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                if (bullets[i].Position.X < -25f || bullets[i].Position.Y < -25f || bullets[i].Position.X > 25f || bullets[i].Position.Y > 25f)
                { 
                    bullets.RemoveAt(i);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(SPACE_BLACK);

            /*RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;*/
            ship.Draw();
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }
            asteroid.Draw();

            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, bullets.Count > 0 ? bullets[0].Position.Y.ToString() : "n/a", new Vector2(0, 0), Color.Red);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
