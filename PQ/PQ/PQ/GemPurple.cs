using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class GemPurple: GemColorState
    {
        public override Color Color
        {
            get { return Color.Purple; }
        }

        public override GemName Name
        {
            get { return GemName.GemPurple; }
        }

        public override void Consumes(Character hero)
        {
            ++hero.MainStats.Exp;         
        }

        public override void PlaySound()
        {
            SoundManager.Play("XP");
        }

        public override void OnEnterState(Gem gem)
        {
            gem.Sprites.Clear();
            gem.Sprites = (gem.Parent.Game.GemManager.CreateObject((int)GemName.GemPurple) as Gem).Sprites;
        }

        public override GemColorState Clone()
        {
            return new GemPurple();
        }
    }
}
