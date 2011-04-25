using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class GameButtonUpState : GameButtonState
    {
        protected override void OnChangeState(GameButton button)
        {
            List<Sprite2D> tmp = button.Sprites;
            button.Sprites = button.UpSprites;
            button.UpSprites = tmp;
        }

        public override void OnDraw(GameButton button, GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Sprite2D i in button.UpSprites)
                i.Draw(gameTime, spriteBatch);
        }

        public override void OnUpdate(GameButton button, GameTime gameTime)
        {
            foreach (Sprite2D i in button.UpSprites)
                i.Update(gameTime);
        }
    }
}
