using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public abstract class GameEntity : GameObject
    {
        public override GameObject Clone()
        {
            return null;
        }
    }
}
