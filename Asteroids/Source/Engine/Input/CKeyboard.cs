using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroids
{
    class CKeyboard
    {
        public KeyboardState state;
        public KeyboardState prevState;

        public void Update()
        {
            state = Keyboard.GetState();
        }

        public void UpdatePrev()
        {
            prevState = state;
        }

        public bool IsKeyHeld(Keys key)
        {
            if (state.IsKeyDown(key) && prevState.IsKeyDown(key))
                return true;

            return false;
        }

        public bool IsKeyPressed(Keys key)
        {
            if (state.IsKeyUp(key) && prevState.IsKeyDown(key))
                return true;

            return false;
        }
    }
}
