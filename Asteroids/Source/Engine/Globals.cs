using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids
{
    public delegate void ObjectDelegate(object obj);


    class Globals
    {
        public static ObjectDelegate AddProjectiles;
        public static ObjectDelegate AddEntities;
        public static ObjectDelegate UpdatePlayerScore;

        public static GraphicsDeviceManager Graphics;
        public static BasicEffect Basic;
        public static SpriteBatch Batch;
        public static KeyboardInput KBInput = new KeyboardInput();

        public static SpriteFont Font;

        #region DISPLAY_SIZE
        public static int SCREEN_WIDTH = 900;
        public static int SCREEN_HEIGHT = 700;
        #endregion
        #region COLORS
        public static Color SPACE_BLACK = new Color(5, 10, 25);
        public static Color SPACE_WHITE = new Color(235, 245, 235);
        public static Color SPACE_RED = new Color(255, 105, 105);
        #endregion
        #region CAMERA_MATRICES
        public static Matrix VIEW_MATRIX = Matrix.CreateLookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
        public static Matrix PROJECTION_MATRIX = Matrix.CreateOrthographic(SCREEN_WIDTH, SCREEN_HEIGHT, 1, 2);
        public static Matrix WORLD_MATRIX = Matrix.CreateTranslation(Vector3.Zero);
        #endregion
    }
}
