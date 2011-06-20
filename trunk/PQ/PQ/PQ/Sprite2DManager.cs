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
        LongMainMenuButtonUp
    }

    public class Sprite2DManager
    {
        Dictionary<Sprite2DName, Sprite2D> _sprites = new Dictionary<Sprite2DName, Sprite2D>();

        public void InitPrototypes(ContentManager content)
        {
            _sprites.Add(Sprite2DName.LongMainMenuButtonUp, 
                new Sprite2D(new Texture2D[]{content.Load<Texture2D>(@"Images\Buttons\Long_Button_Main_Up.png")},0,0));
        }

        public Sprite2D CreateObject(Sprite2DName name)
        {
            return null;
        }
    }
}
