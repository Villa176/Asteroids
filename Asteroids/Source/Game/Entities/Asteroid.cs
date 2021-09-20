using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Asteroids
{
    class Asteroid : Entity2D
    {
        private readonly float ROTATION_SPEED;
        private readonly float MAX_HEALTH;
        private readonly Timer timer;

        public Asteroid(Vector3 entity_position, float collision_radius, float direction_angle_rad, float initial_speed = 3f, float initial_health = 100f, float time_alive = 30f) : 
            base(entity_position, collision_radius, direction_angle_rad, initial_speed)
        {
            type = ENTITY_TYPE.ASTEROID;

            timer = new Timer(time_alive);
            MAX_HEALTH = initial_health;
            health = initial_health;

            Random random = new Random();
            float posneg = random.Next(2) == 1 ? -1f : 1f;
            ROTATION_SPEED = (float)(posneg * random.NextDouble() * 0.035f);

            float min_rad = collision_radius / 2f;
            float max_rad = collision_radius + min_rad;
            Vector3[] shape_vertices = GenerateAsteroidVertices(min_rad, max_rad, 8);
            InitializeShape(shape_vertices, Globals.SPACE_WHITE, false);
        }

        public override void Update(GameTime game_time, Entity2D player)
        {
            if (!IsAlive) return;

            base.Update(game_time);

            timer.Update(game_time);

            if (Health <= 0)
            {
                IsAlive = false;
                AddChildren(3);
                return;
            }
            else if (timer.Finished)
            {
                if (Helpers.NotOnScreen(position, radius))
                {
                    IsAlive = false;
                }
            }

            position += direction;
            angle += ROTATION_SPEED;

            if (Helpers.CirclesIntersect(player.Position, player.Radius, position, radius))
            {
                player.Health = 0;
            }

            if (!timer.Finished)
                WrapAround();
        }

        public override void Draw()
        {
            if (IsAlive) base.Draw();
        }

        private void AddChildren(int n)
        {
            if (Health <= 0 && radius >= 20f)
            {
                Random random = new Random();
                for (int i = 0; i < n; i++)
                {
                    float new_angle = (float)(random.NextDouble() * 2 * Math.PI);
                    Globals.AddEntities(new Asteroid(new Vector3(position.X, position.Y, 0), radius / 2f, new_angle, speed, MAX_HEALTH / 2f));
                }
            }
        }

        private Vector3[] GenerateAsteroidVertices(float min_radius, float max_radius, int num_of_vertices)
        {
            Vector3[] asteroid_vertices = new Vector3[num_of_vertices];

            float angle_interval = (2f * MathF.PI) / num_of_vertices;
            Random random = new Random();

            for (int i = 0; i < num_of_vertices; i++)
            {
                float c_angle = angle_interval * i;

                float magnitude = ((float)random.NextDouble() * (max_radius - min_radius)) + min_radius;

                asteroid_vertices[i] = new Vector3(-MathF.Sin(c_angle), MathF.Cos(c_angle), 0) * magnitude;
            }

            return asteroid_vertices;
        }
    }
}
