using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class PlaneMotionModule
    {
        protected float _vx;
        public float Vx
        {
            get { return _vx; }
            set { _vx = value; }
        }

        protected float _vy;
        public float Vy
        {
            get { return _vy; }
            set { _vy = value; }
        }

        public PlaneMotionModule()
        {
            _vx = _vy = 0;
        }

        public PlaneMotionModule(float vx, float vy)
        {
            _vx = vx;
            _vy = vy;
        }

        public virtual void Stop()
        {

        }

        public virtual void OnMotion(GameObject gameObj, GameTime gameTime)
        {
            
        }
    }
}
