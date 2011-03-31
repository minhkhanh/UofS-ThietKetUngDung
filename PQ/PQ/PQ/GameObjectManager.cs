using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace PQ
{
    public abstract class GameObjectManager
    {
        protected List<GameObject> _prototypes = new List<GameObject>();

        public virtual GameObject CreateObject(int idx)
        {
            if (idx >= 0 && idx < _prototypes.Count)
                return _prototypes[idx].Clone();

            return null;
        }

        public virtual void InitPrototypes(ContentManager content)
        {
            //_prototypeCount = 0;
        }
    }
}
