using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class CharacterStats
    {
        int _earth;

        public int Earth
        {
            get { return _earth; }
            set { _earth = value; }
        }
        int _fire;

        public int Fire
        {
            get { return _fire; }
            set { _fire = value; }
        }
        int _air;

        public int Air
        {
            get { return _air; }
            set { _air = value; }
        }
        int _water;

        public int Water
        {
            get { return _water; }
            set { _water = value; }
        }

        int _battle;

        public int Battle
        {
            get { return _battle; }
            set { _battle = value; }
        }
        int _cunning;

        public int Cunning
        {
            get { return _cunning; }
            set { _cunning = value; }
        }
        int _morale;

        public int Morale
        {
            get { return _morale; }
            set { _morale = value; }
        }

        int _gold = 100;

        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }
        int _exp = 0;

        public int Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }

        int _level = 1;

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public CharacterStats(CharacterStats stats)
        {
            _earth = stats._earth;
            _fire = stats._fire;
            _air = stats._air;
            _water = stats._water;
            _battle = stats._battle;
            _cunning = stats._cunning;
            _morale = stats._morale;

            _gold = stats._gold;
            _exp = stats._exp;
            _level = stats._level;
        }

        public CharacterStats(
            int earth, int fire, int air, int water,
            int battle, int cunning, int morale
            )
        {
            _earth = earth;
            _fire = fire;
            _air = air;
            _water = water;

            _battle = battle;
            _cunning = cunning;
            _morale = morale;
        }
    }
}
