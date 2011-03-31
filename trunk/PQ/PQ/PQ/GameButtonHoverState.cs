using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class GameButtonHoverState : GameButtonState
    {
        protected override void OnChangeState(GameButton button)
        {
            List<Sprite2D> tmp = button.Sprites;
            button.Sprites = button.HoverSprites;
            button.HoverSprites = tmp;
        }

        public override void OnEnterState(GameButton button)
        {
            base.OnEnterState(button);

            button.Animate(10);
        }

        public override void OnExitState(GameButton button)
        {
            base.OnExitState(button);

            button.Pause();
        }
    }
}
