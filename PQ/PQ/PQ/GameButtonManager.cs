using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class GameButtonManager : GameObjectManager
    {
        public override void InitPrototypes(ContentManager content)
        {
            _prototypes = new List<GameObject>();

            GameButton tmpBtn = new GameButton();
            ImageSplittingDetails details = new ImageSplittingDetails(0, 0);

            details.SpaceX = 0;
            details.SpaceY = 1;
            details.ColumnCount = 1;
            details.RowCount = 1;
            details.FrameWidth = 300;
            details.FrameHeight = 50;

            Texture2D txtureBtn = content.Load<Texture2D>(@"Images\pqpc_buttons");
            tmpBtn.DownSprites.Add(new Sprite2D(txtureBtn, 0, 0, details));

            details.ColumnIndex = 0;
            details.RowIndex = 6;
            tmpBtn.UpSprites.Add(new Sprite2D(txtureBtn, 0, 0, details));
            tmpBtn.Sprites = tmpBtn.UpSprites;

            details.RowCount = 4;
            details.RowIndex = 1;
            tmpBtn.HoverSprites.Add(new Sprite2D(txtureBtn, 0, 0, details));

            //tmpBtn.p

            _prototypes.Add(tmpBtn);
        }
    }
}
