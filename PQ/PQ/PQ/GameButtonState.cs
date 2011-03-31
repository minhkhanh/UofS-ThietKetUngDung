using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public abstract class GameButtonState
    {
        protected virtual void OnChangeState(GameButton button)
        {

        }

        public virtual void OnEnterState(GameButton button)
        {
            OnChangeState(button);
        }

        public virtual void OnExitState(GameButton button)
        {
            OnChangeState(button);
        }
    }
}
