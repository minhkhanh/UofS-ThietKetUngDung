using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class VerticalPlaneMotionModule: PlaneMotionModule
    {
        const float G = 100f;

        float _ax;
        public float Ax
        {
            get { return _ax; }
            set { _ax = value; }
        }

        float _ay = G;
        public float Ay
        {
            get { return _ay; }
            set { _ay = value; }
        }

        public override void OnMotion(GameObject gameObj, GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            double dx = _vx * dt + 0.5 * _ax * dt * dt;
            double dy = _vy * dt + 0.5 * _ay * dt * dt;

            gameObj.X += (float)dx;
            _vx += (float)(_ax * dt);

            gameObj.Y += (float)dy;
            _vy += (float)(_ay * dt);
        }
    }
}
