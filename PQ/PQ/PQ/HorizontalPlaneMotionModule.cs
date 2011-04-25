using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        public override void OnMotion(GameObject gameObj, GameTime gameTime)
        {
        }
    }
}
