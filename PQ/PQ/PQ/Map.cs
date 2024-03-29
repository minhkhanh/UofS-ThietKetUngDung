﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class Map : GameEntity
    {
        public override GameObject Clone()
        {
            GlobalMap cloneMap = new GlobalMap();
            for (int i = 0; i < _sprites.Count; ++i)
                cloneMap._sprites.Add(new Sprite2D(_sprites[i]));

            cloneMap.LogicalX = LogicalX;
            cloneMap.LogicalY = LogicalY;
            cloneMap.GameObjectParent = GameObjectParent;
            cloneMap.UpdateChild();

            return cloneMap;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Sprite2D i in _sprites)
                if (i.Bounds.Intersects(spriteBatch.GraphicsDevice.PresentationParameters.Bounds))
                    i.Draw(gameTime, spriteBatch);
        }
    }
}
