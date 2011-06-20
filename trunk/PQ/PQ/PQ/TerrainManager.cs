using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace PQ
{
    public class TerrainManager: Texture2DManager
    {
        int _terrainWidth = 0;
        public int TerrainWidth
        {
            get { return _terrainWidth; }
            set { _terrainWidth = value; }
        }

        int _terrainHeight = 0;
        public int TerrainHeight
        {
            get { return _terrainHeight; }
            set { _terrainHeight = value; }
        }

        public override void InitPrototypes(ContentManager content)
        {
            string[] files = Directory.GetFiles(content.RootDirectory + @"\Images\Terrains\"); ;
            foreach (string i in files)
            {
                string[] parts = i.Split('\\');
                parts = parts[parts.Length - 1].Split('.');
                string filename = @"Images\Terrains\" + parts[0];
                _textures.Add(content.Load<Texture2D>(filename));                
            }
        }

        public override Texture2D CreateObject(int idx)
        {
            if (idx < 0 || idx >= _textures.Count)
                return null;

            return _textures[idx];
        }
    }
}
