using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Asteroids
{
    class Primitive2D
    {
        private Matrix                      world_matrix;
        private VertexPositionColor[]       vertices;
        private VertexBuffer                vertex_buffer;
        private short[]                     indices;
        private IndexBuffer                 index_buffer;
        private bool                        filled;
        public bool                         Initialized = false;

        public Primitive2D(bool is_filled = true)
        {
            filled = is_filled;
        }

        public void Initialize(Vector3[] m_vertices, Color color)
        {
            if (filled) InitializeFilled(m_vertices, color);
            else InitializeWireFrame(m_vertices, color);
        }

        public void Draw(Vector3 pos, float scale, float rotation_rad)
        {
            if (!Initialized) return;

            Globals.Graphics.GraphicsDevice.SetVertexBuffer(vertex_buffer);
            Globals.Graphics.GraphicsDevice.Indices = index_buffer;

            world_matrix = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(0, 0, rotation_rad) * Matrix.CreateTranslation(pos);

            Globals.Basic.World = world_matrix;

            foreach (EffectPass pass in Globals.Basic.CurrentTechnique.Passes) pass.Apply();

            Globals.Graphics.GraphicsDevice.DrawIndexedPrimitives((filled) ? PrimitiveType.TriangleList : PrimitiveType.LineList, 0, 0, vertices.Length);
        }

        private void SetVertexBuffer()
        {
            vertex_buffer = new VertexBuffer(
                Globals.Graphics.GraphicsDevice,
                typeof(VertexPositionColor),
                vertices.Length,
                BufferUsage.WriteOnly
            );
            vertex_buffer.SetData(vertices);
        }

        private void SetIndexBuffer()
        {
            index_buffer = new IndexBuffer(
                Globals.Graphics.GraphicsDevice,
                typeof(short),
                indices.Length,
                BufferUsage.WriteOnly
            );
            index_buffer.SetData(indices);
        }

        private void InitializeWireFrame(Vector3[] m_vertices, Color color)
        {
            vertices = new VertexPositionColor[m_vertices.Length];
            for (int i = 0; i < m_vertices.Length; i++)
            {
                vertices[i] = new VertexPositionColor(m_vertices[i], color);
            }
            SetVertexBuffer();

            indices = new short[m_vertices.Length * 2];
            for (int i = 0; i < m_vertices.Length; i++)
            {
                indices[i * 2] = (short)i;
                indices[i * 2 + 1] = (i == m_vertices.Length - 1) ? indices[0] : (short)(i + 1);
            }
            SetIndexBuffer();

            Initialized = true;
        }

        private void InitializeFilled(Vector3[] m_vertices, Color color)
        {
            vertices = new VertexPositionColor[m_vertices.Length + 1];
            vertices[0] = new VertexPositionColor(Vector3.Zero, color);
            for (int i = 0; i < m_vertices.Length; i++)
            {
                vertices[i + 1] = new VertexPositionColor(m_vertices[i], color);
            }
            SetVertexBuffer();

            indices = new short[m_vertices.Length * 3];
            for (int i = 0; i < m_vertices.Length; i++)
            {
                indices[i * 3] = (short)(i + 1);
                indices[i * 3 + 1] = 0;
                indices[i * 3 + 2] = (i == m_vertices.Length - 1) ? indices[0] : (short)(i + 2);
            }
            SetIndexBuffer();

            Initialized = true;
        }
    }
}
