using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class HorizontalPlaneMotionModule: PlaneMotionModule
    {
        float _a = 0;

        //public float A
        //{
        //    get { return _a; }
        //    set { _a = value; }
        //}

        public override void OnMotion(GameObject gameObj, Microsoft.Xna.Framework.GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;

            double dx = _vx * dt + 0.5 * _a * dt * dt;
            double dy = _vy * dt + 0.5 * _a * dt * dt;

            gameObj.X += (float)dx;
            _vx += (float)(_a * dt);

            gameObj.Y += (float)dy;
            _vy += (float)(_a * dt);
        }
    }
}
