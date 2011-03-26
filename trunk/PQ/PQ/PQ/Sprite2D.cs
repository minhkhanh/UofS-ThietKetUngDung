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
            get { return new Rectangle(_x, _y, _frameWidth, _frameHeight); }
        }        

        protected int _frameWidth = 0;
        protected int _frameHeight = 0;

        int _beginFrame = 0;

        public int BeginFrame
        {
            get { return _beginFrame; }
        }

        int _endFrame = 0;

        public int EndFrame
        {
            get { return _endFrame; }
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

        private int currFrame = 0;
        protected int CurrentFrame
        {
            get { return currFrame; }
            set 
            {
                if (value < 0)
                    currFrame = 0;
                else if (value > _endFrame)
                    currFrame = _endFrame;
                else
                    currFrame = value;
            }
        }

        List<Texture2D> _frames = new List<Texture2D>();
        public List<Texture2D> Frames
        {
            get { return _frames; }
            set { _frames = value; }
        }

        int _x;
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        int _y;
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        //protected bool _visible = true;

        public Sprite2D(Sprite2D sprite, int x, int y)
        {
            _frames = sprite._frames;

            if (x < 0)
                _x = sprite._x;
            else
                _x = x;

            if (y < 0)
                _y = sprite._y;
            else
                _y = y;
        }

        public Sprite2D(Texture2D largeTxture, int x, int y, ImageSplittingDetails details)
        {
            _frameWidth = details.FrameWidth;
            _frameHeight = details.FrameHeight;

            int limHeight = Math.Min(largeTxture.Height, details.InitMarginY + (details.RowCount + details.RowIndex) * (_frameHeight + details.SpaceY));
            int limWidth = Math.Min(largeTxture.Width, details.InitMarginX + (details.ColumnCount + details.ColumnIndex) * (_frameWidth + details.SpaceX));

            int initI = 0, initJ = 0, incrI = 0, incrJ = 0, limI = 0, limJ = 0;

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

            for (int i = initI; i < limI; i += incrI)
                for (int j = initJ; j < limJ; j += incrJ)
                {
                    Color[] singleFrame = new Color[(_frameWidth * _frameHeight)];
                    largeTxture.GetData<Color>(0, new Rectangle(i, j, _frameWidth, _frameHeight), singleFrame, 0, (_frameWidth * _frameHeight));

                    Texture2D newTxture = new Texture2D(largeTxture.GraphicsDevice, _frameWidth, _frameHeight);
                    newTxture.SetData<Color>(singleFrame);
                    _frames.Add(newTxture);
                }

            _x = x;
            _y = y;

            CurrentFrame = FrameCount - 1;
        }

        public void SetFrameRange(int begin, int end)
        {
            // sap xep sao cho: begin < end
            if (begin > end)
            {
                int tmp = begin;
                begin = end;
                end = tmp;
            }

            // chuan hoa:
            if (begin < 0)
                begin = 0;
            if (end < 0)
                end = FrameCount - 1;

            _beginFrame = begin;
            _endFrame = end;
        }

        public bool Collide(Sprite2D sprite)
        {
            Rectangle thisRect = new Rectangle(_x, _y, _frameWidth, _frameHeight);
            Rectangle spriteRect = new Rectangle(sprite._x, sprite._y, sprite._frameWidth, sprite._frameHeight);

            return thisRect.Intersects(spriteRect);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_running && ++_tickCount >= _frameTicks)
            {
                _tickCount = 0;
                CurrentFrame = (CurrentFrame + 1) % (_endFrame - _beginFrame + 1) + _beginFrame;
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_frames.Count != 0)
                spriteBatch.Draw(_frames[CurrentFrame], new Vector2(_x, _y), Color.White);
        }

        public void Pause()
        {
            _running = false;
        }

        public void Animate(int frameTicks, int begin, int end)
        {
            _frameTicks = frameTicks;

            SetFrameRange(begin, end);
            CurrentFrame = 0;

            _running = true;
        }
    }
}
