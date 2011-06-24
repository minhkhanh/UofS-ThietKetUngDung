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
            set 
            { 
                _sprites = value; 
                foreach (Sprite2D s in value)
                {
                    s.GameObjectParent = this;
                }
            }
        }
        public void UpdateChild()
        {
            foreach (Sprite2D s in _sprites)
            {
                s.GameObjectParent = this;
            }
        }
        private float _logicalX = 0;

        public float LogicalX
        {
            get { return _logicalX; }
            set { _logicalX = value; }
        }
        private float _logicalY = 0;

        public float LogicalY
        {
            get { return _logicalY; }
            set { _logicalY = value; }
        }
        private GameObject _gameObjectParent = null;

        public GameObject GameObjectParent
        {
            get { return _gameObjectParent; }
            set { _gameObjectParent = value; }
        }
        //private float x;
        public virtual float PhysicalX
        {
            get 
            { 
                if (_gameObjectParent==null)
                {
                    return _logicalX;
                } 
                else
                {
                    return _logicalX + _gameObjectParent.PhysicalX;
                }
            }
        }
        public virtual float PhysicalY
        {
            get
            {
                if (_gameObjectParent == null)
                {
                    return _logicalY;
                }
                else
                {
                    return _logicalY + _gameObjectParent.PhysicalY;
                }
            }
        }

        //private float y;
        public virtual float X
        {
            get { return LogicalX; }
            set 
            {
                float dx = value - LogicalX;

                List<Sprite2D> allSprites = GetAllSprites();
                foreach (Sprite2D i in allSprites)
                    i.X += dx;

                //x = value;
                LogicalX = value;
            }
            //set { LogicalX = value; }
        }
        public virtual float Y
        {
            get { return LogicalY; }
            set 
            {
                float dy = value - LogicalY;

                List<Sprite2D> allSprites = GetAllSprites();
                foreach (Sprite2D i in allSprites)
                    i.Y += dy;

                //y = value;
                LogicalY = value;
            }
            //set { LogicalY = value; }
        }
        public Vector2 ConvertPhysical2Logical(Vector2 physical)
        {
            Vector2 v = new Vector2(PhysicalX, PhysicalY);
            v = physical - v;
            return v;
        }
        //public Vector2 Position
        //{
        //    get { return new Vector2(X, Y); }
        //    set
        //    {
        //        X = value.X;
        //        Y = value.Y;
        //    }
        //}

        // use Bounds.Center instead
        //public Vector2 Center
        //{
        //    get
        //    {
        //        Rectangle bound = Bounds;
        //        return new Vector2(X + bound.Width/2f, Y + bound.Height/2f);
        //    }
        //    set 
        //    {
        //        Rectangle bound = Bounds;
        //        X = value.X - bound.Width / 2f;
        //        Y = value.Y - bound.Height / 2f;
        //    }
        //}

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

        /// <summary>
        /// Get all sprites that being held by this object
        /// </summary>
        /// <returns></returns>
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
                    reg.Add(i.Bounds);

                return reg;
            }
        }

        public virtual int Width
        {
            get { return Bounds.Width; }
        }

        public virtual int Height
        {
            get { return Bounds.Height; }
        }

        public virtual Rectangle Bounds
        {
            get
            {
                Rectangle bound = Rectangle.Empty;
                List<Sprite2D> allSprites = GetAllSprites();

                if (allSprites.Count > 0)
                    bound = allSprites[0].Bounds;

                foreach (Sprite2D i in allSprites)
                    bound = Rectangle.Union(bound, i.Bounds);

                return bound;
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<Sprite2D> allSprites = GetAllSprites();
            foreach (Sprite2D i in allSprites)
                i.Draw(gameTime, spriteBatch);
        }

        public virtual void Update(GameTime gameTime)
        {
            List<Sprite2D> allSprites = GetAllSprites();
            foreach (Sprite2D i in allSprites)
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
        public event EventHandler<GameMouseEventArgs> MouseMove;

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
                RaiseMouseClickEvent(this, e);
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

        public virtual void OnMouseMove(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Down
                && Contains(e.MouseState.X, e.MouseState.Y)
                )
            {
                // not yet
            }
        }

        public virtual void OnKeyDown(object o, GameKeyEventArgs e)
        {
            RaiseKeyDownEvent(this, e);
        }

        public virtual void OnMouseDown(object o, GameMouseEventArgs e)
        {
            if (_clickState != GameOnMouseState.Down
                && Contains(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Down;
                RaiseMouseDownEvent(this, e);
            }
        }

        public virtual void OnMouseUp(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Down)
            {
                _clickState = GameOnMouseState.Up;
                RaiseMouseUpEvent(this, e);
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
                RaiseMouseHoverEvent(this, e);
            }
        }

        public virtual void OnMouseLeave(object o, GameMouseEventArgs e)
        {
            if (_clickState == GameOnMouseState.Hover
                && !Contains(e.MouseState.X, e.MouseState.Y)
                )
            {
                _clickState = GameOnMouseState.Up;
                RaiseMouseLeaveEvent(this, e);
            }
        }

        #endregion

        #region collision

        public bool BounRectCollide(GameObject obj)
        {
            return Bounds.Intersects(obj.Bounds);
        }

        public bool IsCollided(GameObject obj)
        {
            return BounRectCollide(obj);
        }

        #endregion

        protected GameState _parent = null;
        public GameState Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
    }
}
