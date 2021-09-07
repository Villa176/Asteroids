using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids
{
    class Bullet : Primitive2D
    {
        // communicate collosion events with parent
        private readonly Ship parent; 

        private bool despawn;
        private readonly Timer timer;

        private float speed;
        private Vector3 direction;

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



        public bool Despawn
        {
            get { return despawn; }
            set { despawn = value; }
        }

        public Bullet(float x, float y, Color color, Ship _parent) : base(x, y)
        {
            CreateBullet(color);
            timer = new Timer(3000);
            despawn = false;
            parent = _parent;

            speed = 15f;
            Angle = _parent.Angle;
            direction = new Vector3(-MathF.Sin(Angle), MathF.Cos(Angle), 0);
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

        public override void Update()
        {
            if (!despawn)
            {
                base.Update();
                timer.Update();

                Position += Direction * Speed * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

                if (timer.TimeReached)
                {
                    despawn = true;
                }
            }
        }

        public override void Draw()
        {
            if (!despawn) base.Draw();
        }
    }
}
