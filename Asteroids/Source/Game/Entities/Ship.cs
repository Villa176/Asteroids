using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Asteroids
{
    class Ship : Entity
    {
        private const float MAX_SPEED = 0.3f;
        private const float ACCELERATION = 0.1f;
        private const float TURN_SPEED = 2.5f;
        private List<Bullet> bullets;

        public Ship(float x, float y)  : base(x, y)
        {
            CreateShip();
            bullets = new List<Bullet>();
            Scale = 0.5f;
            Initialized = true;
        }

        private void CreateShip()
        {
            vertexCount = 3;

            VertexPositionColor[] vertices = new VertexPositionColor[3];
            vertices[0] = new VertexPositionColor(new Vector3(0, 1f, 0), Globals.SPACE_WHITE);
            vertices[1] = new VertexPositionColor(new Vector3(0.5f, -0.5f, 0), Globals.SPACE_WHITE);
            vertices[2] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0), Globals.SPACE_WHITE);

            vertexBuffer = new VertexBuffer(Globals.graphicsDevice.GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            
            short[] indices = new short[3];
            indices[0] = 0; indices[1] = 1; indices[2] = 2;

            vertexBuffer.SetData(vertices);
            indexBuffer = new IndexBuffer(Globals.graphicsDevice.GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        public void Update()
        {
            float fElapsedTime = (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

            if (Globals.keyboard.IsKeyPressed(Keys.Space))
            {
                bullets.Add(new Bullet(position.X * Scale, position.Y * scale, Globals.SPACE_WHITE, this));
            }

            if (Globals.keyboard.IsKeyHeld(Keys.W))
            {
                if (speed < MAX_SPEED) speed += ACCELERATION * fElapsedTime;
                direction = new Vector3(-MathF.Sin(angle), MathF.Cos(angle), 0);
                position += direction * speed;
            }
            else if (speed > 0)
            {
                speed -= 0.5f * ACCELERATION * fElapsedTime;
                direction = new Vector3(-MathF.Sin(angle), MathF.Cos(angle), 0);
                position += direction * speed;
            }

            if (Globals.keyboard.IsKeyHeld(Keys.A))
            {
                angle += TURN_SPEED * fElapsedTime;
            }

            if (Globals.keyboard.IsKeyHeld(Keys.D))
            {
                angle -= TURN_SPEED * fElapsedTime;
            }

            if (angle > 2f * MathF.PI) angle -= 2f * MathF.PI;
            else if (angle < -2f * MathF.PI) angle += 2f * MathF.PI;

            // update bullets
            for ( int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();

                if (bullets[i].Despawn)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }
        public override void Draw()
        {
            base.Draw();

            // draw bullets
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }
        }
    }

}
