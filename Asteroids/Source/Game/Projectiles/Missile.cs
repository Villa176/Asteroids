using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids
{
    class Missile : Projectile2D
    {
        public Missile(Entity2D parent_entity, Vector3 initial_position, float collision_radius, float direction_radians, float movement_speed, Color color) : 
            base(parent_entity, initial_position, collision_radius, direction_radians, movement_speed)
        {
            Vector3[] shape_vertices = new Vector3[]
            {
                new Vector3(-1.5f, 2.5f, 0f),
                new Vector3(-1.5f, -2.5f, 0f),
                new Vector3(1.5f, -2.5f, 0f),
                new Vector3(1.5f, 2.5f, 0f)
            };

            InitializeProperties(1f, 15f);
            InitializeShape(shape_vertices, color);
        }

        public override void Update(GameTime game_time, List<Entity2D> entities)
        {
            base.Update(game_time, entities);
            // WrapAround();
        }
    }
}
