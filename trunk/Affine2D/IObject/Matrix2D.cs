using System;
using System.Collections.Generic;
using System.Text;

namespace IObject
{
    public class Matrix2D
    {
        public float[,] matrix = new float[3,3];
        private Matrix2D()
        {
            matrix[0, 0] = 1;
            matrix[0, 1] = 0;
            matrix[0, 2] = 0;
            matrix[1, 1] = 1;
            matrix[1, 0] = 0;
            matrix[1, 2] = 0;
            matrix[2, 2] = 1;
            matrix[2, 0] = 0;
            matrix[2, 1] = 0;
        }

        public Matrix2D MultMatrix(Matrix2D para)
        {
            //Matrix2D m = new Matrix2D();
            matrix[0, 0] = RowMultColumn(this, para, 0, 0);
            matrix[0, 1] = RowMultColumn(this, para, 0, 1);
            matrix[0, 2] = RowMultColumn(this, para, 0, 2);
            matrix[1, 0] = RowMultColumn(this, para, 1, 0);
            matrix[1, 1] = RowMultColumn(this, para, 1, 1);
            matrix[1, 2] = RowMultColumn(this, para, 1, 2);
            matrix[2, 0] = RowMultColumn(this, para, 2, 0);
            matrix[2, 1] = RowMultColumn(this, para, 2, 1);
            matrix[2, 2] = RowMultColumn(this, para, 2, 2);

            return this;
        }

        private static float RowMultColumn(Matrix2D a, Matrix2D b, int row, int colum)
        {
            return a.matrix[row,0]*b.matrix[0, colum] + a.matrix[row,1]*b.matrix[1, colum] + a.matrix[row,2]*b.matrix[2, colum];
        }
        public static Matrix2D CreateIndentity()
        {
            return new Matrix2D();
        }
        public static Matrix2D CreateTranslate(Vector2D vecTranslate)
        {
            Matrix2D m = new Matrix2D();
            m.matrix[2, 0] = vecTranslate.X;
            m.matrix[2, 1] = vecTranslate.Y;
            return m;
        }
        private static Matrix2D CreateRotationO(float fAlpha)
        {
            Matrix2D m = new Matrix2D();
            m.matrix[0, 0] = (float)Math.Cos(fAlpha);
            m.matrix[0, 1] = -(float)Math.Sin(fAlpha);
            m.matrix[1, 0] = (float)Math.Sin(fAlpha);
            m.matrix[1, 1] = (float)Math.Cos(fAlpha);
            return m;
        }
        public static Matrix2D CreateRotation(Vector2D vecCenter, float fAlpha)
        {
            Matrix2D m = new Matrix2D();
            return m;
        }
    }
}
