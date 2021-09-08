namespace Asteroids
{
    class Game
    {
        private readonly Ship player;
        private readonly Asteroid asteroid;

        public Game()
        {
            player = new Ship(0, 0);
            asteroid = new Asteroid(0, 0, 8, 1, Globals.SPACE_WHITE);
        }

        public virtual void Update()
        {
            player.Update();
            asteroid.Update();
        }

        public virtual void Draw()
        {
            player.Draw();
            asteroid.Draw();
        }
    }
}
