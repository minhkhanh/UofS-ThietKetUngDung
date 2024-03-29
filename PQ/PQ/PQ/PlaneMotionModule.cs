﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class PlaneMotionModule
    {
        //protected float _ax;
        //public float Ax
        //{
        //    get { return _ax; }
        //    set { _ax = value; }
        //}

        //protected float _ay;
        //public float Ay
        //{
        //    get { return _ay; }
        //    set { _ay = value; }
        //}

        protected float _vx;
        public float Vx
        {
            get { return _vx; }
            set { _vx = value; }
        }

        protected float _vy;
        public float Vy
        {
            get { return _vy; }
            set { _vy = value; }
        }

        public Vector2 Velocity
        {
            get { return new Vector2(_vx, _vy); }
            set
            {
                _vx = value.X;
                _vy = value.Y;
            }
        }

        public PlaneMotionModule()
        {
            _vx = _vy = 0;
        }

        public PlaneMotionModule(float vx, float vy)
        {
            _vx = vx;
            _vy = vy;
        }
        public virtual void Play()
        {
        }
        public virtual void Play(float fTime)
        {
        }
        public virtual void Pause()
        {
        }

        protected bool _playing = true;
        public bool IsPlaying
        {
            get { return _playing; }
            set 
            {
                _playing = value;
                if (value == false)
                    Stop();
            }
        }


        public virtual void Stop()
        {
            _vx = _vy = 0;
        }

        public virtual void OnMotion(GameObject gameObj, GameTime gameTime)
        {
            gameObj.X += _vx;
            gameObj.Y += _vy;
        }

        public virtual bool IsMoving
        {
            get { return false; }
        }

        public virtual Direction MovingDirection
        {
            get { return Direction.None; }
        }
    }
}
