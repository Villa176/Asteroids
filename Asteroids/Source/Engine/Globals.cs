#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
#endregion
namespace Asteroids
{
    class Globals
    {
        public static ContentManager content;
        public static SpriteBatch spriteBatch;

        public static BasicEffect basicEffect;
        public static GraphicsDeviceManager graphicsDevice;

        public static CKeyboard keyboard;

        public static GameTime gameTime;

        public static int WINDOW_WIDTH = 1200;
        public static int WINDOW_HEIGHT = 900;

        public static Color SPACE_BLACK = new Color(0, 10, 30);
        public static Color SPACE_WHITE = new Color(195, 205, 200);
        public static Color HIGHLIGHT_YELLOW = new Color(255, 255, 150);
    }
}
