using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PQ
{
    public class Sprite2D
    {
        public Rectangle Bounds
        {
            get 
            {
                Rectangle bound;

                if (_frames.Count > 0)
                {
                    bound = _frames[0].Bounds;
                    //bound.X = (int)_x;
                    //bound.Y = (int)_y;
                }
                else
                    return Rectangle.Empty;

                foreach (Texture2D i in _frames)
                    bound = Rectangle.Union(bound, i.Bounds);

                bound.X = (int)_x;
                bound.Y = (int)_y;

                return bound;

                //return new Rectangle((int)_x, (int)_y, (int)_frameWidth, (int)_frameHeight); 
            }
        }

        public int Width
        {
            get { return Bounds.Width; }
        }

        public int Height
        {
            get { return Bounds.Height; }
        }

        //protected float _frameWidth = 0;
        //protected float _frameHeight = 0;

        protected Vector2 _scale = new Vector2(1, 1);
        public Vector2 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        protected float _rotation = 0;

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        protected SpriteEffects _fliping = SpriteEffects.None;
        protected Rectangle? _srcRect = null;

        int _beginFrame = 0;
        public int BeginFrame
        {
            get { return _beginFrame; }
            set
            {
                if (value >= 0)
                // _beginFrame = 0;
                //else
                {
                    _beginFrame = value % FrameCount;
                    _currFrame = _beginFrame;
                }
            }
        }
        int _directionCount = 0;
        int _direction = 0;
        public int Direction
        {
            get { return _direction; }
            set 
            { 
                if (value>=0 && value<=_directionCount && _directionCount>0)
                {
                    _direction = value;
                    BeginFrame = (_frames.Count / _directionCount) * _direction;
                    EndFrame = BeginFrame + (_frames.Count / _directionCount) -1;
                }                
            }
        }

        int _endFrame = 0;
        public int EndFrame
        {
            get { return _endFrame; }
            set
            {
                if (value >= 0)
                    //_endFrame = FrameCount - 1;
                //else
                    _endFrame = value % FrameCount;
            }
        }

        bool _running = false;

        int _frameTicks = 0;
        public int FrameTicks
        {
            get { return _frameTicks; }
            set { _frameTicks = value; }
        }

        int _tickCount = 0;

        public int FrameCount
        {
            get { return _frames.Count; }
        }

        private int _currFrame = 0;
        protected int CurrentFrame
        {
            get { return _currFrame; }
            set 
            {
                if (value < 0)
                    _currFrame = 0;
                else
                    _currFrame = value % FrameCount;
            }
        }

        protected List<Texture2D> _frames = new List<Texture2D>();
        public List<Texture2D> Frames
        {
            get { return _frames; }
            set { _frames = value; }
        }

        float _x;
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        float _y;
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(_x, _y);
            }
            set
            {
                _x = value.X;
                _y = value.Y;
            }
        }

        // use Bounds.Center instead
        //public Vector2 Center
        //{
        //    get 
        //    {
        //        Rectangle bound = Bounds;
        //        return new Vector2(X + bound.Width / 2f, Y + bound.Height / 2f); 
        //    }
        //}

        public Sprite2D() { }

        public Sprite2D(Sprite2D sprite)
        {
            _frames = sprite._frames;

            _x = sprite._x;
            _y = sprite._y;
            _directionCount = sprite._directionCount;
            Direction = 0;
            //_frameWidth = sprite._frameWidth;
            //_frameHeight = sprite._frameHeight;
        }

        public Sprite2D(List<Texture2D> frames, int x, int y)
        {
            _x = x;
            _y = y;

            if (frames.Count() == 0)
                return;

            _frames = frames;

            //_frameWidth = frames[0].Width;
            //_frameHeight = frames[0].Height;
        }

        public Sprite2D(Texture2D largeTxture, float x, float y, SplittingDetails details)
        {
            _frames = GlobalClass.SplitImage(largeTxture, details);

            _x = x;
            _y = y;

            CurrentFrame = FrameCount - 1;
        }

        public Sprite2D(Texture2D largeTxture, float x, float y, SplittingDetails details, int dirCount)
        {
            _frames = GlobalClass.SplitImage(largeTxture, details);

            _x = x;
            _y = y;            

            CurrentFrame = FrameCount - 1;

            _directionCount = dirCount;
            Direction = 0;
        }
        
        public virtual void Update(GameTime gameTime)
        {
            if (_running && ++_tickCount >= _frameTicks)
            {
                _tickCount = 0;
                //CurrentFrame = (CurrentFrame + 1) % (_endFrame - _beginFrame + 1) + _beginFrame;
                if (_currFrame == _endFrame)
                    _currFrame = _beginFrame;
                else
                    _currFrame = (_currFrame + 1);// % (_endFrame - _beginFrame + 1);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_frames.Count != 0)
                spriteBatch.Draw(_frames[CurrentFrame], Position * GlobalClass.SCALE, _srcRect, Color.White, _rotation, Vector2.Zero, _scale * GlobalClass.SCALE, _fliping, 0);
        }

        public void Pause()
        {
            _running = false;
        }

        public void Animate(int frameTicks, int begin, int end)
        {
            _frameTicks = frameTicks;

            BeginFrame = begin;
            EndFrame = end;
            CurrentFrame = BeginFrame;

            _running = true;
        }

        //public virtual void Dispose()
        //{
        //    for (int i = 0; i < _frames.Count; ++i )
        //    {
        //        _frames[i].Dispose();
        //    }

        //    _frames.Clear();
        //}
    }
}
