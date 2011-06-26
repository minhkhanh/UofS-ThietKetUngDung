using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class GemColorState
    {
        public virtual GemName Name
        {
            get { return GemName.None; }
        }

        public virtual Color Color
        {
            get { return Color.Black; }
        }

        public virtual void OnEnterState(Gem gem)
        {
            gem.Sprites.Clear();
        }

        public virtual void OnExitState(Gem gem)
        {

        }

        public virtual GemColorState Clone()
        {
            return new GemColorState();
        }

        public virtual void Consumes(MiniGameStats stats)
        {

        }
    }
}
