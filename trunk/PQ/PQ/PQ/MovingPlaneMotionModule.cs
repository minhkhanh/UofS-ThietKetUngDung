using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            _timeMoving = fTime;
            _fTimeOld = 0;
        }
        public override void Pause()
        {
            _bPlay = false;
            _fTimeOld = 0;
        }
        double _timeMoving = 0;

        public double FrameTickMoving
        {
            get { return _timeMoving; }
            set { _timeMoving = value; }
        }
        public MovingPlaneMotionModule(float vx, float vy, double fTime)
            : base(vx, vy)
        {
            _timeMoving = fTime;
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
            if (!_bPlay || _timeMoving <= 0)
            {
                _bPlay = false;
                return;
            }
            if (_fTimeOld>0)
            {
                _timeMoving -= (gameTime.TotalGameTime.TotalSeconds - _fTimeOld);
                gameObj.LogicalX += (float)(Vx * (gameTime.TotalGameTime.TotalSeconds - _fTimeOld));
                gameObj.LogicalY += (float)(Vy * (gameTime.TotalGameTime.TotalSeconds - _fTimeOld));
            }
            _fTimeOld = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}
