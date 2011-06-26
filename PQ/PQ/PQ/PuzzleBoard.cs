﻿using System;
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
        Gem _gem1 = null;
        Gem _gem2 = null;

        //GemManager _gemManager;

        Random _rand = new Random();
        Gem[,] _gems;
        int[,] _chains;

        GemSelectedEffect _selGemEffect;
        GemWrongSelectedEffect _wrongGemEffect;

        ParticleEngine _particles = new ParticleEngine();

        public PuzzleBoard(SplittingDetails details, Sprite2DManager spriteManager)
        {
            _details = details;

            _selGemEffect = spriteManager.CreateObject((int)Sprite2DName.GemSelectedEffect) as GemSelectedEffect;
            _wrongGemEffect = spriteManager.CreateObject((int)Sprite2DName.GemWrongSelectedEffect) as GemWrongSelectedEffect;

            _chains = new int[_details.RowCount + 1, _details.ColumnCount];
            _gems = new Gem[_details.RowCount, _details.ColumnCount];
            for (int r = 0; r < _details.RowCount; ++r)
                for (int c = 0; c < _details.ColumnCount; ++c)
                {
                    _gems[r, c] = new Gem();
                    _chains[r, c] = 1;
                }
        }

        Vector2 Coord2Pos(int r, int c)
        {
            return new Vector2(
                this.X + c * (_details.FrameWidth + _details.SpaceX) + _details.InitMarginX,
                this.Y + r * (_details.FrameHeight + _details.SpaceY) + _details.InitMarginY
                );
        }

        public Gem GenerateGem()
        {
            int gemIdx = _rand.Next(7);
            Gem gem = Parent.Game.GemManager.CreateObject(gemIdx) as Gem;

            gem.MouseDown += new EventHandler<GameMouseEventArgs>(gem_MouseDown);

            this.ManageObjects(gem);
            _gameObjects.Add(gem);

            return gem;
        }

        void DropGems()
        {
            for (int c = 0; c < _details.ColumnCount; ++c)
            {
                int count1 = _chains[_details.RowCount, c];
                for (int r = 0; r < _details.RowCount; ++r)
                {
                    if (_chains[r,c] == 1)
                    {
                        UnmanageObjects(_gems[r, c]);
                        _gameObjects.Remove(_gems[r, c]);

                        _gems[r, c] = GenerateGem();
                        _gems[r, c].Position = new Vector2(Coord2Pos(r, c).X, this.Y + (r - count1) * _gems[r, c].Height);
                        _gems[r, c].MotionModule.Velocity = new Vector2(0, 5);
                    }
                }
            }
        }

        public void Reset()
        {
            this.UnmanageObjects(_gameObjects.ToArray());
            _gameObjects.Clear();
            _gems = new Gem[_details.RowCount, _details.ColumnCount];
            _chains = new int[_details.RowCount+1, _details.ColumnCount];
            for (int r = 0; r < _details.RowCount; ++r)
                for (int c = 0; c < _details.ColumnCount; ++c)
                {
                    _gems[r, c] = new Gem();
                    _chains[r, c] = 1;
                }

            ResolveChains();
            DropGems();
        }

        /// <summary>
        /// Return relative position of gem 1 to gem 2
        /// </summary>
        /// <param name="g1">gem 1</param>
        /// <param name="g2">gem 2</param>
        /// <returns></returns>
        Direction IsNext4(Gem g1, Gem g2)
        {
            if (g1.X + _details.FrameWidth + _details.SpaceX == g2.X && g1.Y == g2.Y)
                return Direction.Left;

            if (g1.X - _details.FrameWidth - _details.SpaceX == g2.X && g1.Y == g2.Y)
                return Direction.Right;

            if (g1.Y + _details.FrameHeight + _details.SpaceY == g2.Y && g1.X == g2.X)
                return Direction.Above;

            if (g1.Y - _details.FrameHeight - _details.SpaceY == g2.Y && g1.X == g2.X)
                return Direction.Below;

            return Direction.None;
        }

        Point GetGemCoord(Gem gem)
        {
            int rowIdx = (int)(gem.Y - this.Y - _details.InitMarginY) / (_details.FrameHeight + _details.SpaceY);
            int colIdx = (int)(gem.X - this.X - _details.InitMarginX) / (_details.FrameWidth + _details.SpaceX);

            return new Point(rowIdx, colIdx);
        }

        void gem_MouseDown(object o, GameMouseEventArgs e)
        {
            if (!IsAllGemsStopped)
                return;

            if (_gem1 == null)
            {
                _gem1 = o as Gem;
                _selGemEffect.X = _gem1.X - Math.Abs((_selGemEffect.Width - _gem1.Width) / 2f);
                _selGemEffect.Y = _gem1.Y - Math.Abs((_selGemEffect.Height - _gem1.Height) / 2f);
            }
            else
            {
                _gem2 = o as Gem;
                
                Direction direct = IsNext4(_gem1, _gem2);
                if (direct != Direction.None)
                {
                    Point coord1 = GetGemCoord(_gem1);
                    Point coord2 = GetGemCoord(_gem2);

                    Gem tmp = _gems[coord1.X, coord1.Y];
                    _gems[coord1.X, coord1.Y] = _gems[coord2.X, coord2.Y];
                    _gems[coord2.X, coord2.Y] = tmp;

                    List<int> gemInARow1 = CheckRow(coord1.X, coord1.Y);
                    if (gemInARow1.Count < 2)
                    {
                        List<int> gemInACol1 = CheckCol(coord1.X, coord1.Y);
                        if (gemInACol1.Count < 2)
                        {
                            List<int> gemInARow2 = CheckRow(coord2.X, coord2.Y);
                            if (gemInARow2.Count < 2)
                            {
                                List<int> gemInACol2 = CheckCol(coord2.X, coord2.Y);
                                if (gemInACol2.Count < 2)
                                {
                                    tmp = _gems[coord1.X, coord1.Y];
                                    _gems[coord1.X, coord1.Y] = _gems[coord2.X, coord2.Y];
                                    _gems[coord2.X, coord2.Y] = tmp;

                                    _wrongGemEffect.Refresh();
                                    _wrongGemEffect.X = _gem1.X - Math.Abs((_wrongGemEffect.Width - _gem1.Width) / 2f);
                                    _wrongGemEffect.Y = _gem1.Y - Math.Abs((_wrongGemEffect.Height - _gem1.Height) / 2f);

                                    return;
                                }
                            }
                        }
                    }

                    switch (direct)
                    {
                        case Direction.Left:
                            _gem1.MotionModule.Vx = 5;
                            _gem2.MotionModule.Vx = -5;

                            _gem1.MotionModule.Vy = _gem2.MotionModule.Vy = 0;
                            break;
                        case Direction.Right:
                            _gem1.MotionModule.Vx = -5;
                            _gem2.MotionModule.Vx = 5;

                            _gem1.MotionModule.Vy = _gem2.MotionModule.Vy = 0;
                            break;
                        case Direction.Above:
                            _gem1.MotionModule.Vy = 5;
                            _gem2.MotionModule.Vy = -5;

                            _gem1.MotionModule.Vx = _gem2.MotionModule.Vx = 0;
                            break;
                        case Direction.Below:
                            _gem1.MotionModule.Vy = -5;
                            _gem2.MotionModule.Vy = 5;

                            _gem1.MotionModule.Vx = _gem2.MotionModule.Vx = 0;
                            break;
                    }

                    
                }

                _gem1 = null;        // must be done at last
            }

        }

        public void CheckCollision()
        {
            for (int r = 0; r < _details.RowCount; ++r)
                for (int c = 0; c < _details.ColumnCount; ++c)
                {
                    if (_gems[r, c].ColorState.Name != GemName.None && _gems[r, c].MotionModule.IsMoving)
                    {
                        Direction movDirect = _gems[r,c].MotionModule.MovingDirection;
                        switch (movDirect)
                        {
                            case Direction.Downward:
                                for (int d = r + 1; d < _details.RowCount; ++d)
                                {
                                    if (!_gems[d, c].IsThrough && _gems[r, c].IsCollided(_gems[d, c]))
                                    {
                                        // stop moving when collided
                                        _gems[r, c].MotionModule.Stop();
                                        break;
                                    }
                                }
                                // if gem continues to move, stop it when out of table bound
                                if (_gems[r,c].MotionModule.IsMoving && 
                                    _gems[r,c].Y + _gems[r,c].Height > this.Y + _details.InitMarginY + (_details.FrameHeight + _details.SpaceY) * _details.RowCount)
                                    _gems[r,c].MotionModule.Stop();

                                break;

                            case Direction.Upward:
                                for (int d = r - 1; d >= 0; --d)
                                {
                                    if (!_gems[d,c].IsThrough && _gems[r,c].IsCollided(_gems[d,c]))
                                    {
                                        _gems[r,c].MotionModule.Stop();
                                        break;
                                    }
                                }
                                if (_gems[r,c].MotionModule.IsMoving && _gems[r,c].Y < this.Y)
                                {
                                    _gems[r,c].MotionModule.Stop();
                                }

                                break;

                            case Direction.Rightward:
                                for (int d = c + 1; d < _details.ColumnCount; ++d)
                                {
                                    if (!_gems[r, d].IsThrough && _gems[r, c].IsCollided(_gems[r, d]))
                                    {
                                        _gems[r, c].MotionModule.Stop();
                                        break;
                                    }
                                }
                                if (_gems[r,c].MotionModule.IsMoving && 
                                    _gems[r,c].X + _gems[r,c].Width > this.X + _details.InitMarginX + (_details.FrameWidth + _details.SpaceX) * _details.ColumnCount)
                                {
                                    _gems[r,c].MotionModule.Stop();
                                }

                                break;

                            case Direction.Leftward:
                                for (int d = c - 1; d >= 0; --d)
                                {
                                    if (!_gems[r,d].IsThrough && _gems[r,c].IsCollided(_gems[r,d]))
                                    {
                                        _gems[r,c].MotionModule.Stop();
                                        break;
                                    }
                                }
                                if (_gems[r,c].MotionModule.IsMoving && _gems[r,c].X < this.X)
                                    _gems[r,c].MotionModule.Stop();

                                break;
                        }

                        // if gem stopped, snap it to nearest cell by its move direction
                        if (!_gems[r,c].MotionModule.IsMoving)
                        {
                            _gems[r, c].Position = Coord2Pos(r, c);
                        }
                    }
                }
        }

        GameStateMiniGame StateMiniGame
        {
            get { return Parent as GameStateMiniGame; }
        }

        List<int> CheckCol(int r, int c)
        {
            List<int> gemInACol = new List<int>();
            //int count = 0;
            // above
            for (int d = r - 1; d >= 0; --d)
            {
                if (_gems[r,c].IsSameColor(_gems[d,c]))
                {
                    gemInACol.Add(d);
                    //_chains[d, c] = 1;
                    //++count;
                }
                else
                    break;
            }

            // below
            for (int d = r + 1; d < _details.RowCount; ++d)
            {
                if (_gems[r,c].IsSameColor(_gems[d,c]))
                {
                    gemInACol.Add(d);
                    //_chains[d, c] = 1;
                    //++count;
                }
                else
                    break;
            }

            //return gemInACol;
            //if (gemInACol.Count >= 2)
            //{
            //    _chains[r, c] = 1;
            //    _particles.Generators.Add(new GemExplosion(_gems[r, c]));
            //    foreach (int i in gemInACol)
            //    {
            //        _chains[i, c] = 1;
            //        _particles.Generators.Add(new GemExplosion(_gems[i, c]));
            //    }
            //}

            return gemInACol;
        }

        List<int> CheckRow(int r, int c)
        {
            List<int> gemInARow = new List<int>();
            //int count = 0;

            // left side
            for (int d = c - 1; d >= 0; --d)
            {
                if (_gems[r,c].IsSameColor(_gems[r,d]))
                {
                    gemInARow.Add(d);
                    //_chains[r, d] = 1;
                    //++count;
                }
                else
                    break;
            }

            // right side
            for (int d = c + 1; d < _details.ColumnCount; ++d)
            {
                if (_gems[r, c].IsSameColor(_gems[r, d]))
                {
                    gemInARow.Add(d);
                    //_chains[r, d] = 1;
                    //++count;
                }
                else
                    break;
            }

            return gemInARow;
        }

        public bool IsAllGemsStopped
        {
            get
            {
                for (int r = 0; r < _details.RowCount; ++r)
                    for (int c = 0; c < _details.ColumnCount; ++c)
                        if (_gems[r, c].MotionModule.IsMoving && _gems[r, c].ColorState.Name != GemName.None)
                            return false;

                return true;
            }
        }

        void CheckChains()
        {
            //bool flag = false;
            _chains = new int[_details.RowCount + 1, _details.ColumnCount];
            for (int r = 0; r < _details.RowCount; ++r)
                for (int c = 0; c < _details.ColumnCount; ++c)
                {
                    _chains[r, c] = 0;
                }

            for (int r = 0; r < _details.RowCount; ++r)
            {
                for (int c = 0; c < _details.ColumnCount; ++c)
                {
                    List<int> gemInARow = CheckRow(r, c);
                    if (gemInARow.Count >= 2)
                    {
                        _chains[r, c] = 1;
                        _particles.Generators.Add(new GemExplosion(_gems[r, c]));
                        foreach (int i in gemInARow)
                        {
                            _chains[r, i] = 1;
                            _particles.Generators.Add(new GemExplosion(_gems[r, i]));
                        }
                    }

                    List<int> gemInACol = CheckCol(r, c);
                    if (gemInACol.Count >= 2)
                    {
                        _chains[r, c] = 1;
                        _particles.Generators.Add(new GemExplosion(_gems[r, c]));
                        foreach (int i in gemInACol)
                        {
                            _chains[i, c] = 1;
                            _particles.Generators.Add(new GemExplosion(_gems[i, c]));
                        }
                    }
                }
            }
        }

        void ResolveChains()
        {
            for (int c = 0; c < _details.ColumnCount; ++c)
            {
                int count1 = 0;
                int r = _details.RowCount - 1;
                for ( ; r >= 0 ; --r)
                {
                    if (_chains[r, c] == 1)
                    {
                        ++count1;
                        for (int d = r - 1; d >=0; --d)
                            if (_chains[d, c] == 0)
                            {
                                int tmp = _chains[d, c];
                                _chains[d, c] = _chains[r, c];
                                _chains[r, c] = tmp;

                                _gems[d, c].MotionModule.Velocity = new Vector2(0, 5);

                                Gem gem = _gems[d, c];
                                _gems[d, c] = _gems[r, c];
                                _gems[r, c] = gem;

                                break;
                            }
                    }
                }

                _chains[_details.RowCount, c] = count1;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (IsAllGemsStopped)
            {
                CheckChains();

                ResolveChains();

                DropGems();
            }

            foreach (Gem i in _gems)
            {
                i.Update(gameTime);
            }            

            if (_gem1 != null)      // one gem is selected, no gems are moving
            {
                _selGemEffect.Update(gameTime);
            }
            else
                CheckCollision();

            _wrongGemEffect.Update(gameTime);

            _particles.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Gem i in _gems)
            {
                i.Draw(gameTime, spriteBatch);
            }
            
            if (_gem1 != null)
            {
                _selGemEffect.Draw(gameTime, spriteBatch);
            }

            _wrongGemEffect.Draw(gameTime, spriteBatch);

            _particles.Draw(gameTime, spriteBatch);
        }
    }
}
