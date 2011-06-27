using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class GemSelectedEffect: Sprite2D
    {
        float _angularVar = 0.02f;
        public Vector2 RotationOrigin
        {
            get { return new Vector2(Width / 2, Height / 2); }
        }

        public GemSelectedEffect(List<Texture2D> frames, int x, int y)
            : base(frames, x, y)
        {
        }

        public GemSelectedEffect(Texture2D txt2d, float x, float y, SplittingDetails detals)
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

        public override void Update(GameTime gameTime)
        {
            _rotation -= _angularVar;
            if (_rotation < 0)
                _rotation += 360;
        }

        public GemSelectedEffect(Sprite2D sprite)
            : base(sprite)
        {

        }

        public override Sprite2D Clone()
        {
            GemSelectedEffect sprite = new GemSelectedEffect(this);

            return sprite;
        }
    }
}
