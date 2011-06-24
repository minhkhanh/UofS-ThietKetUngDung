using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class SelectedGemEffect: Sprite2D
    {
        public Vector2 RotationOrigin
        {
            get { return new Vector2(Width / 2, Height / 2); }
        }

        public SelectedGemEffect(Texture2D txt2d, float x, float y, SplittingDetails detals)
            : base(txt2d, x, y, detals)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_frames.Count != 0)
            {
                //Vector2 rotOrigin = new Vector2(Width/2, Height/2);
                spriteBatch.Draw(_frames[CurrentFrame], Position * GlobalClass.SCALE + RotationOrigin, _srcRect, Color.White, _rotation, RotationOrigin, _scale * GlobalClass.SCALE, _fliping, 1);
            }
        }
    }
}
