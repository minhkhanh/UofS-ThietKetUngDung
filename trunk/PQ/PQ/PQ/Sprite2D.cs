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
        public Rectangle Bound
        {
            get { return new Rectangle((int)_x, (int)_y, (int)_frameWidth, (int)_frameHeight); }
        }

        protected float _frameWidth = 0;
        protected float _frameHeight = 0;

        protected Vector2 _scale = new Vector2(1, 1);
        public Vector2 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        
        protected float _rotation = 0;
        protected SpriteEffects _fliping = SpriteEffects.None;
        Nullable<Rectangle> _srcRect = null;

        int _beginFrame = 0;
        public int BeginFrame
        {
            get { return _beginFrame; }
            set
            {
                if (value < 0)
                    _beginFrame = 0;
                else
                    _beginFrame = value % FrameCount;
            }
        }

        int _endFrame = 0;
        public int EndFrame
        {
            get { return _endFrame; }
            set
            {
                if (value < 0)
                    _endFrame = FrameCount - 1;
                else
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

        List<Texture2D> _frames = new List<Texture2D>();
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

        public Sprite2D(Sprite2D sprite)
        {
            _frames = sprite._frames;

            _x = sprite._x;
            _y = sprite._y;

            _frameWidth = sprite._frameWidth;
            _frameHeight = sprite._frameHeight;
        }

        public Sprite2D(Texture2D[] frames, int x, int y)
        {
            _x = x;
            _y = y;

            if (frames.Count() == 0)
                return;

            _frames.AddRange(frames);

            _frameWidth = frames[0].Width;
            _frameHeight = frames[0].Height;
        }

        public Sprite2D(Texture2D largeTxture, int x, int y, ImageSplittingDetails details)
        {
            _frameWidth = details.FrameWidth;
            _frameHeight = details.FrameHeight;

            float limHeight = Math.Min(largeTxture.Height, details.InitMarginY + (details.RowCount + details.RowIndex) * (_frameHeight + details.SpaceY));
            float limWidth = Math.Min(largeTxture.Width, details.InitMarginX + (details.ColumnCount + details.ColumnIndex) * (_frameWidth + details.SpaceX));

            float initI = 0, initJ = 0, incrI = 0, incrJ = 0, limI = 0, limJ = 0;

            // khoi tao thong so ban dau danh cho vong lap for:
            if (details.SplittingDirection == SplittingDirection.Vertically)    // cat anh theo chieu doc
            {
                initI = details.InitMarginX + details.ColumnIndex * (_frameWidth + details.SpaceX);
                initJ = details.InitMarginY + details.RowIndex * (_frameHeight + details.SpaceY);
                limI = limWidth;
                limJ = limHeight;
                incrI = _frameWidth + details.SpaceX;
                incrJ = _frameHeight + details.SpaceY;                
            }
            else if (details.SplittingDirection == SplittingDirection.Horizontally) // cat anh theo chieu ngang
            {
                initI = details.InitMarginY + details.RowIndex * (_frameHeight + details.SpaceY);
                initJ = details.InitMarginX + details.ColumnIndex * (_frameWidth + details.SpaceX);
                limI = limHeight;
                limJ = limWidth;
                incrI = _frameHeight + details.SpaceY;
                incrJ = _frameWidth + details.SpaceX;
            }

            for (float i = initI; i < limI; i += incrI)
                for (float j = initJ; j < limJ; j += incrJ)
                {
                    Color[] singleFrame = new Color[(int)_frameWidth * (int)_frameHeight];
                    largeTxture.GetData<Color>(0, new Rectangle((int)i, (int)j, (int)_frameWidth, (int)_frameHeight), singleFrame, 0, (int)_frameWidth * (int)_frameHeight);

                    Texture2D newTxture = new Texture2D(largeTxture.GraphicsDevice, (int)_frameWidth, (int)_frameHeight);
                    newTxture.SetData<Color>(singleFrame);
                    _frames.Add(newTxture);
                }

            _x = x;
            _y = y;

            CurrentFrame = FrameCount - 1;
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
                    _currFrame = (_currFrame + 1) % FrameCount;
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_frames.Count != 0)
                spriteBatch.Draw(_frames[CurrentFrame], Position, _srcRect, Color.White, _rotation, Vector2.Zero, _scale, _fliping, 0);
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
