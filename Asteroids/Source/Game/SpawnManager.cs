using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids
{
    class SpawnManager
    {
        private float ufo_probability = 0.1f;

        float asteroid_radius = 40f;
        float asteroid_health = 100f;
        float asteroid_speed = 5f;
        float asteroid_time_alive = 30f;

        int difficulty_marker = 1000;
        
        readonly Timer enemy_spawn_rate;

        public SpawnManager()
        {
            enemy_spawn_rate = new Timer(10f);
            AddAsteroid();
        }

        public void Update(GameTime game_time, int current_score)
        {
            enemy_spawn_rate.Update(game_time);
            
            if (enemy_spawn_rate.Finished)
            {
                if (current_score >= difficulty_marker)
                {
                    // TODO: add difficulty cap
                    IncreaseDifficulty();
                    difficulty_marker *= 5;
                }
               
                AddAsteroid();
                
                enemy_spawn_rate.Reset();
            
            }
        }

        private void IncreaseDifficulty()
        {
            ufo_probability += 0.5f;

            asteroid_radius += 5f;

            asteroid_health += 5f;

            asteroid_speed += 0.25f;

            asteroid_time_alive += 1f;

            enemy_spawn_rate.EndTime -= 0.5f;
        }

        private void AddAsteroid()
        {
            Random random = new Random();

            float posneg = random.Next(2) == 0 ? -1f : 1f;
            
            float directional_angle = posneg * (float)(random.NextDouble() * 2 * Math.PI);
            
            Globals.AddEntities(new Asteroid(RandPosOffScreen(), asteroid_radius, directional_angle, asteroid_speed, asteroid_health, asteroid_time_alive));
        }

        private Vector3 RandPosOffScreen()
        {
            float WIDTH = Globals.SCREEN_WIDTH / 2;
            float HEIGHT = Globals.SCREEN_HEIGHT / 2;

            Random random = new Random();
            
            float x = (float)(random.NextDouble() * Globals.SCREEN_WIDTH + WIDTH);
            float y = (float)(random.NextDouble() * Globals.SCREEN_HEIGHT + HEIGHT);

            x *= random.Next(2) == 0 ? -1f : 1f;
            y *= random.Next(2) == 0 ? -1f : 1f;

            return new Vector3(x, y, 0);
        }
    }
}
