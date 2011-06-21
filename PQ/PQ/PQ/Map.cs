using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PQ
{
    public class Map: GameEntity
    {
        public override void OnKeyDown(object o, GameKeyEventArgs e)
        {
            Game mainGame = o as Game;
            if (mainGame == null)
                return;

            Rectangle gameBound = mainGame.GraphicsDevice.PresentationParameters.Bounds;
            Rectangle mapBound = Bounds;
            if (e.KeyboardState.IsKeyDown(Keys.Down))
            {
                if (mapBound.Bottom - 10 >= gameBound.Bottom)
                    Y -= 10;
            }
            if (e.KeyboardState.IsKeyDown(Keys.Up))
            {
                if (mapBound.Top + 10 <= gameBound.Top)
                    Y += 10;
            }
            if (e.KeyboardState.IsKeyDown(Keys.Left))
            {
                if (mapBound.Left + 10 <= gameBound.Left)
                    X += 10;
            }
            if (e.KeyboardState.IsKeyDown(Keys.Right))
            {
                if (mapBound.Right - 10 >= gameBound.Right)
                    X -= 10;
            }

            RaiseKeyDownEvent(this, e);
        }
    }
}
