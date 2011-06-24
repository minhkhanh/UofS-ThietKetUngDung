using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class CharacterStats
    {
        int _earth;
        int _fire;
        int _air;
        int _water;

        int _battle;
        int _cunning;
        int _morale;

        int _gold = 100;
        int _exp = 0;

        int _level = 1;

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
