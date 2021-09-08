using Microsoft.Xna.Framework;
using System;

namespace Asteroids
{
    class Projectile2D : Primitive2D
    {
        protected float speed;
        protected Vector3 direction;
        protected bool despawn;
        protected Timer timer;

        public Timer Timer
        {
            get { return timer; }
            private set { timer = value; }
        }

        public bool Despawn
        {
            get { return despawn; }
            set { despawn = value; }
        }

        public Vector3 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Projectile2D(float x, float y, float directionAngle, int timeAlive):base(x, y)
        {
            despawn = false;
            speed = 5f;
            angle = directionAngle;
            direction = new Vector3(-MathF.Sin(angle), MathF.Cos(angle), 0);
            timer = new Timer(timeAlive);
        }

        public virtual void Update()
        {
            if (despawn) return;

            timer.Update();

            position += direction * speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

            if (timer.TimeReached) despawn = true;

        }
    }
}
