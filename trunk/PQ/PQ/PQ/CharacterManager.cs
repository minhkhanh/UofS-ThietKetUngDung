using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class CharacterManager : EntityManager
    {
        public override void InitPrototypes(ContentManager content)
        {
            _prototypes = new List<GameEntity>();

            ImageSplittingDetails details = new ImageSplittingDetails();

            details.RowCount = 4;
            details.ColumnCount = 8;
            details.RowIndex = details.ColumnIndex = 0;
            details.FrameWidth = details.FrameHeight = 49;

            GameEntity entity = new Character();
            Texture2D tmpTexture = content.Load<Texture2D>(@"Images\MaleHeroKnight1_49x49");
            entity.MainSprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            _prototypes.Add(entity);

            tmpTexture = content.Load<Texture2D>(@"Images\MaleHeroKnight2_49x49");
            entity = new Character();
            entity.MainSprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            _prototypes.Add(entity);
        }
    }
}
