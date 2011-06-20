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
        Click,
        Hover,
    }

    public abstract class GameObject: IDisposable
    {
        #region motion

        protected PlaneMotionModule _motionModule = new PlaneMotionModule();

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

                List<Sprite2D> allSprites = GetAllSprites();
                foreach (Sprite2D i in allSprites)
                    i.X += dx;

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

                List<Sprite2D> allSprites = GetAllSprites();
                foreach (Sprite2D i in allSprites)
                    i.Y += dy;

                y = value;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(X / 2, Y / 2);
            }
            set 
            {
                X = value.X - Bounds.Width / 2;
                Y = value.Y - Bounds.Height / 2;
            }
        }

        public virtual GameObject Clone()
        {
            return null;
        }

        public virtual void Dispose()
        {
            //for (int i = 0; i < _sprites.Count; ++i )
            //{
            //    _sprites[i].Dispose();
            //}

            _sprites.Clear();
        }

        public void Animate(int frameTicks)
        {
            List<Sprite2D> allSprites = GetAllSprites();

            foreach (Sprite2D i in allSprites)
                i.Animate(frameTicks, -1, -1);
        }

        public void Pause()
        {
            List<Sprite2D> allSprites = GetAllSprites();

            foreach (Sprite2D i in allSprites)
                i.Pause();
        }

        protected virtual List<Sprite2D> GetAllSprites()
        {
            return _sprites;
        }

        public virtual List<Rectangle> Regions
        {
            get
            {
                List<Rectangle> reg = new List<Rectangle>();

                List<Sprite2D> allSprites = GetAllSprites();
                foreach (Sprite2D i in allSprites)
                    reg.Add(i.Bound);

                return reg;
            }
        }

        public virtual Rectangle Bounds
        {
            get
            {
                Rectangle bound = new Rectangle();
                List<Sprite2D> allSprites = GetAllSprites();

                if (allSprites.Count > 0)
                    bound = allSprites[0].Bound;

                foreach (Sprite2D i in allSprites)
                    bound = Rectangle.Union(bound, i.Bound);

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

        protected bool Contains(int px, int py)
        {
            List<Rectangle> regs = Regions;

            for (int i = 0; i < regs.Count; ++i)
                if (regs[i].Contains(px, py))
                    return true;

            return false;
        }

        public event EventHandler<GameMouseEventArgs> MouseDown;
        public event EventHandler<GameMouseEventArgs> MouseUp;
        public event EventHandler<GameMouseEventArgs> MouseHover;
        public event EventHandler<GameMouseEventArgs> MouseLeave;
        public event EventHandler<GameMouseEventArgs> MouseClick;

        public event EventHandler<GameKeyEventArgs> KeyDown;
        public event EventHandler<GameKeyEventArgs> KeyUp;

        protected void RaiseKeyDownEvent(object o, GameKeyEventArgs e)
        {
            EventHandler<GameKeyEventArgs> handler = KeyDown;
            if (handler != null)
                handler(o, e);
        }

        protected void RaiseMouseClickEvent(object o, GameMouseEventArgs e)
        {
            EventHandler<GameMouseEventArgs> handler = MouseClick;
            if (handler != null)
                handler(o, e);
        }

        protected void RaiseMouseUpEvent(object o, GameMouseEventArgs e)
        {
            EventHandler<GameMouseEventArgs> handler = MouseUp;
            if (handler != null)
                handler(o, e);

            if (Contains(e.MouseState.X, e.MouseState.Y))
                RaiseMouseClickEvent(o, e);
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

        public virtual void OnKeyDown(object o, GameKeyEventArgs e)
        {

        }

        public virtual void OnMouseDown(object o, GameMouseEventArgs e)
        {
            if (_clickState != GameOnMouseState.Down
                && Contains(e.MouseState.X, e.MouseState.Y)
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
                //if (Contains(e.MouseState.X, e.MouseState.Y))
                //    RaiseMouseClickEvent(o, e);
            }
        }

        public virtual void OnMouseHover(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Up
                && Contains(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Hover;
                RaiseMouseHoverEvent(o, e);
            }
        }

        public virtual void OnMouseLeave(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Hover
                && !Contains(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Up;
                RaiseMouseLeaveEvent(o, e);
            }
        }

        #endregion

    }
}
