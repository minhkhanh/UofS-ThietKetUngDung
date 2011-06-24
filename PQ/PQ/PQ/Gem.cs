using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class Gem: GameEntity
    {
        GemName _name = GemName.None;
        public GemName Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override GameObject Clone()
        {
            Gem gem = new Gem();
            foreach (Sprite2D i in _sprites)
            {
                gem._sprites.Add(new Sprite2D(i));
            }

            gem.X = this.X;
            gem.Y = this.Y;

            gem.Name = this.Name;

            // clone without motion module
            gem.MotionModule = new VerticalPlaneMotionModule(0, 0, 0, 0);

            return gem;
        }


    }
}
