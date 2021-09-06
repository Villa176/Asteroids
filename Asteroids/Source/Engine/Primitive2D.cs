using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids
{
    class Primitive2D
    {
        private bool isAlive;

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public Vector3 Position { get; set; }
        public float Scale { get; set; } = 1f;
        public float Angle { get; set; } = 0f;

        protected bool Initialized { get; set; } = false;

        protected int vertexCount;
        protected IndexBuffer indexBuffer;
        protected VertexBuffer vertexBuffer;

        public Primitive2D(float x, float y)
        {
            Position = new Vector3(x, y, 0);
            isAlive = true;
        }

        public Primitive2D(float x, float y, int numOfVertices, int radius, Color color, bool isUniformShape = true)
        {
            vertexCount = numOfVertices;
            Position = new Vector3(x, y, 0);

            if (vertexCount < 3) throw new Exception("Cannot create random primitive with < 3 vertices");
            GeneratePrimitiveShape(radius, color, isUniformShape);

            isAlive = true;
        }

        private void GeneratePrimitiveShape(int radius, Color color, bool isUniform)
        {
            Random rand = new Random();
            float angleInc = (2f * MathF.PI) / vertexCount;

            VertexPositionColor[] vertices = new VertexPositionColor[vertexCount + 1];
            for (int i = 0; i < vertexCount + 1; i++)
            {
                if (i == 0)
                {
                    vertices[i] = new VertexPositionColor(new Vector3(0, 0, 0), color);
                }
                else
                {
                    float randFloat = isUniform ? 1f : (float)rand.NextDouble() * 2f;
                    float offset = randFloat + radius;
                    vertices[i] = new VertexPositionColor(new Vector3(-MathF.Sin(angleInc * (float)(i - 1)), MathF.Cos(angleInc * (float)(i - 1)), 0) * offset, color);
                }
            }
            vertexBuffer = new VertexBuffer(Globals.graphicsDevice.GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            short[] indices = new short[vertexCount * 3];
            for (int i = 0; i < vertexCount; i++)
            {
                indices[i * 3] = (short)(i + 1);
                indices[i * 3 + 1] = 0;

                if (i == vertexCount - 1)
                    indices[i * 3 + 2] = indices[0];
                else
                    indices[i * 3 + 2] = (short)(i + 2);

            }
            indexBuffer = new IndexBuffer(Globals.graphicsDevice.GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);

            Initialized = true;
        }

        public virtual void Update()
        {
            if (!Initialized) return;
        }

        public virtual void Draw()
        {
            if (!Initialized) return;

            Globals.graphicsDevice.GraphicsDevice.SetVertexBuffer(vertexBuffer);
            Globals.graphicsDevice.GraphicsDevice.Indices = indexBuffer;

            Globals.basicEffect.World = Matrix.CreateFromYawPitchRoll(0, 0, Angle) * Matrix.CreateTranslation(Position) * Matrix.CreateScale(Scale);

            foreach (EffectPass pass in Globals.basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }

            Globals.graphicsDevice.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexCount);
        }
    }
}
