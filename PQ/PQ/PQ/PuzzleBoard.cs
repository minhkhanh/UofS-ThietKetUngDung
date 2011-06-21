using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class PuzzleBoard : GameDialog
    {
        int _colNum = 0;
        int _rowNum = 0;

        Random _rand = new Random();

        List<Gem> _gems = new List<Gem>();

        public PuzzleBoard(int rowNum, int colNum)
        {
            _rowNum = rowNum;
            _colNum = colNum;
        }

        public override void CheckCollision()
        {
            for (int c = 0; c < _colNum; ++c)
                for (int r = 0; r < _rowNum; ++r)
                {

                }
        }
    }
}
