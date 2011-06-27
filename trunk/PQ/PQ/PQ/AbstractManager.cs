using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace PQ
{
    public abstract class AbstractManager
    {
        //protected List<object> _prototypes = new List<object>();
        protected Dictionary<int, object> _prototypes = new Dictionary<int, object>();
        public virtual void LoadPrototypes(ContentManager content)
        {

        }

        public abstract object CreateObject(int idx);

        public virtual object RandomObject()
        {
            Random rand = new Random();
            return CreateObject(rand.Next(_prototypes.Count));
        }
    }
}
