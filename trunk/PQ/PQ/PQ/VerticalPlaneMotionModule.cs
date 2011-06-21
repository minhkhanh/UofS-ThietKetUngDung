using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class VerticalPlaneMotionModule: PlaneMotionModule
    {
        public const float G = 0.1f;      // acceleration of gravity: pixel / game tick ^ 2

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

        public VerticalPlaneMotionModule(float ax, float ay, float vx, float vy)
            : base(vx, vy)
        {
            _ax = ax;
            _ay = ay;
        }

        public override void Stop()
        {
            _ax = 0;
            _ay = 0;
            _vx = 0;
            _vy = 0;
        }

        public override void OnMotion(GameObject gameObj, GameTime gameTime)
        {
            //double dt = gameTime.ElapsedGameTime.TotalSeconds;
            //double dx = _vx * dt + 0.5 * _ax * dt * dt;
            //double dy = _vy * dt + 0.5 * _ay * dt * dt;

            //gameObj.X += (float)dx;
            //_vx += (float)(_ax * dt);

            //gameObj.Y += (float)dy;
            //_vy += (float)(_ay * dt);

            gameObj.X += _vx;
            gameObj.Y += _vy;

            _vx += _ax;
            _vy += _ay;
        }
    }
}
