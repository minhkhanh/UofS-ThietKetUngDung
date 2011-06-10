using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace IObject
{
    public class Vector2D
    {
        float fX;
        public float X
        {
            get { return fX; }
            set { fX = value; }
        }

        float fY;
        public float Y
        {
            get { return fY; }
            set { fY = value; }
        }
        public Point ToPoint()
        {
            return new Point((int)fX, (int)fY);
        }
        public Vector2D()
        {
            fX = 0;
            fY = 0;
        }
        public Vector2D(float _x, float _y)
        {
            fX = _x;
            fY = _y;
        }
        public void Transform(Matrix2D matrix)
        {
            float _x = fX;
            float _y = fY;
            fX = matrix.matrix[0, 0] * _x + matrix.matrix[1, 0] * _y + matrix.matrix[2, 0];
            fY = matrix.matrix[0, 1] * _x + matrix.matrix[1, 1] * _y + matrix.matrix[2, 1];
        }
    }
}
