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
        GemGreen,
        GemRed,
        GemYellow,
        GemBlue,
        GemWhite,
        GemPurple,
        GemGold,
        X2Gem,
        X3Gem,
        X4Gem,
        X5Gem,
        X6Gem,
        X7Gem,
        X8Gem,
        None,
    }

    public class GemManager: AbstractManager
    {
        public override void LoadPrototypes(ContentManager content)
        {
            //_prototypes = new List<object>();
            _prototypes = new Dictionary<int, object>();

            Texture2D txt2dGemsGrid = content.Load<Texture2D>(@"Images\Skin_Gems_Grid");

            SplittingDetails details;
            List<Texture2D> images;
            
            details = new SplittingDetails(1, 7, 0, 0, 71, 66, 1, 6, 0, 2);
            images = GlobalClass.SplitImage(txt2dGemsGrid, details);

            Gem gem = new Gem(new GemGreen());
            gem.Sprites.Add(new Sprite2D(new List<Texture2D> { images[0] }, 0, 0));
            _prototypes.Add((int)GemName.GemGreen, gem);

            gem = new Gem(new GemRed());
            gem.Sprites.Add(new Sprite2D(new List<Texture2D> { images[1] }, 0, 0));
            _prototypes.Add((int)GemName.GemRed, gem);

            gem = new Gem(new GemYellow());
            gem.Sprites.Add(new Sprite2D(new List<Texture2D> { images[2] }, 0, 0));
            _prototypes.Add((int)GemName.GemYellow, gem);

            gem = new Gem(new GemBlue());
            gem.Sprites.Add(new Sprite2D(new List<Texture2D> { images[3] }, 0, 0));
            _prototypes.Add((int)GemName.GemBlue, gem);

            gem = new Gem(new GemWhite());
            gem.Sprites.Add(new Sprite2D(new List<Texture2D> { images[4] }, 0, 0));
            _prototypes.Add((int)GemName.GemWhite, gem);

            gem = new Gem(new GemPurple());
            gem.Sprites.Add(new Sprite2D(new List<Texture2D> { images[5] }, 0, 0));
            _prototypes.Add((int)GemName.GemPurple, gem);

            gem = new Gem(new GemGold());
            gem.Sprites.Add(new Sprite2D(new List<Texture2D> { images[6] }, 0, 0));
            _prototypes.Add((int)GemName.GemGold, gem);
        }

        public override object CreateObject(int idx)
        {
            return (_prototypes[idx] as Gem).Clone();
        }
    }
}
