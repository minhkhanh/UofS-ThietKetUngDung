using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class GemWhite: GemColorState
    {
        public override Color Color
        {
            get { return Color.White; }
        }

        public override GemName Name
        {
            get { return GemName.GemWhite; }
        }

        public override void Consumes(Character heroInTurn, Character heroNext)
        {
            --heroNext.MiniStats.Hp;
        }

        public override void PlaySound()
        {
            SoundManager.Play("Damage");
        }

        public override void OnEnterState(Gem gem)
        {
            gem.Sprites.Clear();
            gem.Sprites = (gem.Parent.Game.GemManager.CreateObject((int)GemName.GemWhite) as Gem).Sprites;
        }

        public override GemColorState Clone()
        {
            return new GemWhite();
        }
    }
}
