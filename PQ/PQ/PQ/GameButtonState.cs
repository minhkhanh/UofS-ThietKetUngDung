using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public abstract class GameButtonState
    {
        protected virtual void OnChangeState(GameButton button)
        {

        }

        public virtual void OnEnterState(GameButton button)
        {
            //OnChangeState(button);
        }

        public virtual void OnExitState(GameButton button)
        {
            //OnChangeState(button);
        }

        public virtual void OnDraw(GameButton button, GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public virtual void OnUpdate(GameButton button, GameTime gameTime)
        {

        }
    }
}
