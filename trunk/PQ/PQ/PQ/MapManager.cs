using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace PQ
{
    public class MapManager : GameObjectManager
    {
        public override void LoadPrototypes(ContentManager content)
        {
            GlobalMap globalMap = new GlobalMap();

            Texture2D mapPiece;
            ImageSplittingDetails details = new ImageSplittingDetails(1, 1, 0, 0, 512, 512, 0, 0, 0, 0);
            //details.ColumnCount = details.RowCount = 1;
            //details.FrameWidth = details.FrameHeight = 512;

            for (int r = 0; r < 4; ++r)
                for (int c = 0; c < 4; ++c)
                {
                    mapPiece = content.Load<Texture2D>(@"Images\Maps\Map" + r + c);
                    globalMap.Sprites.Add(new Sprite2D(mapPiece, c*512, r*512, details));
                    // "+ c" va "+ r" de thay ro map gom nhieu manh
                }

            _prototypes.Add(globalMap);

            TiledMap tiledMap = new TiledMap();
            tiledMap.LoadMap(Directory.GetCurrentDirectory() + @"\SampleTiledMap.txt");
            TerrainManager terrainManager = new TerrainManager();
            terrainManager.InitPrototypes(content);
            terrainManager.TerrainWidth = 101;
            terrainManager.TerrainHeight = 80;
            tiledMap.TerrainManager = terrainManager;

            _prototypes.Add(tiledMap);

        }
    }
}
