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
        public override void LoadPrototypes(ContentManager content)
        {
            _prototypes = new List<GameObject>();

            SplittingDetails details = new SplittingDetails(1, 1, 5, 0, 300, 50, 0, 1, 0, 0);
            //details.SpaceX = 0;
            //details.SpaceY = 1;
            //details.ColumnCount = 1;
            //details.RowCount = 1;
            //details.FrameWidth = 300;
            //details.FrameHeight = 50;
            //details.RowIndex = 5;
            //details.ColumnIndex = 0;

            Texture2D txtureBtn = content.Load<Texture2D>(@"Images\Skin_Buttons_Main");
            Sprite2D downSprite = new Sprite2D(txtureBtn, 0, 0, details);

            details.ColumnIndex = 0;
            details.RowIndex = 0;
            Sprite2D upSprite = new Sprite2D(txtureBtn, 0, 0, details);

            details.RowCount = 4;
            details.RowIndex = 1;
            Sprite2D hoverSprite = new Sprite2D(txtureBtn, 0, 0, details);

            GameButton tmpBtn = new GameButton( 
                new Sprite2D[0],
                new Sprite2D[] { downSprite },
                new Sprite2D[] { upSprite },
                new Sprite2D[] { hoverSprite }
                );

            _prototypes.Add(tmpBtn);
        }
    }
}
