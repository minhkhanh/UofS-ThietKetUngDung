using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class GameButtonDownState : GameButtonState
    {
        protected override void OnChangeState(GameButton button)
        {
            List<Sprite2D> tmp = button.Sprites;
            button.Sprites = button.DownSprites;
            button.DownSprites = tmp;
        }
    }
}
