using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PQ
{
    public abstract class GameObject
    {
        protected List<Sprite2D> _mainSprites = new List<Sprite2D>();
        public List<Sprite2D> MainSprites
        {
            get { return _mainSprites; }
            set { _mainSprites = value; }
        }

        protected int _x;
        protected int _y;

        public abstract GameObject Clone();

        public void Animate(int frameTicks)
        {
            foreach (Sprite2D i in _mainSprites)
                i.Animate(frameTicks, -1, -1);
        }

        public Rectangle[] MainRegions
        {
            get
            {
                Rectangle[] reg = new Rectangle[_mainSprites.Count];

                for (int i = 0; i < _mainSprites.Count; ++i)
                    reg[i] = _mainSprites[i].Bound;

                return reg;
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Sprite2D i in _mainSprites)
                i.Draw(gameTime, spriteBatch);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Sprite2D i in _mainSprites)
                i.Update(gameTime);
        }
    }
}
