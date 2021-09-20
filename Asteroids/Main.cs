using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Asteroids
{
    public class Asteroids : Game
    {
        GameWorld game_world;

        public Asteroids()
        {
            Globals.Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.Graphics.PreferredBackBufferWidth = Globals.SCREEN_WIDTH;
            Globals.Graphics.PreferredBackBufferHeight = Globals.SCREEN_HEIGHT;
            Globals.Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Globals.Graphics.SynchronizeWithVerticalRetrace = false;
            Globals.Graphics.ApplyChanges();

            Globals.Basic = new BasicEffect(Globals.Graphics.GraphicsDevice)
            {
                View =          Globals.VIEW_MATRIX,
                Projection =    Globals.PROJECTION_MATRIX,
                World =         Globals.WORLD_MATRIX,

                VertexColorEnabled = true
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.Batch = new SpriteBatch(Globals.Graphics.GraphicsDevice);
            Globals.Font = Content.Load<SpriteFont>(@"Fonts/GameFont");
            game_world = new GameWorld();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.KBInput.Update();

            game_world.Update(gameTime);

            Globals.KBInput.UpdatePrev();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Globals.Graphics.GraphicsDevice.Clear(Globals.SPACE_BLACK);

            game_world.Draw();

            base.Draw(gameTime);
        }
    }
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new Asteroids();
            game.Run();
        }
    }
}
