using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;
using System;

namespace Asteroids
{
    class Projectile2D
    {
        protected Vector3 position;
        protected Vector3 direction;
        protected float speed;
        protected float angle;
        protected float radius;
        protected float scale = 1f;
        protected float damage;
        protected bool despawn;
        protected Entity2D parent;

        #region GET/SET
        public Entity2D Parent
        {
            get { return parent; }
        }

        public float Damage
        {
            get { return damage; }
        }

        public bool Despawn
        {
            get { return despawn; }
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
        }

        public Vector3 Direction
        {
            get { return direction; }
        }

        public float Speed
        {
            get { return speed; }
        }

        public float Angle
        {
            get { return angle; }
        }
        #endregion

        protected Timer timer;
        private Primitive2D shape;

        public Projectile2D(Entity2D parent_entity, Vector3 initial_position, float collision_radius, float direction_radians, float movement_speed)
        {
            parent = parent_entity;
            despawn = false;
            position = initial_position;
            speed = movement_speed;
            angle = direction_radians;
            radius = collision_radius;
            direction = new Vector3(-MathF.Sin(angle), MathF.Cos(angle), 0);
        }

        public void InitializeProperties(float seconds_alive, float impact_damage)
        {
            timer = new Timer(seconds_alive);
            damage = impact_damage;
        }

        public void InitializeShape(Vector3[] primitive_vertices, Color color, bool filled_shape = true)
        {
            shape = new Primitive2D(filled_shape);
            shape.Initialize(primitive_vertices, color);
        }

        public virtual void Update(GameTime game_time, List<Entity2D> entities)
        {
            if (despawn) return;
            if (!shape.Initialized) throw new ArgumentNullException("Primitive shape not initialized");

            if (timer.Finished) despawn = true;
            timer.Update(game_time);
            position += direction * speed;
            if (entities != null)
            {
                foreach (Entity2D entity in entities)
                {
                    if (Helpers.CirclesIntersect(position, radius, entity.Position, entity.Radius))
                    {
                        entity.Health -= damage;
                        despawn = true;
                        Globals.UpdatePlayerScore(entity);
                        break;
                    }
                }
            }
        }

        public virtual void Draw()
        {
            if (!shape.Initialized) throw new ArgumentNullException("Primitive shape not initialized");
            if (!despawn) shape.Draw(position, scale, angle);
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
