using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    // GemName enum below is listed by loading order of gem images in the GemManager
    public enum GemName
    {
        GreenGem,
        X2Gem,
        RedGem,
        X3Gem,
        YellowGem,
        X4Gem,
        BlueGem,
        X5Gem,
        SkullGem,
        X6Gem,
        PurpleGem,
        X7Gem,
        GoldGem,
        X8Gem,
        None
    }

    public class GemManager: AbstractManager
    {
        public override void LoadPrototypes(ContentManager content)
        {
            //_prototypes = new List<object>();
            _prototypes = new Dictionary<int, object>();

            Texture2D largePic = content.Load<Texture2D>(@"Images\Skin_Gems_Grid");

            SplittingDetails details = new SplittingDetails(2, 7, 0, 0, 71, 66, 1, 6, 0, 2);
            List<Texture2D> images = GlobalClass.SplitImage(largePic, details);

            //Gem[] gems = new Gem[14];

            for (int i = 0; i < images.Count; ++i)
            {
                Gem gem = new Gem();
                gem.MotionModule = new VerticalPlaneMotionModule(0, 0, 0, 0);
                gem.Sprites.Add(new Sprite2D(new List<Texture2D>() { images[i] }, 0, 0));

                // important: index of image must be equal with GemName
                _prototypes.Add(i, gem);
            }

            //_prototypes.AddRange(gems);
        }

        public override object CreateObject(int idx)
        {
            return (_prototypes[idx] as Gem).Clone();
        }
    }
}
