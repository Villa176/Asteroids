using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids
{
    class Helpers
    {
        public static bool CirclesIntersect(Vector3 p1, float r1, Vector3 p2, float r2)
        {
            return Vector3.Distance(p1, p2) < (r1 + r2);
        }

        public static bool NotOnScreen(Vector3 position, float offset = 0f)
        {
            float WIDTH = Globals.SCREEN_WIDTH / 2 + offset;
            float HEIGHT = Globals.SCREEN_HEIGHT / 2 + offset;

            return (position.X > WIDTH || position.X < -WIDTH || position.Y > HEIGHT || position.Y < -HEIGHT);
        }
    }
}
