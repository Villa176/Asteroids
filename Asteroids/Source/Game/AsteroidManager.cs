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


    }
}
