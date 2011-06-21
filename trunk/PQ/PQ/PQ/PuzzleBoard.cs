using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PQ
{
    public class PuzzleBoard : GameDialog
    {
        SplittingDetails _details;

        //int _selGemIdx = -1;
        Gem _downGem;
        Gem _upGem;

        GemManager _gemManager;

        Random _rand = new Random();

        List<Gem> _gems = new List<Gem>();

        public PuzzleBoard(SplittingDetails details, GemManager gemManager)
        {
            _gemManager = gemManager;

            _details = details;
        }

        public void Reset()
        {
            this.UnmanageObjects(_gameObjects.ToArray());
            _gameObjects.Clear();
            _gems.Clear();

            for (int c = 0; c < _details.ColumnCount; ++c)
            {
                for (int r = 0; r < _details.RowCount; ++r)
                {
                    int gemIdx = _rand.Next(14);
                    Gem gem = _gemManager.CreateObject(gemIdx) as Gem;

                    gem.X = this.X + c * (_details.FrameWidth + _details.SpaceX) + _details.InitMarginX;
                    gem.Y = this.Y + r * (_details.FrameHeight + _details.SpaceY) + _details.InitMarginY;

                    gem.MouseDown += new EventHandler<GameMouseEventArgs>(gem_MouseDown);
                    gem.MouseUp += new EventHandler<GameMouseEventArgs>(gem_MouseUp);

                    _gems.Add(gem);
                    this.ManageObjects(gem);
                    _gameObjects.Add(gem);
                }
            }

            //this.ManageObjects(_gems.ToArray());
            //_gameObjects.AddRange(_gems);
        }

        bool IsNext4(Gem g1, Gem g2)
        {
            if (
                (g1.X + _details.FrameWidth + _details.SpaceX == g2.X && g1.Y == g2.Y)
                || (g1.X - _details.FrameWidth - _details.SpaceX == g2.X && g1.Y == g2.Y)
                || (g1.Y + _details.FrameHeight + _details.SpaceY == g2.Y && g1.X == g2.X)
                || (g1.Y - _details.FrameHeight - _details.SpaceY == g2.Y && g1.X == g2.X)
                )
                return true;

            return false;
        }

        int Coord2Idx(int x, int y)
        {


            return -1;
        }

        void gem_MouseUp(object o, GameMouseEventArgs e)
        {
            _upGem = o as Gem;

            //if (IsNext4(_upGem, _downGem))
            //{

            //}
        }

        void gem_MouseDown(object o, GameMouseEventArgs e)
        {
            _downGem = o as Gem;

        }

        public void GenerateGems()
        {

        }

        public override void CheckCollision()
        {
            for (int c = 0; c < _details.ColumnCount; ++c)
            {
                for (int r = 0; r < _details.RowCount; ++r)
                {
                    //_gems
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _gems.Count; ++i)
            {
                _gems[i].Draw(gameTime, spriteBatch);
            }

            //spriteBatch.Draw(_gems[0].Sprites[0].Frames[0], new Vector2(X, Y), Color.White);
        }
    }
}
