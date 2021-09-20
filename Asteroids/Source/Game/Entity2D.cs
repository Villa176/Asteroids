using Microsoft.Xna.Framework;
using System;

namespace Asteroids
{
    public enum ENTITY_TYPE
    {
        PLAYER,
        ASTEROID,
        UFO,
        NONE
    }

    class Entity2D
    {
        protected ENTITY_TYPE type = ENTITY_TYPE.NONE;
        protected Vector3 position;
        protected Vector3 direction;
        protected float speed;
        protected float angle;
        protected float radius;
        protected float scale = 1f;
        protected float health = 100f;

        #region GET/SET

        public ENTITY_TYPE EntityType
        {
            get { return type; }
        }

        public float Radius
        {
            get { return radius; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector3 Direction
        {
            get { return direction; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public float Angle
        {
            get { return angle; }
        }
        public float Health 
        {
            get { return health; }
            set { health = value; }
        }
        #endregion

        public bool IsAlive = true;

        private Primitive2D shape;

        public Entity2D(Vector3 entity_position,float collision_radius, float direction_angle_rad, float initial_speed = 0f)
        {
            position = entity_position;
            angle = direction_angle_rad;
            speed = initial_speed;
            radius = collision_radius;
            direction = new Vector3(-MathF.Sin(angle), MathF.Cos(angle), 0);
        }

        public void InitializeShape(Vector3[] primitive_vertices, Color color, bool filled_shape = true)
        {
            shape = new Primitive2D(filled_shape);
            shape.Initialize(primitive_vertices, color);
        }

        public virtual void Update(GameTime game_time, Entity2D collision_object = null)
        {
            if (!shape.Initialized) throw new ArgumentNullException("Primitive shape not initialized");

            if (Health <= 0f) IsAlive = false;
        }

        public virtual void Draw()
        {
            if (!shape.Initialized) throw new ArgumentNullException("Primitive shape not initialized");

            shape.Draw(position, scale, angle);
        }

        protected void WrapAround()
        {
            float WIDTH = Globals.SCREEN_WIDTH / 2 + radius;
            float HEIGHT = Globals.SCREEN_HEIGHT / 2 + radius;

            if (position.X > WIDTH) position.X = -WIDTH;
            else if (position.X < -WIDTH) position.X = WIDTH;

            if (position.Y > HEIGHT) position.Y = -HEIGHT;
            else if (position.Y < -HEIGHT) position.Y = HEIGHT;
        }
    }
}
