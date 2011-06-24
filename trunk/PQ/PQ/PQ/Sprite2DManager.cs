using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public enum Sprite2DName
    {
        SelectedGemEffect,
    }

    public class Sprite2DManager: AbstractManager
    {
        //Dictionary<Sprite2DName, Sprite2D> _sprites = new Dictionary<Sprite2DName, Sprite2D>();

        public override void LoadPrototypes(ContentManager content)
        {
            Texture2D txt2dBattleMisc = content.Load<Texture2D>(@"Images\Skin_Battle_Misc");
            SplittingDetails details = new SplittingDetails(
                1,1,0,0,
                120,120,0,0,
                224,78
                );
            Sprite2D sprite = new SelectedGemEffect(txt2dBattleMisc, 0, 0, details);
            _prototypes.Add((int)Sprite2DName.SelectedGemEffect, sprite);
        }

        public override object CreateObject(int idx)
        {
            return (_prototypes[idx] as Sprite2D);
        }
    }
}
