using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids
{
    class World
    {
        private Ship player;
        private readonly Bullet bullet;

        // TODO: Add projectile management 
        public World()
        {
            player = new Ship(0, 0);
            bullet = new Bullet(0, 0, Globals.SPACE_WHITE, player);
        }

        public virtual void Update()
        {
            player.Update();
            bullet.Update();
        }

        public virtual void Draw()
        {
            player.Draw();
            bullet.Draw();
        }
    }
}
