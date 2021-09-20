using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;
using System;

namespace Asteroids
{
    class Player
    {
        // TODO: add gun overheating
        public Starship Ship;

        public int Lives;
        //public int Shields;
        public int Score;

        private readonly Timer respawn_timer;
    
        public Player(int lives, int shields)
        {
            Globals.UpdatePlayerScore = UpdateScore;

            Lives = lives;
            //Shields = shields;
            Score = 0;
            respawn_timer = new Timer(1.3f);

            Ship = new Starship();
        }

        public void Update(GameTime game_time)
        {
            Ship.Update(game_time);

            if (Lives == 0) Ship.IsAlive = false;
            else if (!Ship.IsAlive && Lives > 0)
            {
                respawn_timer.Update(game_time);
                if (respawn_timer.Finished)
                {
                    respawn_timer.Reset();
                    Ship.Health = 100f;
                    Ship.Speed = 0f;
                    Ship.Position = Vector3.Zero;
                    Ship.IsAlive = true;
                    Lives--;
                }
            }
        }

        public void Draw()
        {
            if (Lives > 0)
            Ship.Draw();
        }

        private void UpdateScore(object enemy)
        {
            switch(((Entity2D)enemy).EntityType)
            {
                case ENTITY_TYPE.ASTEROID:
                    if (((Entity2D)enemy).Health > 0)
                    {
                        Score += 10; 
                        break;
                    }
                    float r = ((Entity2D)enemy).Radius;
                    if (r >= 40f) Score += 200;
                    else if (r >= 20f) Score += 400;
                    else Score += 600;
                    break;
                case ENTITY_TYPE.UFO:
                    Score += 1000;
                    break;
                default: 
                    break;
            }
        }
    }
}
