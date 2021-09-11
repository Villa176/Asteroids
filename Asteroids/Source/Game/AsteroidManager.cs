using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids
{
    class AsteroidManager
    {
        private Timer timer;
        private List<Asteroid> asteroidsList;

        public List<Asteroid> AsteroidsList
        {
            get { return asteroidsList; }
            private set { asteroidsList = value; }
        }

        public AsteroidManager()
        {
            timer = new Timer(5000);
            asteroidsList = new List<Asteroid>();
            AddAsteroids(1);
        }

        public void Update()
        {
            timer.Update();

            if (timer.TimeReached)
            {
                AddAsteroids(1);
                timer.Reset();
            }

            for (int i = 0; i < asteroidsList.Count; i++)
            { 
                asteroidsList[i].Update();

                if (asteroidsList[i].Health != 0 && asteroidsList[i].Despawn && !asteroidsList[i].IsOnScreen)
                {
                    asteroidsList.RemoveAt(i);
                    i--;
                }
                else if (asteroidsList[i].Health <= 0)
                {
                    // TODO: create new asteroids
                    asteroidsList.RemoveAt(i);
                    i--;
                }

            }
        }

        public void Draw()
        {
            foreach (var a in asteroidsList) a.Draw();
        }

        private void AddAsteroids(int n)
        {
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                float pny = rand.Next(10) < 5 ? -1 : 1;
                float pnx = rand.Next(10) < 5 ? -1 : 1;
                float y = pny * ((float)rand.NextDouble() * 23f + 23f);
                float x = pnx * ((float)rand.NextDouble() * 23f + 23f);
                asteroidsList.Add(new Asteroid(x, y, 9, 3f, Globals.SPACE_WHITE));
            }
        }
    }
}
