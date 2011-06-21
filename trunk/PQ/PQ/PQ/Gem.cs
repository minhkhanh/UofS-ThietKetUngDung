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
        bool _isSelected = false;

        public override GameObject Clone()
        {
            Gem gem = new Gem();
            foreach (Sprite2D i in _sprites)
            {
                gem._sprites.Add(new Sprite2D(i));
            }

            gem.X = this.X;
            gem.Y = this.Y;
            gem.MotionModule = new VerticalPlaneMotionModule(0, 0, 0, 0);

            return gem;
        }
    }
}
