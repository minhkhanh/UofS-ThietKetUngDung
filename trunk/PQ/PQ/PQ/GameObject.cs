using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PQ
{
    public enum GameOnMouseState
    {
        Up,
        Down,
        Hover,
    }

    public abstract class GameObject
    {
        #region motion

        PlaneMotionModule _motionModule = new PlaneMotionModule();

        public PlaneMotionModule MotionModule
        {
            get { return _motionModule; }
            set { _motionModule = value; }
        }

        #endregion

        #region drawing

        protected List<Sprite2D> _sprites = new List<Sprite2D>();
        public List<Sprite2D> Sprites
        {
            get { return _sprites; }
            set { _sprites = value; }
        }

        private float x;
        public virtual float X
        {
            get { return x; }
            set 
            {
                float dx = value - x;
                MoveSprites(dx, 0);
                x = value;
            }
        }

        private float y;
        public virtual float Y
        {
            get { return y; }
            set 
            {
                float dy = value - y;
                MoveSprites(0, dy);
                y = value;
            }
        }

        public virtual void Init()
        {

        }

        protected virtual void MoveSprites(float dx, float dy)
        {
            if (dx != 0)
                for (int i = 0; i < _sprites.Count; ++i)
                    _sprites[i].X += dx;

            if (dy != 0)
                for (int i = 0; i < _sprites.Count; ++i)
                    _sprites[i].Y += dy;
        }

        public virtual GameObject Clone()
        {
            return null;
        }

        public void Animate(int frameTicks)
        {
            foreach (Sprite2D i in _sprites)
                i.Animate(frameTicks, -1, -1);
        }

        public void Pause()
        {
            for (int i = 0; i < _sprites.Count; ++i)
                _sprites[i].Pause();
        }

        public virtual List<Rectangle> Regions
        {
            get
            {
                List<Rectangle> reg = new List<Rectangle>();

                for (int i = 0; i < _sprites.Count; ++i)
                    reg.Add(_sprites[i].Bound);

                return reg;
            }
        }

        public virtual Rectangle Bound
        {
            get
            {
                Rectangle bound = new Rectangle();
                if (_sprites.Count > 0)
                    bound = _sprites[0].Bound;
                for (int i = 1; i < _sprites.Count; ++i)
                    bound = Rectangle.Union(bound, _sprites[i].Bound);

                return bound;
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Sprite2D i in _sprites)
                i.Draw(gameTime, spriteBatch);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Sprite2D i in _sprites)
                i.Update(gameTime);

            _motionModule.OnMotion(this, gameTime);
        }
        #endregion

        #region events

        protected GameOnMouseState _clickState = GameOnMouseState.Up;

        protected bool ContainMouse(int mouseX, int mouseY)
        {
            List<Rectangle> regs = Regions;

            for (int i = 0; i < regs.Count; ++i)
                if (regs[i].Contains(mouseX, mouseY))
                    return true;

            return false;
        }

        public event EventHandler<GameMouseEventArgs> MouseDown;
        public event EventHandler<GameMouseEventArgs> MouseUp;
        public event EventHandler<GameMouseEventArgs> MouseHover;
        public event EventHandler<GameMouseEventArgs> MouseLeave;

        protected void RaiseMouseUpEvent(object o, GameMouseEventArgs e)
        {
            EventHandler<GameMouseEventArgs> handler = MouseUp;
            if (handler != null)
                handler(o, e);
        }

        protected void RaiseMouseHoverEvent(object o, GameMouseEventArgs e)
        {
            EventHandler<GameMouseEventArgs> handler = MouseHover;
            if (handler != null)
                handler(o, e);
        }

        protected void RaiseMouseLeaveEvent(object o, GameMouseEventArgs e)
        {
            EventHandler<GameMouseEventArgs> handler = MouseLeave;
            if (handler != null)
                handler(o, e);
        }

        protected void RaiseMouseDownEvent(object o, GameMouseEventArgs e)
        {
            EventHandler<GameMouseEventArgs> handler = MouseDown;
            if (handler != null)
                handler(o, e);
        }

        public virtual void OnMouseDown(object o, GameMouseEventArgs e)
        {
            if (_clickState != GameOnMouseState.Down
                && ContainMouse(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Down;
                RaiseMouseDownEvent(o, e);
            }
        }

        public virtual void OnMouseUp(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Down)
            {
                _clickState = GameOnMouseState.Up;
                RaiseMouseUpEvent(o, e);
            }
        }

        public virtual void OnMouseHover(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Up
                && ContainMouse(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Hover;
                RaiseMouseHoverEvent(o, e);
            }
        }

        public virtual void OnMouseLeave(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Hover
                && !ContainMouse(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Up;
                RaiseMouseLeaveEvent(o, e);
            }
        }

        #endregion

    }
}
