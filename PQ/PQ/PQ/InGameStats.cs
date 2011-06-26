using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class MiniGameStats
    {
        int _manaYellow;

        public int ManaY
        {
            get { return _manaYellow; }
            set { _manaYellow = value; }
        }
        int _manaGreen;

        public int ManaG
        {
            get { return _manaGreen; }
            set { _manaGreen = value; }
        }
        int _manaRed;

        public int ManaR
        {
            get { return _manaRed; }
            set { _manaRed = value; }
        }
        int _manaBlue;

        public int ManaB
        {
            get { return _manaBlue; }
            set { _manaBlue = value; }
        }

        int _hp;

        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }

        public MiniGameStats(int y, int g, int r, int b, int hp)
        {
            _manaYellow = y;
            _manaGreen = g;
            _manaRed = r;
            _manaBlue = b;
            _hp = hp;
        }
    }
}
