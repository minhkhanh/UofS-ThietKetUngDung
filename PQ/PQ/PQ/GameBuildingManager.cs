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
        Building1,
        Building2,
        Building3,
        Building4,
        Building5,
        Building6
    }

    public class GameBuildingManager : AbstractManager
    {
        public override void LoadPrototypes(ContentManager content)
        {
            _prototypes = new Dictionary<int, object>();
            SplittingDetails details = new SplittingDetails(1, 4, 0, 0, 100, 100, 0, 0, 100, 0);
            GameEntity building = new GameBuilding();
            Texture2D tmpTexture = content.Load<Texture2D>(@"Maps\Sites");
            building.Sprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            building.UpdateChild();
            _prototypes.Add((int)BuildingName.Building1, building);

            details = new SplittingDetails(1, 4, 0, 0, 64, 64, 0, 0, 128, 100);
            building = new GameBuilding();
            tmpTexture = content.Load<Texture2D>(@"Maps\Sites");
            building.Sprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            building.UpdateChild();
            _prototypes.Add((int)BuildingName.Building2, building);

            details = new SplittingDetails(1, 4, 0, 0, 64, 64, 0, 0, 128, 164);
            building = new GameBuilding();
            tmpTexture = content.Load<Texture2D>(@"Maps\Sites");
            building.Sprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            building.UpdateChild();
            _prototypes.Add((int)BuildingName.Building3, building);

            details = new SplittingDetails(1, 4, 0, 0, 28, 32, 0, 0, 0, 480);
            building = new GameBuilding();
            tmpTexture = content.Load<Texture2D>(@"Maps\Sites");
            building.Sprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            building.UpdateChild();
            _prototypes.Add((int)BuildingName.Building4, building);

            details = new SplittingDetails(1, 4, 0, 0, 28, 32, 0, 0, 112, 480);
            building = new GameBuilding();
            tmpTexture = content.Load<Texture2D>(@"Maps\Sites");
            building.Sprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            building.UpdateChild();
            _prototypes.Add((int)BuildingName.Building5, building);

            details = new SplittingDetails(1, 4, 0, 0, 28, 32, 0, 0, 224, 480);
            building = new GameBuilding();
            tmpTexture = content.Load<Texture2D>(@"Maps\Sites");
            building.Sprites.Add(new Sprite2D(tmpTexture, 0, 0, details));
            building.UpdateChild();
            _prototypes.Add((int)BuildingName.Building6, building);
        }

        public override object CreateObject(int idx)
        {
            return (_prototypes[idx % _prototypes.Count] as GameBuilding).Clone();
        }
    }
}
