using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids
{
    class Timer
    {
        public bool TimeReached;
        protected TimeSpan timer = new TimeSpan();
        protected int milliseconds;

        public int MSec
        {
            get { return milliseconds; }
            set { milliseconds = value; }
        }

        public int GetTimer
        {
            get { return (int)timer.TotalMilliseconds; }
        }


        public Timer(int ms)
        {
            milliseconds = ms;
            TimeReached = false;
        }

        public void Update()
        {
            timer += Globals.gameTime.ElapsedGameTime;
            if (milliseconds <= (int)timer.TotalMilliseconds)
            {
                TimeReached = true;
            }
        }

        public void Reset()
        {
            TimeReached = false;
            timer = TimeSpan.Zero;
        }

        public void Reset(int ms)
        {
            milliseconds = ms;
            Reset();
        }

    }
}
