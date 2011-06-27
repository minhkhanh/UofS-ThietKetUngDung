using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PQ
{
    enum FontName
    {
        Algerian_22_Bld,
        Tahoma_S_Bld,
    }

    public static class SpriteFontManager
    {
        static Dictionary<int, SpriteFont> _prototypes;
        public static void LoadPrototypes(ContentManager content)
        {
            _prototypes = new Dictionary<int, SpriteFont>();

            _prototypes.Add((int)FontName.Algerian_22_Bld, content.Load<SpriteFont>(@"Fonts\Algerian_22_Bold"));
            _prototypes.Add((int)FontName.Tahoma_S_Bld, content.Load<SpriteFont>(@"Fonts\Tahoma_S_Bld"));
        }

        public static SpriteFont CreateObject(int idx)
        {
            return _prototypes[idx];
        }
    }
}
