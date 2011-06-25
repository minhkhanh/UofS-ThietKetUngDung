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
        public override void Play(float fLen)
        {
            _bPlay = true;
            _lenMoving = fLen;
            _fTimeOld = 0;
        }
        public override void Pause()
        {
            _bPlay = false;
            _fTimeOld = 0;
        }
        public override void Stop()
        {
            base.Stop();
            _bPlay = false;
            _lenMoving = 0;
            _gameObj.DirectionSprite = _directOld;
        }
        int _directOld = 0;
        GameObject _gameObj = null;
        int _direct = 0;
        public int Direct
        {
            get { return _direct; }
            set { _direct = value; }
        }
        double _lenMoving = 0;

        public double LenMoving
        {
            get { return _lenMoving; }
            set { _lenMoving = value; }
        }
        public MovingPlaneMotionModule(float vx, float vy, double fLen, GameObject obj, int iDirect)
            : base(vx, vy)
        {
            _lenMoving = fLen;
            _gameObj = obj;
            _directOld = obj.DirectionSprite;
            _direct = iDirect;
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
                if (_bPlay && _directOld != _gameObj.DirectionSprite)
                {
                    _gameObj.DirectionSprite = _directOld;
                }
                _bPlay = false;
                return;
            }
            if (_fTimeOld>0)
            {
                Vector2 l = new Vector2((float)(Vx * (gameTime.TotalGameTime.TotalSeconds - _fTimeOld)),(float)(Vy * (gameTime.TotalGameTime.TotalSeconds - _fTimeOld)));
                _lenMoving -= l.Length();
                _gameObj.LogicalX += l.X;
                _gameObj.LogicalY += l.Y;
            }
            if (_direct != gameObj.DirectionSprite)
            {
                _gameObj.DirectionSprite = _direct;
            }
            _fTimeOld = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}
