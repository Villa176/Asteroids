using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Asteroids
{
    class Starship : Entity2D
    {
        private const float MAX_SPEED = 4f;
        private const float ACCELERATION = 0.04f;
        private const float DECELERATION = 0.02f;
        private const float DIRECTION_ANGLE = 0f;
        private const float SHIP_RADIUS = 10f;

        public Starship() : base(Vector3.Zero, SHIP_RADIUS, DIRECTION_ANGLE)
        {
            type = ENTITY_TYPE.PLAYER;

            Vector3[] shape_vertices = new Vector3[]
            {
                new Vector3(0f, 10f, 0f),
                new Vector3(-6f, -10f, 0f),
                new Vector3(6f, -10f, 0f)
            };

            InitializeShape(shape_vertices, Globals.SPACE_WHITE, true);
        }

        public void Update(GameTime game_time)
        {
            if (!IsAlive) return;

            base.Update(game_time);

            if (Globals.KBInput.IsKeyPressed(Keys.Space))
            {
                Globals.AddProjectiles(new Missile(this, position, 2f, angle, 7f, Globals.SPACE_RED));
            }

            if (Globals.KBInput.IsKeyHeld(Keys.W))
            {
                if (speed < MAX_SPEED) speed += ACCELERATION;
            }
            else if (speed > 0)
            {
                speed -= DECELERATION;
            }

            if (Globals.KBInput.IsKeyHeld(Keys.A))
            {
                angle += 0.035f;
            }

            if (Globals.KBInput.IsKeyHeld(Keys.D))
            {
                angle -= 0.035f;
            }

            if (speed > 0)
            {
                direction.X = -MathF.Sin(angle);
                direction.Y = MathF.Cos(angle);
                position += direction * speed;
            }

            WrapAround();
        }

        public override void Draw()
        {
            if (IsAlive) base.Draw();
        }
    }
}
