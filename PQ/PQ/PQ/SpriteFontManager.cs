using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PQ
{
    public class SpriteFontManager
    {
        List<SpriteFont> _fonts = new List<SpriteFont>();

        public void LoadPrototypes(ContentManager content)
        {
            _fonts.Add(content.Load<SpriteFont>(@"Fonts\Algerian"));
        }

        public SpriteFont CreateObject(int idx)
        {
            if (idx < 0 || idx >= _fonts.Count)
                return null;

            return _fonts[idx];
        }
    }
}
