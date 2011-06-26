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
        GemSelectedEffect,
        GemGreen,
        GemRed,
        GemYellow,
        GemBlue,
        GemWhite,
        GemPurple,
        GemGold,
        Sparkle,
        GemWrongSelectedEffect,
    }

    public class Sprite2DManager: AbstractManager
    {
        //Dictionary<Sprite2DName, Sprite2D> _sprites = new Dictionary<Sprite2DName, Sprite2D>();

        public override void LoadPrototypes(ContentManager content)
        {
            //////////////////////////////////////////////////////////////////////////
            // Skin_Battle_Misc
            //////////////////////////////////////////////////////////////////////////
            
            Texture2D txt2dBattleMisc = content.Load<Texture2D>(@"Images\Skin_Battle_Misc");
            SplittingDetails details = new SplittingDetails(
                1,2,0,0,
                128,127,
                0,1,
                91,74
                );

            List<Texture2D> images = GlobalClass.SplitImage(txt2dBattleMisc, details);
            Sprite2D sprite = new GemSelectedEffect(new List<Texture2D>{images[1]}, 0, 0);
            _prototypes.Add((int)Sprite2DName.GemSelectedEffect, sprite);

            sprite = new GemWrongSelectedEffect(new List<Texture2D> { images[0] }, 0, 0);
            _prototypes.Add((int)Sprite2DName.GemWrongSelectedEffect, sprite);

            //////////////////////////////////////////////////////////////////////////
            // Skin_Gems_Grid
            //////////////////////////////////////////////////////////////////////////

            Texture2D txt2dGemsGrid = content.Load<Texture2D>(@"Images\Skin_Gems_Grid");

            details = new SplittingDetails(1, 7, 0, 0, 71, 66, 1, 6, 0, 2);
            images = GlobalClass.SplitImage(txt2dGemsGrid, details);
            sprite = new Sprite2D(new List<Texture2D> { images[0] }, 0, 0);
            _prototypes.Add((int)Sprite2DName.GemGreen, sprite);

            sprite = new Sprite2D(new List<Texture2D> { images[1] }, 0, 0);
            _prototypes.Add((int)Sprite2DName.GemRed, sprite);

            sprite = new Sprite2D(new List<Texture2D> { images[2] }, 0, 0);
            _prototypes.Add((int)Sprite2DName.GemYellow, sprite);

            sprite = new Sprite2D(new List<Texture2D> { images[3] }, 0, 0);
            _prototypes.Add((int)Sprite2DName.GemBlue, sprite);

            sprite = new Sprite2D(new List<Texture2D> { images[4] }, 0, 0);
            _prototypes.Add((int)Sprite2DName.GemWhite, sprite);

            sprite = new Sprite2D(new List<Texture2D> { images[5] }, 0, 0);
            _prototypes.Add((int)Sprite2DName.GemPurple, sprite);

            sprite = new Sprite2D(new List<Texture2D> { images[6] }, 0, 0);
            _prototypes.Add((int)Sprite2DName.GemGold, sprite);

            //details = new SplittingDetails(1,7)

            //////////////////////////////////////////////////////////////////////////
            // Particles
            //////////////////////////////////////////////////////////////////////////

            Texture2D txt2dParticle = content.Load<Texture2D>(@"Images\Sparkle");
            sprite = new Sprite2D(new List<Texture2D> { txt2dParticle }, 0, 0);
            _prototypes.Add((int)Sprite2DName.Sparkle, sprite);            

        }

        public override object CreateObject(int idx)
        {
            return (_prototypes[idx] as Sprite2D);
        }
    }
}
