using Microsoft.Xna.Framework;
using System;

namespace Asteroids
{
    class Timer
    {
        public float EndTime;
        public bool Finished;
        public TimeSpan TimeInterval;

        public Timer(float seconds)
        {
            EndTime = seconds;
            Finished = false;
            TimeInterval = new TimeSpan();
        }

        public void Update(GameTime game_time)
        {
            TimeInterval += game_time.ElapsedGameTime;
            if (EndTime <= TimeInterval.TotalSeconds) Finished = true;
        }

        public void Reset()
        {
            Finished = false;
            TimeInterval = TimeSpan.Zero;
        }

        public void Reset(float seconds)
        {
            EndTime = seconds;
            Reset();
        }
    }
}
