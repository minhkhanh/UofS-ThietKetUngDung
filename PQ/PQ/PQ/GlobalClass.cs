using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PQ
{
    public static class GlobalClass
    {
        public static List<Texture2D> SplitImage(Texture2D largeTxture, SplittingDetails details)
        {
            List<Texture2D> frames = new List<Texture2D>();

            //_frameWidth = details.FrameWidth;
            //_frameHeight = details.FrameHeight;

            float limHeight = Math.Min(largeTxture.Height, details.InitMarginY + (details.RowCount + details.RowIndex) * (details.FrameHeight + details.SpaceY));
            float limWidth = Math.Min(largeTxture.Width, details.InitMarginX + (details.ColumnCount + details.ColumnIndex) * (details.FrameWidth + details.SpaceX));

            float initI = 0, initJ = 0, incrI = 0, incrJ = 0, limI = 0, limJ = 0;

            // khoi tao thong so ban dau danh cho vong lap for:
            if (details.SplittingDirection == SplittingDirection.Vertically)    // cat anh theo chieu doc
            {
                initI = details.InitMarginX + details.ColumnIndex * (details.FrameWidth + details.SpaceX);
                initJ = details.InitMarginY + details.RowIndex * (details.FrameHeight + details.SpaceY);
                limI = limWidth;
                limJ = limHeight;
                incrI = details.FrameWidth + details.SpaceX;
                incrJ = details.FrameHeight + details.SpaceY;
            }
            else if (details.SplittingDirection == SplittingDirection.Horizontally) // cat anh theo chieu ngang
            {
                initI = details.InitMarginY + details.RowIndex * (details.FrameHeight + details.SpaceY);
                initJ = details.InitMarginX + details.ColumnIndex * (details.FrameWidth + details.SpaceX);
                limI = limHeight;
                limJ = limWidth;
                incrI = details.FrameHeight + details.SpaceY;
                incrJ = details.FrameWidth + details.SpaceX;
            }

            for (float i = initI; i < limI; i += incrI)
                for (float j = initJ; j < limJ; j += incrJ)
                {
                    Color[] singleFrame = new Color[(int)details.FrameWidth * (int)details.FrameHeight];
                    largeTxture.GetData<Color>(0, new Rectangle((int)i, (int)j, (int)details.FrameWidth, (int)details.FrameHeight), singleFrame, 0, (int)details.FrameWidth * (int)details.FrameHeight);

                    Texture2D newTxture = new Texture2D(largeTxture.GraphicsDevice, (int)details.FrameWidth, (int)details.FrameHeight);
                    newTxture.SetData<Color>(singleFrame);
                    frames.Add(newTxture);
                }

            return frames;
        }

        public static float SCALE = 1;  // must equals to 1 currently
    }
}
