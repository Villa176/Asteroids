using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;
using System;

namespace Asteroids
{
    class GameWorld
    {
        readonly Player player;
        readonly List<Projectile2D> projectiles;
        readonly List<Entity2D> enemies;

        // ui
        List<Primitive2D> lives;
        SpawnManager spawner;

        public GameWorld()
        {
            Globals.AddProjectiles = CreateProjectile;
            Globals.AddEntities = CreateEntity;

            player = new Player(3, 2);

            projectiles = new List<Projectile2D>();

            enemies = new List<Entity2D>();

            spawner = new SpawnManager();

            lives = new List<Primitive2D>();

            CreatePlayerLives(player.Lives, Globals.SPACE_RED);
        }

        public void Update(GameTime game_time)
        {
            spawner.Update(game_time, player.Score);

            for (int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i].Despawn)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
                else
                {
                    projectiles[i].Update(game_time, enemies);
                }
            }

            player.Update(game_time);

            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].IsAlive)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
                else
                {
                    enemies[i].Update(game_time, (Entity2D)player.Ship);
                }
            }

            UpdatePlayerLives();
        }

        public void Draw()
        {
            foreach (Projectile2D projectile in projectiles)
            {
                projectile.Draw();
            }

            player.Draw();

            foreach (Entity2D entity in enemies)
            {
                entity.Draw();
            }

            // draw player score
            Globals.Batch.Begin();
            Globals.Batch.DrawString(Globals.Font, player.Score.ToString(), new Vector2(Globals.SCREEN_WIDTH - player.Score.ToString().Length * 10 - 12, 10), Globals.SPACE_RED);
            Globals.Batch.End();

            DrawPlayerLives();
        }

        private void CreateProjectile(object projectile)
        {
            projectiles.Add((Projectile2D)projectile);
        }

        private void CreateEntity(object entity)
        {
            enemies.Add((Entity2D)entity);
        }

        private void CreatePlayerLives(int n, Color color)
        {
            for (int i = 0; i < n; i++)
            { 
                lives.Add(new Primitive2D());

                Vector3[] vertices = new Vector3[3];
                vertices[0] = new Vector3(0, 7, 0);
                vertices[1] = new Vector3(-4.5f, -7f, 0);
                vertices[2] = new Vector3(4.5f, -7f, 0);

                lives[i].Initialize(vertices, color);
            }

        }

        private void UpdatePlayerLives()
        {
            if (player.Lives != lives.Count && lives.Count > 0)
                lives.RemoveAt(player.Lives);
        }

        private void DrawPlayerLives()
        {
            for (int i = 0; i < lives.Count; i++)
            {
                float x = -Globals.SCREEN_WIDTH / 2 + 20 * (i+1);
                float y = Globals.SCREEN_HEIGHT / 2 - 20;
                lives[i].Draw(new Vector3(x, y, 0), 1f, 0f);
            }
        }
    }
}
