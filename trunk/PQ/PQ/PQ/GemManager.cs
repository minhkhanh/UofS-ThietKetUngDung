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

            Texture2D largePic = content.Load<Texture2D>(@"\Images\Skin_Gems_Grid");

            ImageSplittingDetails details = new ImageSplittingDetails(2, 7, 0, 0, 71, 66, 1, 6, 0, 2);
            List<Texture2D> images = GlobalClass.SplitImage(largePic, details);

            Gem gem = new Gem();
            gem.Sprites.Add(new Sprite2D(new List<Texture2D>() { images[0] }, 0, 0));

        }
    }
}
