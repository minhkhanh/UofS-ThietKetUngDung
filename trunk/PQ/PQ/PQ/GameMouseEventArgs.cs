using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PQ
{
    public class GameMouseEventArgs: EventArgs
    {
        MouseState _mouseState;

        public GameMouseEventArgs(MouseState state)
        {
            _mouseState = state;
        }

        public MouseState MouseState
        {
            get { return _mouseState; }
            set { _mouseState = value; }
        }
    }
}
