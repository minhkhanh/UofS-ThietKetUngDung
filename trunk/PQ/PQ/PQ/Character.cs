using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using Microsoft.Xna.Framework.Input;

namespace PQ
{
    public class Character : GameEntity
    {
        public override GameObject Clone()
        {
            Character character = new Character();

            for (int i = 0; i < _sprites.Count; ++i)
                character._sprites.Add(new Sprite2D(_sprites[i]));
            
            character.X = this.X;
            character.Y = this.Y;

            return character;
        }
    }
}
