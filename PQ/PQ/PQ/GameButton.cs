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

        public SpriteFont Font
        {
            get 
            {
                return SpriteFontManager.CreateObject((int)FontName.Algerian_22_Bld); 
            }
        }

        string _caption = "";

        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        public GameButton()
        {
        }

        public override void Dispose()
        {
            base.Dispose();

            //for (int i = 0; i < _downSprites.Count; ++i)
            //{
            //    _downSprites[i].Dispose();
            //}

            _downSprites.Clear();

            //for (int i = 0; i < _upSprites.Count; ++i)
            //{
            //    _upSprites[i].Dispose();
            //}

            _upSprites.Clear();

            //for (int i = 0; i < _hoverSprites.Count; ++i)
            //{
            //    _hoverSprites[i].Dispose();
            //}

            _hoverSprites.Clear();
        }

        public GameButton(Sprite2D[] sprites, Sprite2D[] downSprites, Sprite2D[] upSprites, Sprite2D[] hoverSprites)
        {
            _sprites.AddRange(sprites);
            _downSprites.AddRange(downSprites);
            _upSprites.AddRange(upSprites);
            _hoverSprites.AddRange(hoverSprites);
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            _buttonState.OnDraw(this, gameTime, spriteBatch);

            Rectangle bound = Bounds;

            Vector2 textVect = Font.MeasureString(_caption);
            spriteBatch.DrawString(Font, _caption, GlobalClass.SCALE * new Vector2((bound.Width - textVect.X) / 2 + bound.X, (bound.Height - textVect.Y) / 2 + bound.Y+3), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            _buttonState.OnUpdate(this, gameTime);
            base.Update(gameTime);
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

        protected override List<Sprite2D> GetAllSprites()
        {
            List<Sprite2D> allSprites = new List<Sprite2D>();
            allSprites.AddRange(_sprites);
            allSprites.AddRange(_downSprites);
            allSprites.AddRange(_upSprites);
            allSprites.AddRange(_hoverSprites);

            return allSprites;
        }

        public override void OnMouseDown(object o, GameMouseEventArgs e)
        {
            if (_clickState != GameOnMouseState.Down
                && Contains(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Down;

                _buttonState.OnExitState(this);
                _buttonState = new GameButtonDownState();
                _buttonState.OnEnterState(this);

                RaiseMouseDownEvent(this, e);
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

                RaiseMouseUpEvent(this, e);
            }
        }

        public override void OnMouseHover(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Up
                && Contains(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Hover;

                _buttonState.OnExitState(this);
                _buttonState = new GameButtonHoverState();
                _buttonState.OnEnterState(this);

                RaiseMouseHoverEvent(this, e);
            }
        }

        public override void OnMouseLeave(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Hover
                && !Contains(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Up;

                _buttonState.OnExitState(this);
                _buttonState = new GameButtonUpState();
                _buttonState.OnEnterState(this);

                RaiseMouseLeaveEvent(this, e);
            }
        }
    }
}
