using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class GemBlue : GemColorState
    {
        public override Color Color
        {
            get { return Color.Cyan; }
        }

        public override GemName Name
        {
            get { return GemName.GemBlue; }
        }

        public override void Consumes(Character hero)
        {
            ++hero.MiniStats.ManaB;
        }

        public override void PlaySound()
        {
            SoundManager.Play("WaterMana");
        }

        public override void OnEnterState(Gem gem)
        {
            gem.Sprites.Clear();
            gem.Sprites = (gem.Parent.Game.GemManager.CreateObject((int)GemName.GemBlue) as Gem).Sprites;
        }

        public override GemColorState Clone()
        {
            return new GemBlue();
        }
    }
}
