using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class GemManager: GameObjectManager
    {
        public override void LoadPrototypes(ContentManager content)
        {
            _prototypes = new List<GameObject>();

            Texture2D largePic = content.Load<Texture2D>(@"Images\Skin_Gems_Grid");

            SplittingDetails details = new SplittingDetails(2, 7, 0, 0, 71, 66, 1, 6, 0, 2);
            List<Texture2D> images = GlobalClass.SplitImage(largePic, details);

            //Gem[] gems = new Gem[14];

            for (int i = 0; i < images.Count; ++i)
            {
                Gem gem = new Gem();
                gem.MotionModule = new VerticalPlaneMotionModule(0, 0, 0, 0);
                gem.Sprites.Add(new Sprite2D(new List<Texture2D>() { images[i] }, 0, 0));

                _prototypes.Add(gem);
            }

            //_prototypes.AddRange(gems);
        }
    }
}
