using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class Gem: GameEntity
    {
        GemColorState _colorState = new GemColorState();
        public GemColorState ColorState
        {
            get { return _colorState; }
            //set { _colorState = value; }
        }

        public VerticalPlaneMotionModule VerticalMotionModule
        {
            get { return MotionModule as VerticalPlaneMotionModule; }
        }

        public void ChangeColorState(GemColorState state)
        {
            _colorState.OnExitState(this);
            _colorState = state;
            _colorState.OnEnterState(this);
        }

        public bool IsThrough
        {
            get
            {
                return _motionModule.IsMoving || _colorState.Name == GemName.None;
            }
        }

        public Gem()
        {
            _motionModule = new VerticalPlaneMotionModule();
            //_colorState = colorState;
        }

        public Gem(GemColorState clrState)
        {
            _motionModule = new VerticalPlaneMotionModule();
            _colorState = clrState;
        }

        public bool IsSameColor(Gem gem)
        {
            if (_colorState.Name == GemName.None)
                return false;

            return (_colorState.Name == gem._colorState.Name);
        }

        public override GameObject Clone()
        {
            Gem gem = new Gem(this.ColorState.Clone());

            foreach (Sprite2D i in _sprites)
            {
                gem._sprites.Add(new Sprite2D(i));
            }

            gem.X = this.X;
            gem.Y = this.Y;

            return gem;
        }
    }
}
