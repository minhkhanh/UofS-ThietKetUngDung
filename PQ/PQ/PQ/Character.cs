using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Xml;

namespace PQ
{
    public class Character : GameEntity
    {
        public override GameObject Clone()
        {
            Character entity = new Character();

            for (int i = 0; i < _mainSprites.Count; ++i)
                entity._mainSprites.Add(new Sprite2D(_mainSprites[i], -1, -1));
            
            entity._x = this._x;
            entity._y = this._y;

            return entity;
        }
    }
}
