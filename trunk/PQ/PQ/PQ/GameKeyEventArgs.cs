using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PQ
{
    public class GameKeyEventArgs: EventArgs
    {
        KeyboardState _kbState;

        public GameKeyEventArgs(KeyboardState kbState)
        {
            _kbState = kbState;
        }

        public KeyboardState KeyboardState
        {
            get { return _kbState; }
            set { _kbState = value; }
        }
    }
}
