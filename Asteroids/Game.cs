using Asteroids.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Asteroids
{
    public class Game : Microsoft.Xna.Framework.Game
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
        private List<Asteroid> asteroids;
        private List<Bullet> bullets;
        private bool fire = false;

        public Game()
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

            ship = new Ship(0, 0, 0, SPACE_WHITE);
            ship.Initialize(_graphics, basicEffect);

            bullets = new List<Bullet>();

            Random rand = new Random();
            asteroids = new List<Asteroid>();
            for (int i = 0; i < 5; i++)
            {
                float px = rand.Next(51) >= 25 ? -1f : 1f;
                float py = rand.Next(51) >= 25 ? -1f : 1f;
                asteroids.Add(new Asteroid(5, px * ((float)(rand.NextDouble()) * 25f + 25f), py * ((float)(rand.NextDouble()) * 25f + 30f), -1f * px, -1f * py, 0, SPACE_WHITE));
                asteroids[^1].Initialize(7, _graphics, basicEffect);
            }
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

            for (int i = asteroids.Count - 1; i >= 0; i--)
            {
                asteroids[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
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

            foreach (Asteroid asteroid in asteroids)
            {
                asteroid.Draw();
            }

            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, bullets.Count > 0 ? bullets[0].Position.Y.ToString() : "n/a", new Vector2(0, 0), Color.Red);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
