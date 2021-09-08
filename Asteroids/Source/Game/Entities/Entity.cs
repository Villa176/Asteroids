using Microsoft.Xna.Framework;

namespace Asteroids
{
    class Entity : Primitive2D
    {
        protected int health = 100;
        protected float speed;
        protected Vector3 direction;

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

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public Entity(float x, float y) : base(x, y)
        {
        }

        public Entity(float x, float y, int numOfVertices, int radius, Color color, bool isUniformShape = true) : base(x, y, numOfVertices, radius, color, isUniformShape)
        {
        }

        public new virtual void Draw()
        {
            base.Draw();
        }
    }
}
