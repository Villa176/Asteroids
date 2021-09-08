using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    class Bullet : Projectile2D
    {
        // communicate collosion events with parent
        private readonly Ship parent;

        public Bullet(float x, float y, Color color, Ship _parent) : base(x, y, _parent.Angle, 5000)
        {
            CreateBullet(color);
            parent = _parent;
            speed = 15f;
        }

        private void CreateBullet(Color color)
        {
            vertexCount = 4;

            VertexPositionColor[] vertices = new VertexPositionColor[4];
            vertices[0] = new VertexPositionColor(new Vector3(-0.05f, 0.15f, 0), color);
            vertices[1] = new VertexPositionColor(new Vector3(0.05f, 0.15f, 0), color);
            vertices[2] = new VertexPositionColor(new Vector3(0.05f, -0.15f, 0), color);
            vertices[3] = new VertexPositionColor(new Vector3(-0.05f, -0.15f, 0), color);

            vertexBuffer = new VertexBuffer(Globals.graphicsDevice.GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            short[] indices = new short[6];
            indices[0] = 0; indices[1] = 1; indices[2] = 2;
            indices[3] = 0; indices[4] = 2; indices[5] = 3;

            indexBuffer = new IndexBuffer(Globals.graphicsDevice.GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);

            Initialized = true;
        }
    }
}
