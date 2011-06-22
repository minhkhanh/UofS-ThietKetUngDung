using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class GemState
    {
        public virtual void OnChangeState(Gem gem)
        {
            // reserved
        }

        public virtual void OnEnterState(Gem gem)
        {
            
        }

        public virtual void OnExitState(Gem gem)
        {
            
        }

        public virtual void OnDraw(Gem gem, GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public virtual void OnUpdate(Gem gem, GameTime gameTime)
        {

        }
    }
}
