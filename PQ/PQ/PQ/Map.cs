using System;
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
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Sprite2D i in _mainSprites)
                if (i.Bound.Intersects(spriteBatch.GraphicsDevice.PresentationParameters.Bounds))
                    i.Draw(gameTime, spriteBatch);
        }
    }
}
