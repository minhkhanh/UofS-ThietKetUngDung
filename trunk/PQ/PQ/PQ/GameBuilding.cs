using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class GameBuilding : GameEntity
    {
        public override GameObject Clone()
        {
            GameBuilding building = new GameBuilding();

            for (int i = 0; i < _sprites.Count; ++i)
                building._sprites.Add(new Sprite2D(_sprites[i]));

            building.X = this.X;
            building.Y = this.Y;

            return building;
        }
    }
}
