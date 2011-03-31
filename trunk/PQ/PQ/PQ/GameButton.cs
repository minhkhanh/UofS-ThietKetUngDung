using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class GameButton : GameControl
    {
        List<Sprite2D> _downSprites = new List<Sprite2D>();

        public List<Sprite2D> DownSprites
        {
            get { return _downSprites; }
            set { _downSprites = value; }
        }
        List<Sprite2D> _upSprites = new List<Sprite2D>();

        public List<Sprite2D> UpSprites
        {
            get { return _upSprites; }
            set { _upSprites = value; }
        }

        List<Sprite2D> _hoverSprites = new List<Sprite2D>();

        public List<Sprite2D> HoverSprites
        {
            get { return _hoverSprites; }
            set { _hoverSprites = value; }
        }

        GameButtonState _buttonState = new GameButtonUpState();

        SpriteFont _font;

        public SpriteFont Font
        {
            get { return _font; }
            set { _font = value; }
        }

        string _caption = "";

        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            Rectangle bound = Bound;

            Vector2 textVect = _font.MeasureString(_caption);
            spriteBatch.DrawString(_font, _caption, new Vector2((bound.Width - textVect.X) / 2 + bound.X, (bound.Height - textVect.Y) / 2 + bound.Y), Color.White);//, 0, Vector2.Zero, 0, SpriteEffects.None, 0);
        }

        public override GameObject Clone()
        {
            GameButton btn = new GameButton();

            for (int i = 0; i < _sprites.Count; ++i)
                btn._sprites.Add(new Sprite2D(_sprites[i]));
            for (int i = 0; i < _downSprites.Count; ++i)
                btn._downSprites.Add(new Sprite2D(_downSprites[i]));
            for (int i = 0; i < _upSprites.Count; ++i)
                btn._upSprites.Add(new Sprite2D(_upSprites[i]));
            for (int i = 0; i < _hoverSprites.Count; ++i)
                btn._hoverSprites.Add(new Sprite2D(_hoverSprites[i]));

            btn.X = X;
            btn.Y = Y;

            return btn;
        }

        protected override void MoveSprites(float dx, float dy)
        {
            if (dx != 0)
            {
                for (int i = 0; i < _sprites.Count; ++i)
                    _sprites[i].X += dx;
                for (int i = 0; i < _downSprites.Count; ++i)
                    _downSprites[i].X += dx;
                for (int i = 0; i < _upSprites.Count; ++i)
                    _upSprites[i].X += dx;
                for (int i = 0; i < _hoverSprites.Count; ++i)
                    _hoverSprites[i].X += dx;
            }

            if (dy != 0)
            {
                for (int i = 0; i < _sprites.Count; ++i)
                    _sprites[i].Y += dy;
                for (int i = 0; i < _downSprites.Count; ++i)
                    _downSprites[i].Y += dy;
                for (int i = 0; i < _upSprites.Count; ++i)
                    _upSprites[i].Y += dy;
                for (int i = 0; i < _hoverSprites.Count; ++i)
                    _hoverSprites[i].Y += dy;
            }
        }

        public override List<Rectangle> Regions
        {
            get
            {
                List<Rectangle> regs = new List<Rectangle>();

                for (int i = 0; i < _sprites.Count; ++i)
                    regs.Add(_sprites[i].Bound);
                for (int i = 0; i < _downSprites.Count; ++i)
                    regs.Add(_downSprites[i].Bound);
                for (int i = 0; i < _upSprites.Count; ++i)
                    regs.Add(_upSprites[i].Bound);
                for (int i = 0; i < _hoverSprites.Count; ++i)
                    regs.Add(_hoverSprites[i].Bound);

                return regs;
            }
        }

        public override void OnMouseDown(object o, GameMouseEventArgs e)
        {
            if (_clickState != GameOnMouseState.Down
                && ContainMouse(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Down;

                _buttonState.OnExitState(this);
                _buttonState = new GameButtonDownState();
                _buttonState.OnEnterState(this);

                RaiseMouseDownEvent(o, e);
            }
        }

        public override void OnMouseUp(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Down)
            {
                _clickState = GameOnMouseState.Up;

                _buttonState.OnExitState(this);
                _buttonState = new GameButtonUpState();
                _buttonState.OnEnterState(this);

                RaiseMouseUpEvent(o, e);
            }
        }

        public override void OnMouseHover(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Up
                && ContainMouse(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Hover;

                _buttonState.OnExitState(this);
                _buttonState = new GameButtonHoverState();
                _buttonState.OnEnterState(this);

                RaiseMouseHoverEvent(o, e);
            }
        }

        public override void OnMouseLeave(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Hover
                && !ContainMouse(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Up;

                _buttonState.OnExitState(this);
                _buttonState = new GameButtonUpState();
                _buttonState.OnEnterState(this);

                RaiseMouseLeaveEvent(o, e);
            }
        }
    }
}
