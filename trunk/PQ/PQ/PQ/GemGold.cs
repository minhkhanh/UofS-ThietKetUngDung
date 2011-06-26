using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class GemGold: GemColorState
    {
        public override Color Color
        {
            get { return Color.Orange; }
        }

        public override GemName Name
        {
            get { return GemName.GemGold; }
        }

        public override void Consumes(MiniGameStats stats)
        {
            //++stats.ManaR;
            SoundManager.Play("Gold");
        }

        public override void OnEnterState(Gem gem)
        {
            gem.Sprites.Clear();
            gem.Sprites = (gem.Parent.Game.GemManager.CreateObject((int)GemName.GemGold) as Gem).Sprites;
        }

        public override GemColorState Clone()
        {
            return new GemGold();
        }
    }
}
