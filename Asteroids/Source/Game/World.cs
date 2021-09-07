using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids
{
    class World
    {
        private readonly Ship player;

        public World()
        {
            player = new Ship(0, 0);
        }

        public virtual void Update()
        {
            player.Update();
        }

        public virtual void Draw()
        {
            player.Draw();
        }
    }
}
