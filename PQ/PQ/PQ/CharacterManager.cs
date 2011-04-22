using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class CharacterManager : GameObjectManager
    {
        public override void InitPrototypes(ContentManager content)
        {
            _prototypes = new List<GameObject>();

            ImageSplittingDetails details = new ImageSplittingDetails(0, 0);

            details.RowCount = 4;
            details.ColumnCount = 8;
            details.FrameWidth = details.FrameHeight = 49;

            GameEntity character = new Character();
            Texture2D tmpTexture = content.Load<Texture2D>(@"Images\MaleHeroKnight1_49x49");
            character.Sprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            _prototypes.Add(character);

            tmpTexture = content.Load<Texture2D>(@"Images\MaleHeroKnight2_49x49");
            character = new Character();
            character.Sprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            _prototypes.Add(character);
        }
    }
}
