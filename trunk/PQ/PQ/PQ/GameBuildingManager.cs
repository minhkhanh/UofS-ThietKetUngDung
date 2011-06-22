using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class GameBuildingManager : GameObjectManager
    {
        public override void LoadPrototypes(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            _prototypes = new List<GameObject>();
            SplittingDetails details = new SplittingDetails(1, 4, 0, 0, 100, 100, 0, 0, 100, 0);
            GameEntity building = new GameBuilding();
            Texture2D tmpTexture = content.Load<Texture2D>(@"Maps\Sites");
            building.Sprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            _prototypes.Add(building);
        }
    }
}
