using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Data;

namespace PQ
{
    enum MapName
    {
        GlobalMap
    }

    public class MapManager : AbstractManager
    {
        public override object CreateObject(int idx)
        {
            return _prototypes[idx];
        }

        public override void LoadPrototypes(ContentManager content)
        {
            GlobalMap globalMap = new GlobalMap();

            DataSet vdDataSet = new DataSet();
            string database = @"Content\Maps\Map.xml";
            //string schema = "..//..//MapSchema.xsd";
            //vdDataSet.ReadXmlSchema(schema);
            vdDataSet.ReadXml(database);
            DataRow rowMap = vdDataSet.Tables["Map"].Rows[0];
            int width;
            int.TryParse(rowMap["width"].ToString(), out width);
            int height;
            int.TryParse(rowMap["height"].ToString(), out height);
            Texture2D mapPiece;
            SplittingDetails details = new SplittingDetails(1, 1, 0, 0, width, height, 0, 0, 0, 0);
            DataRow[] drs = vdDataSet.Tables["Segment"].Select();
            foreach (DataRow dr in drs)
            {
                //Console.WriteLine(dr["row"].ToString() + "\t" + dr["column"].ToString() + "\t" + dr["texture"].ToString());
                mapPiece = content.Load<Texture2D>(@"Maps\Map" + dr["row"] + dr["column"]);
                int c;
                int.TryParse(dr["column"].ToString(), out c);
                int r;
                int.TryParse(dr["row"].ToString(), out r);
                globalMap.Sprites.Add(new Sprite2D(mapPiece, c * width, r * height, details));
            }


            //Texture2D mapPiece;
            //SplittingDetails details = new SplittingDetails(1, 1, 0, 0, 512, 512, 0, 0, 0, 0);
            //for (int r = 0; r < 4; ++r)
            //    for (int c = 0; c < 4; ++c)
            //    {
            //        mapPiece = content.Load<Texture2D>(@"Images\Maps\Map" + r + c);
            //        globalMap.Sprites.Add(new Sprite2D(mapPiece, c*512, r*512, details));
            //        // "+ c" va "+ r" de thay ro map gom nhieu manh
            //    }
            globalMap.UpdateChild();
            _prototypes.Add((int)MapName.GlobalMap, globalMap);

            //TiledMap tiledMap = new TiledMap();
            //tiledMap.LoadMap(Directory.GetCurrentDirectory() + @"\SampleTiledMap.txt");
            //TerrainManager terrainManager = new TerrainManager();
            //terrainManager.InitPrototypes(content);
            //terrainManager.TerrainWidth = 101;
            //terrainManager.TerrainHeight = 80;
            //tiledMap.TerrainManager = terrainManager;

            //_prototypes.Add(tiledMap);

        }
    }
}
