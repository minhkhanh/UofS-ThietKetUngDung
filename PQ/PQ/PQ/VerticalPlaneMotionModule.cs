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

        float _ay;
        public float Ay
        {
            get { return _ay; }
            set { _ay = value; }
        }

        public VerticalPlaneMotionModule()
            : base(0,0)
        {
            _ax = 0;
            _ay = 0;
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
            if (IsPlaying)
            {
                gameObj.X += _vx;
                gameObj.Y += _vy;

                _vx += _ax;
                _vy += _ay;
            }
        }

        public override bool IsMoving
        {
            get
            {
                return (_vx != 0 || _vy != 0 || _ax != 0 || _ay != 0);
            }
        }

        public override Direction MovingDirection
        {
            get
            {
                if (_vx == 0 && _ax == 0)
                {
                    if (_vy > 0)
                        return Direction.Downward;
                    else if (_vy < 0)
                        return Direction.Upward;
                }

                if (_vy == 0 && _ay == 0)
                {
                    if (_vx > 0)
                        return Direction.Rightward;
                    else if (_vx < 0)
                        return Direction.Leftward;
                }

                return Direction.None;
            }
        }
    }
}
