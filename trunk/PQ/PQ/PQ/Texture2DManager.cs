using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public abstract class Texture2DManager
    {
        protected List<Texture2D> _textures = new List<Texture2D>();

        public virtual void InitPrototypes(ContentManager content)
        {
            
        }

        public virtual Texture2D CreateObject(int idx)
        {
            return null;
        }
    }
}
