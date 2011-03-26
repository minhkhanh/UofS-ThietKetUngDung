using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class PhysicModule
    {
        Sprite2D _owner;

        public const int G = 100;                        // gia tốc rơi tự do (pixel / s^2)

        Vector2 _acceleration = new Vector2(0, G);
        Vector2 _velocity = new Vector2(0, 0);

        public PhysicModule(Sprite2D owner)
        {
            _owner = owner;
        }

        public void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float deltaS = _velocity.Y * t + 0.5f * _acceleration.Y * t * t;
            _owner.Y += (int)deltaS;
            _velocity.Y += (int)(_acceleration.Y * t);

            deltaS = _velocity.X * t + 0.5f * _acceleration.X * t * t;
            _owner.X += (int)deltaS;
            _velocity.X += (int)(_acceleration.Y * t);
        }
    }
}
