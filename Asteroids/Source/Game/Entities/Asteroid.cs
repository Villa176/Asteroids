using Microsoft.Xna.Framework;
using System;

namespace Asteroids
{
    class Asteroid : Entity
    {
        private float BORDER_LIMIT = 23f;

        private bool spin;
        private float spinSpeed;
        private bool despawn;
        private Timer timer;
        private float radius;
        private bool isOnScreen;

        public bool IsOnScreen
        {
            get { return isOnScreen; }
            set { isOnScreen = value; }
        }

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }

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

        public Asteroid(float x, float y, int numOfVertices, float _radius, Color color) : base(x, y, numOfVertices, _radius, color, false)
        {
            Random rand = new Random();

            despawn = false;
            isOnScreen = false;
            timer = new Timer(20000);
            
            spin = true;
            float spinDirection = rand.Next(10) < 5 ? -1f : 1f; 
            spinSpeed = (float)rand.NextDouble() * spinDirection;

            scale = 1f;
            radius = _radius;
            BORDER_LIMIT /= scale;

            angle = (float)(rand.NextDouble() * 2 * Math.PI);
            speed = (float)(rand.NextDouble() * 2 + 2) / scale;
            direction = new Vector3(-MathF.Sin(angle), MathF.Cos(angle), 0);
        }

        public void Update()
        {
            if (despawn && !isOnScreen || health <= 0) return;
            timer.Update();
            if (spin)
            {
                float fElapsedTime = (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

                position += direction * speed * fElapsedTime;
                angle += spinSpeed * fElapsedTime;

                if (angle > 2f * MathF.PI) angle -= 2f * MathF.PI;
                else if (angle < -2f * MathF.PI) angle += 2f * MathF.PI;
            }

            // check bounds
            isOnScreen = true;

            if (position.X > BORDER_LIMIT + radius)
            {
                if (!despawn) { position.X = -BORDER_LIMIT - radius; }
                else isOnScreen = false;
            }
            else if (position.X < -BORDER_LIMIT - radius)
            {
                if (!despawn) { position.X = BORDER_LIMIT + radius; }
                else isOnScreen = false;
            }

            if (position.Y > BORDER_LIMIT + radius)
            {
                if (!despawn) { position.Y = -BORDER_LIMIT - radius; }
                else isOnScreen = false;
            }
            else if (position.Y < -BORDER_LIMIT - radius)
            {
                if (!despawn) { position.Y = BORDER_LIMIT + radius; }
                else isOnScreen = false;     
            }

            if (timer.TimeReached) despawn = true;
        }

        public override void Draw()
        {
            if (despawn && !isOnScreen || health <= 0) return;
            base.Draw();
        }
    }
}
