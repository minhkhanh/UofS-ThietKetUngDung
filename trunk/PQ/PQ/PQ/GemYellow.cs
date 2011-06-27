using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class GemYellow: GemColorState
    {
        public override Color Color
        {
            get { return Color.Yellow; }
        }

        public override GemName Name
        {
            get { return GemName.GemYellow; }
        }

        public override void Consumes(Character heroInTurn, Character heroNext)
        {
            ++heroInTurn.MiniStats.ManaY;
        }

        public override void PlaySound()
        {
            SoundManager.Play("AirMana");
        }

        public override void OnEnterState(Gem gem)
        {
            gem.Sprites.Clear();
            gem.Sprites = (gem.Parent.Game.GemManager.CreateObject((int)GemName.GemYellow) as Gem).Sprites;
        }

        public override GemColorState Clone()
        {
            return new GemYellow();
        }
    }
}
