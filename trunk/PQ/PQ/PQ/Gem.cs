using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class Gem: GameEntity
    {
        public override GameObject Clone()
        {
            Gem gem = new Gem();
            foreach (Sprite2D i in _sprites)
            {
                gem._sprites.Add(new Sprite2D(i));
            }

            gem.X = this.X;
            gem.Y = this.Y;

            return gem;
        }
    }
}
