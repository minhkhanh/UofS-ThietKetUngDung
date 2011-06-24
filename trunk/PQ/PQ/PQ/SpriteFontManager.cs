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
        Algerian,
    }

    public class SpriteFontManager: AbstractManager
    {
        public override void LoadPrototypes(ContentManager content)
        {
            _prototypes.Add((int)FontName.Algerian, content.Load<SpriteFont>(@"Fonts\Algerian"));
        }

        public override object CreateObject(int idx)
        {
            return _prototypes[idx];
        }
    }
}
