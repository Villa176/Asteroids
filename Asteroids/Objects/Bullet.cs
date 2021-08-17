using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids.Objects
{
    class Bullet : SpaceObject
    {
        public Bullet(float x, float y, float angle, Color color) : base(x, y, angle, color)
        {
            Direction = new Vector3(-MathF.Sin(Angle), MathF.Cos(Angle), 0);
            Speed = 15f;
        }

        public void Initialize(GraphicsDeviceManager graphicsDeviceManager, BasicEffect basicEffect)
        {
            gDeviceManager = graphicsDeviceManager;
            bEffect = basicEffect;
            VertexCount = 2;

            float zindex = 0;

            VertexPositionColor[] vertices = new VertexPositionColor[4];
            vertices[0] = new VertexPositionColor(new Vector3(-0.05f, 0.15f, zindex), Color);
            vertices[1] = new VertexPositionColor(new Vector3(0.05f, 0.15f, zindex), Color);
            vertices[2] = new VertexPositionColor(new Vector3(0.05f, -0.15f, zindex), Color);
            vertices[3] = new VertexPositionColor(new Vector3(-0.05f, -0.15f, zindex), Color);
            vertexBuffer = new VertexBuffer(gDeviceManager.GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            short[] indices = new short[6];
            indices[0] = 0; indices[1] = 1; indices[2] = 2;
            indices[3] = 0; indices[4] = 2; indices[5] = 3;
            indexBuffer = new IndexBuffer(gDeviceManager.GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        public void Update(float fElapsedTime)
        {
            Position += Direction * Speed * fElapsedTime;
        }
    }
}
