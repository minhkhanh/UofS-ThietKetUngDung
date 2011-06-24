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

            building.UpdateChild();
            building.LogicalX = LogicalX;
            building.LogicalY = LogicalY;
            building.GameObjectParent = GameObjectParent;

            return building;
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
