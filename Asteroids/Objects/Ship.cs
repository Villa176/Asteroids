using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Asteroids.Objects
{
    class Ship : SpaceObject
    {
        public Ship(float x, float y, float angle, Color color) : base(x, y, angle, color)
        {
        }

        public void Initialize(GraphicsDeviceManager graphicsDeviceManager, BasicEffect basicEffect)
        {
            gDeviceManager = graphicsDeviceManager;
            bEffect = basicEffect;
            VertexCount = 1;

            float zindex = 0;

            VertexPositionColor[] vertices = new VertexPositionColor[3];
            vertices[0] = new VertexPositionColor(new Vector3(0, 0.5f, zindex), Color);
            vertices[1] = new VertexPositionColor(new Vector3(0.25f, -0.25f, zindex), Color);
            vertices[2] = new VertexPositionColor(new Vector3(-0.25f, -0.25f, zindex), Color);
            vertexBuffer = new VertexBuffer(gDeviceManager.GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            short[] indices = new short[3];
            indices[0] = 0; indices[1] = 1; indices[2] = 2;
            indexBuffer = new IndexBuffer(gDeviceManager.GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        public void Update(float fElapsedTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (Speed < 10f)
                    Speed += 0.05f;
                Direction = new Vector3(-MathF.Sin(Angle), MathF.Cos(Angle), 0);
                Position += Direction * Speed * fElapsedTime;
            }
            else
            {
                if (Speed > 0)
                {
                    Speed -= 0.02f;
                    Position += Direction * Speed * fElapsedTime;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Angle += 2.5f * fElapsedTime;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Angle -= 2.5f * fElapsedTime;
            }

            if (Angle > 2 * MathF.PI) Angle -= 2 * MathF.PI;
            else if (Angle < -2 * MathF.PI) Angle += 2 * MathF.PI;
        }

    }
}
