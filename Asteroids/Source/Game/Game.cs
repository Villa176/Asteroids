using System.Collections.Generic;

namespace Asteroids
{
    class Game
    {
        private readonly Ship player;
        private readonly AsteroidManager am;

        public Game()
        {
            player = new Ship(0, 0);
            am = new AsteroidManager();
        }

        public virtual void Update()
        {
            player.Update();
            am.Update();
        }

        public virtual void Draw()
        {
            player.Draw();
            am.Draw();
        }
    }
}
