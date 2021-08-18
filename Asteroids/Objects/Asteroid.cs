using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids.Objects
{
    class Asteroid : SpaceObject
    {
        public float Radius { get; set; }
        public Asteroid(float radius, float x, float y, float angle, Color color) : base(x, y, angle, color)
        {
            Radius = radius;
        }

        public void Initialize(int vertexCount, GraphicsDeviceManager graphicsDeviceManager, BasicEffect basicEffect)
        {
            gDeviceManager = graphicsDeviceManager;
            bEffect = basicEffect;
            VertexCount = vertexCount;

            Random rand = new Random();

            float angleIncrement = (2 * MathF.PI) / vertexCount;
            VertexPositionColor[] vertices = new VertexPositionColor[vertexCount + 1];
            for (int i = 0; i < vertexCount + 1; i++)
            {
                if (i == 0) vertices[i] = new VertexPositionColor(new Vector3(0, 0, 0), Color);
                else vertices[i] = new VertexPositionColor(new Vector3(-MathF.Sin(angleIncrement * (float)(i - 1)), MathF.Cos(angleIncrement * (float)(i - 1)), 0) * ((float)rand.NextDouble() * 1f + (Radius - 1f)), Color);
            }
            vertexBuffer = new VertexBuffer(gDeviceManager.GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            int[] indices = new int[vertexCount * 3];
            for (int i = 0; i < vertexCount; i++)
            {
                indices[i * 3] = i + 1;
                indices[i * 3 + 1] = 0;

                if (i == vertexCount - 1)
                    indices[i * 3 + 2] = indices[0];
                else
                    indices[i * 3 + 2] = i + 2;

            }
            indexBuffer = new IndexBuffer(gDeviceManager.GraphicsDevice, typeof(int), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        public void Update(float fElapsedTime)
        {

        }
    }
}
