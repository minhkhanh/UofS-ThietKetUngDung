using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class Gem: GameEntity
    {
        bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        //SelectedGemEffect _selGemEffect;

        //public Gem(Sprite2DManager spriteManager)
        //{
        //    _selGemEffect = spriteManager.CreateObject(Sprite2DName.SelectedGem) as SelectedGemEffect;
        //}

        public override GameObject Clone()
        {
            Gem gem = new Gem();
            foreach (Sprite2D i in _sprites)
            {
                gem._sprites.Add(new Sprite2D(i));
            }

            gem.X = this.X;
            gem.Y = this.Y;
            gem.MotionModule = new VerticalPlaneMotionModule(0, 0, 0, 0);

            return gem;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            //if (_isSelected)
                //_selGemEffect.Draw(gameTime, spriteBatch);
        }
    }
}
