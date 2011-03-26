using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace PQ
{
    public abstract class EntityManager
    {
        protected List<GameEntity> _prototypes = new List<GameEntity>();

        public virtual GameEntity CreateObject(int idx)
        {
            if (idx >= 0 && idx < _prototypes.Count)
                return (GameEntity)_prototypes[idx].Clone();

            return null;
        }

        public virtual void InitPrototypes(ContentManager contentManager)
        {
            //_prototypeCount = 0;
        }
    }
}
