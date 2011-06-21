using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public enum SplittingDirection
    {
        Vertically,
        Horizontally
    }

    public class SplittingDetails
    {
        SplittingDirection _splittingDirection = SplittingDirection.Vertically;    // vertical or horizon

        public SplittingDirection SplittingDirection
        {
            get { return _splittingDirection; }
            set { _splittingDirection = value; }
        }

        int _rowCount = 0;
        public int RowCount
        {
            get { return _rowCount; }
            set { _rowCount = value; }
        }

        int _colCount = 0;
        public int ColumnCount
        {
            get { return _colCount; }
            set { _colCount = value; }
        }

        int _rowIdx = -1;
        public int RowIndex
        {
            get { return _rowIdx; }
            set { _rowIdx = value; }
        }

        int _colIdx = -1;
        public int ColumnIndex
        {
            get { return _colIdx; }
            set { _colIdx = value; }
        }

        int _frameWidth = 0;
        public int FrameWidth
        {
            get { return _frameWidth; }
            set { _frameWidth = value; }
        }

        int _frameHeight = 0;
        public int FrameHeight
        {
            get { return _frameHeight; }
            set { _frameHeight = value; }
        }

        int _xSpace = 0;
        public int SpaceX
        {
            get { return _xSpace; }
            set { _xSpace = value; }
        }

        int _ySpace = 0;
        public int SpaceY
        {
            get { return _ySpace; }
            set { _ySpace = value; }
        }

        int _xMargin = 0;
        public int InitMarginX
        {
            get { return _xMargin; }
            set { _xMargin = value; }
        }

        int _yMargin = 0;
        public int InitMarginY
        {
            get { return _yMargin; }
            set { _yMargin = value; }
        }

        public SplittingDetails(int rowCount, int colCount,
            int rowIdx, int colIdx,
            int frmWidth, int frmHeight,
            int xSpace, int ySpace,
            int xMargin, int yMargin)
        {
            _rowIdx = rowIdx;
            _colIdx = colIdx;

            _rowCount = rowCount;
            _colCount = colCount;

            _frameWidth = frmWidth;
            _frameHeight = frmHeight;

            _xSpace = xSpace;
            _ySpace = ySpace;

            _xMargin = xMargin;
            _yMargin = yMargin;
        }
    }
}
