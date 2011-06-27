using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    enum Texture2DName
    {
        CursorMain,
        PortraitHeroKnight0,
        PortraitHeroKnight1
    }

    public class Texture2DManager: AbstractManager
    {
        public override object CreateObject(int idx)
        {
            return _prototypes[idx] as Texture2D;
        }

        public override void LoadPrototypes(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            //////////////////////////////////////////////////////////////////////////
            // Cursors
            //////////////////////////////////////////////////////////////////////////

            Texture2D txt2d = content.Load<Texture2D>(@"Images\CursorMain");
            _prototypes.Add((int)Texture2DName.CursorMain, txt2d);

            //////////////////////////////////////////////////////////////////////////
            // Mana bars
            //////////////////////////////////////////////////////////////////////////

            //txt2d = content.Load<Texture2D>(@"Images\")
        }
    }
}
