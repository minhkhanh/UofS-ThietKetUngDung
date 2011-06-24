using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class MovingPlaneMotionModule : PlaneMotionModule
    {
        protected bool _bPlay = false;
        public override void Play()
        {
            _bPlay = true;
            _fTimeOld = 0;
        }
        public override void Play(float fTime)
        {
            _bPlay = true;
            _lenMoving = fTime;
            _fTimeOld = 0;
        }
        public override void Pause()
        {
            _bPlay = false;
            _fTimeOld = 0;
        }
        double _lenMoving = 0;

        public double LenMoving
        {
            get { return _lenMoving; }
            set { _lenMoving = value; }
        }
        public MovingPlaneMotionModule(float vx, float vy, double fLen)
            : base(vx, vy)
        {
            _lenMoving = fLen;
        }
        public override bool IsMoving
        {
            get
            {
                return _bPlay;
            }
        }
        double _fTimeOld = 0;
        public override void OnMotion(GameObject gameObj, Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (!_bPlay || _lenMoving <= 0)
            {
                _bPlay = false;
                return;
            }
            if (_fTimeOld>0)
            {
                Vector2 l = new Vector2((float)(Vx * (gameTime.TotalGameTime.TotalSeconds - _fTimeOld)),(float)(Vy * (gameTime.TotalGameTime.TotalSeconds - _fTimeOld)));
                _lenMoving -= l.Length();
                gameObj.LogicalX += l.X;
                gameObj.LogicalY += l.Y;
            }
            _fTimeOld = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}
