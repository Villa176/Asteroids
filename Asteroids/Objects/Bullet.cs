using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids.Objects
{
    class Bullet : SpaceObject
    {
        public Bullet(float x, float y, float angle, Color color) : base(x, y, angle, color)
        {
        }
    }
}
