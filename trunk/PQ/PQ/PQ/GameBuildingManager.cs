using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PQ
{
    enum BuildingName
    {
        Building1
    }

    public class GameBuildingManager : GameObjectManager
    {
        public override void LoadPrototypes(ContentManager content)
        {
            //_prototypes = new List<GameObject>();
            _prototypes = new Dictionary<int, object>();
            SplittingDetails details = new SplittingDetails(1, 4, 0, 0, 100, 100, 0, 0, 100, 0);
            GameEntity building = new GameBuilding();
            Texture2D tmpTexture = content.Load<Texture2D>(@"Maps\Sites");
            building.Sprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            _prototypes.Add((int)BuildingName.Building1, building);
        }

        public override object CreateObject(int idx)
        {
            return (_prototypes[idx] as GameBuilding).Clone();
        }
    }
}
