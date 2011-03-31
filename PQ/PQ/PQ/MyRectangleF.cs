using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class MyRectangleF
    {
        float _x;

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        float _y;

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        float _w;

        public float Width
        {
            get { return _w; }
            set { _w = value; }
        }
        float _h;

        public float Height
        {
            get { return _h; }
            set { _h = value; }
        }

        public float Bottom
        {
            get { return _y + _h; }
        }

        public float Right
        {
            get { return _x + _w; }
        }

        public MyRectangleF(float x, float y, float w, float h)
        {
            _x = x;
            _y = y;
            _w = w;
            _h = h;
        }

        public static MyRectangleF Union(MyRectangleF rect1, MyRectangleF rect2)
        {
            float x = rect1._x < rect2._x ? rect1._x : rect2._x;
            float y = rect1._y < rect2._y ? rect1._y : rect2._y;
            float r = rect1.Right > rect2.Right ? rect1.Right : rect2.Right;
            float b = rect1.Bottom > rect2.Bottom ? rect1.Bottom : rect2.Bottom;

            return new MyRectangleF(x, y, r - x, b - y);
        }
    }
}
