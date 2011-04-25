using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace PQ
{
    public class TiledMap: Map
    {
        int[][] _tileMatrix;
        TerrainManager _terrainManager;

        public TerrainManager TerrainManager
        {
            get { return _terrainManager; }
            set { _terrainManager = value; }
        }

        public override Rectangle Bounds
        {
            get
            {
                if (_tileMatrix == null)
                    return Rectangle.Empty;

                return new Rectangle((int)X, (int)Y, _tileMatrix[0].Length * _terrainManager.TerrainWidth, _tileMatrix.Length * _terrainManager.TerrainHeight);
            }
        }

        public void LoadMap(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            string stream = reader.ReadToEnd();
            reader.Close();

            string[] lines = stream.Split(new char[]{'\r','\n'}, StringSplitOptions.RemoveEmptyEntries);
            _tileMatrix = new int[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
            {
                string[] terrainName = lines[i].Split(' ');
                _tileMatrix[i] = new int[terrainName.Length];
                for (int j = 0; j < terrainName.Length; ++j)
                    _tileMatrix[i][j] = int.Parse(terrainName[j]);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _tileMatrix.Length; ++i)
                for (int j = 0; j < _tileMatrix[i].Length; ++j)
                {
                    Rectangle rect = new Rectangle((int)X + j * _terrainManager.TerrainWidth, (int)Y + i * _terrainManager.TerrainHeight, _terrainManager.TerrainWidth, _terrainManager.TerrainHeight);
                    if (rect.Intersects(spriteBatch.GraphicsDevice.PresentationParameters.Bounds))
                    {
                        spriteBatch.Draw(_terrainManager.CreateObject(_tileMatrix[i][j]), rect, Color.White);
                    }
                }
        }

        public override GameObject Clone()
        {
            TiledMap map = new TiledMap();
            
            map._tileMatrix = new int[_tileMatrix.Length][];
            for (int i = 0; i < _tileMatrix.Length; ++i)
            {
                map._tileMatrix[i] = new int[_tileMatrix[i].Length];
                for (int j = 0; j < _tileMatrix[i].Length; ++j)
                    map._tileMatrix[i][j] = _tileMatrix[i][j];
            }

            for (int i = 0; i < _sprites.Count; ++i)
                map._sprites.Add(new Sprite2D(_sprites[i]));

            map._terrainManager = _terrainManager;

            return map;
        }
    }
}
