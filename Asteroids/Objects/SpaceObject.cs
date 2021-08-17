using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Objects
{
    class SpaceObject
    {
        public Color Color { get; set; }
        public float Scale { get; set; } = 0.5f;
        public float Angle { get; set; } = 0;
        public float Speed { get; set; } = 0;
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }

        protected VertexBuffer vertexBuffer;
        protected IndexBuffer indexBuffer;
        protected int VertexCount;

        protected GraphicsDeviceManager gDeviceManager;
        protected BasicEffect bEffect;

        public SpaceObject(float x, float y, float angle, Color color)
        {
            Position = new Vector3(x, y, 0);
            Angle = angle;
            Color = color;
        }

        public void Draw()
        {
            gDeviceManager.GraphicsDevice.SetVertexBuffer(vertexBuffer);
            gDeviceManager.GraphicsDevice.Indices = indexBuffer;

            bEffect.World = Matrix.CreateFromYawPitchRoll(0, 0, Angle) * Matrix.CreateTranslation(Position) * Matrix.CreateScale(Scale);

            foreach (EffectPass pass in bEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }

            gDeviceManager.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, VertexCount);
        }
    }
}
