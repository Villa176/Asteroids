using Microsoft.Xna.Framework;
using System;

namespace Asteroids
{
    class Asteroid : Entity
    {
        private bool spin;
        private float spinSpeed;
        private bool despawn;
        private Timer timer;

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

        public float SpinSpeed
        {
            get { return spinSpeed; }
            set { spinSpeed = value; }
        }

        public bool Spin
        {
            get { return spin; }
            set { spin = value; }
        }

        public Asteroid(float x, float y, int numOfVertices, int radius, Color color) : base(x, y, numOfVertices, radius, color,false)
        {
            Random rand = new Random();
            
            spin = true;
            spinSpeed = (float)rand.NextDouble();
            speed = (float)(rand.NextDouble() * 2 + 1);

            angle = (float)(rand.NextDouble() * 2 * Math.PI);
            direction = new Vector3(-MathF.Sin(angle), MathF.Cos(angle), 0);
        }

        public void Update()
        {
            if (spin)
            {
                float fElapsedTime = (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

                position += direction * speed * fElapsedTime;
                angle += spinSpeed * fElapsedTime;

                if (angle > 2f * MathF.PI) angle -= 2f * MathF.PI;
                else if (angle < -2f * MathF.PI) angle += 2f * MathF.PI;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
