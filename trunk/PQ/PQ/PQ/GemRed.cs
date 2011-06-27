using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class GemRed : GemColorState
    {
        public override Color Color
        {
            get { return Color.Red; }
        }

        public override GemName Name
        {
            get { return GemName.GemRed; }
        }

        public override void Consumes(Character heroInTurn, Character heroNext)
        {
            ++heroInTurn.MiniStats.ManaR;            
        }

        public override void PlaySound()
        {
            SoundManager.Play("FireMana");
        }

        public override void OnEnterState(Gem gem)
        {
            gem.Sprites.Clear();
            gem.Sprites = (gem.Parent.Game.GemManager.CreateObject((int)GemName.GemRed) as Gem).Sprites;
        }

        public override GemColorState Clone()
        {
            return new GemRed();
        }
    }
}
