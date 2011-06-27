using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class GemGreen: GemColorState
    {
        public override Color Color
        {
            get { return Color.Green; }
        }

        public override GemName Name
        {
            get { return GemName.GemGreen; }
        }

        public override void Consumes(Character hero)
        {
            ++hero.MiniStats.ManaG;
        }

        public override void PlaySound()
        {
            SoundManager.Play("EarthMana");
        }

        public override void OnEnterState(Gem gem)
        {
            gem.Sprites.Clear();
            gem.Sprites = (gem.Parent.Game.GemManager.CreateObject((int)GemName.GemGreen) as Gem).Sprites;
        }

        public override GemColorState Clone()
        {
            return new GemGreen();
        }
    }
}
