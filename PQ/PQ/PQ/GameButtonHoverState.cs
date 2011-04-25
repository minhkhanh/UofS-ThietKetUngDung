using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class GameButtonHoverState : GameButtonState
    {
        protected override void OnChangeState(GameButton button)
        {
            List<Sprite2D> tmp = button.Sprites;
            button.Sprites = button.HoverSprites;
            button.HoverSprites = tmp;
        }

        public override void OnEnterState(GameButton button)
        {
            base.OnEnterState(button);

            button.Animate(10);
        }

        public override void OnExitState(GameButton button)
        {
            base.OnExitState(button);

            button.Pause();
        }

        public override void OnDraw(GameButton button, GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Sprite2D i in button.HoverSprites)
                i.Draw(gameTime, spriteBatch);
        }

        public override void OnUpdate(GameButton button, GameTime gameTime)
        {
            foreach (Sprite2D i in button.HoverSprites)
                i.Update(gameTime);
        }
    }
}
