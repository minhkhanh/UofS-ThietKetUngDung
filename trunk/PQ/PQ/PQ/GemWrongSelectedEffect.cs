using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class GemWrongSelectedEffect: Sprite2D
    {
        byte _alphaVar = 5;
        float _scaleVar = 0.02f;

        public float ScaleVar
        {
            get { return _scaleVar; }
            set { _scaleVar = value; }
        }
        Color _color = Color.White;

        public Color AlphaColor
        {
            get { return _color; }
            set { _color = value; }
        }
        
        public GemWrongSelectedEffect(List<Texture2D> frames, int x, int y)
            : base(frames, x, y)
        {
        }

        public GemWrongSelectedEffect(Texture2D txt2d, float x, float y, SplittingDetails detals)
            : base(txt2d, x, y, detals)
        {

        }

        public GemWrongSelectedEffect(Sprite2D sprite)
            : base(sprite) { }

        public override Sprite2D Clone()
        {
            GemWrongSelectedEffect sprite = new GemWrongSelectedEffect(this);

            return sprite;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_frames.Count != 0)
            {
                //Vector2 rotOrigin = new Vector2(Width/2, Height/2);
                spriteBatch.Draw(_frames[CurrentFrame], Position * GlobalClass.SCALE, _srcRect, _color, _rotation, Vector2.Zero, _scale * GlobalClass.SCALE, _fliping, 1);
            }
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 incrScale = _scale * _scaleVar;
            _scale += incrScale;

            if (_color.A <= _alphaVar)
            {
                //_color.A = 0;
                _color = Color.Black;
                _color.A = 0;
            }
            else
                _color.A -= _alphaVar;
            

            X -= (incrScale.X * Width) / 2f;
            Y -= (incrScale.Y * Height) / 2f;
        }

        public void Refresh()
        {
            _color = Color.White;
            _scale = new Vector2(1, 1);
        }
    }
}
