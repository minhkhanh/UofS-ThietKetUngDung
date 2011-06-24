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
        Gem _gem1 = null;
        Gem _gem2 = null;

        GemManager _gemManager;

        Random _rand = new Random();
        List<Gem> _gems = new List<Gem>();

        SelectedGemEffect _selGemEffect;

        public PuzzleBoard(SplittingDetails details, GemManager gemManager, Sprite2DManager spriteManager)
        {
            _gemManager = gemManager;
            _details = details;

            _selGemEffect = spriteManager.CreateObject((int)Sprite2DName.SelectedGem) as SelectedGemEffect;
        }

        public void Reset()
        {
            this.UnmanageObjects(_gameObjects.ToArray());
            _gameObjects.Clear();
            _gems.Clear();

            for (int r = 0; r < _details.RowCount; ++r)
            {
                for (int c = 0; c < _details.ColumnCount; ++c)
                {
                    int gemIdx = _rand.Next(14);
                    Gem gem = _gemManager.CreateObject(gemIdx) as Gem;

                    gem.X = this.X + c * (_details.FrameWidth + _details.SpaceX) + _details.InitMarginX;
                    gem.Y = this.Y + r * (_details.FrameHeight + _details.SpaceY) + _details.InitMarginY;

                    gem.MouseDown += new EventHandler<GameMouseEventArgs>(gem_MouseDown);

                    _gems.Add(gem);
                    this.ManageObjects(gem);
                    _gameObjects.Add(gem);
                }
            }
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

        int GetGemIdx(Gem gem)
        {
            Point coord = GetGemCoord(gem);

            return coord.X * _details.ColumnCount + coord.Y;
        }

        Point GetGemCoord(Gem gem)
        {
            int rowIdx = (int)(gem.Y - this.Y - _details.InitMarginY) / (_details.FrameHeight + _details.SpaceY);
            int colIdx = (int)(gem.X - this.X - _details.InitMarginX) / (_details.FrameWidth + _details.SpaceX);

            return new Point(rowIdx, colIdx);
        }

        void gem_MouseDown(object o, GameMouseEventArgs e)
        {
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

                    int idx1 = GetGemIdx(_gem1);
                    int idx2 = GetGemIdx(_gem2);

                    Gem tmp = _gems[idx1];
                    _gems[idx1] = _gems[idx2];
                    _gems[idx2] = tmp;
                }

                _gem1 = null;        // must be done at last
            }

        }

        public void GenerateGems()
        {

        }

        public override void CheckCollision()
        {
            List<Gem> movingGems = _gems.FindAll(u => u.MotionModule.IsMoving);

            foreach (Gem i in movingGems)
            {
                int rowIdx = (int)(i.Y - this.Y - _details.InitMarginY) / (_details.FrameHeight + _details.SpaceY);
                int colIdx = (int)(i.X - this.X - _details.InitMarginX) / (_details.FrameWidth + _details.SpaceX);

                switch (i.MotionModule.MovingDirection)
                {
                    case Direction.Downward:
                        for (int d = rowIdx*_details.ColumnCount + colIdx; d < _details.RowCount * _details.ColumnCount; d += _details.ColumnCount)
                        {
                            // check collision with stable gems
                            if (!_gems[d].MotionModule.IsMoving && i.IsCollided(_gems[d]))
                            {
                                // stop moving when collided
                                i.MotionModule.Stop();
                                break;
                            }
                        }
                        // if gem continues to move, stop it when out of table bound
                        if (i.MotionModule.IsMoving && i.Y + i.Height > this.Y + _details.InitMarginY + (_details.FrameHeight + _details.SpaceY)*_details.RowCount)
                            i.MotionModule.Stop();

                        break;

                    case Direction.Upward:
                        for (int d = rowIdx * _details.ColumnCount + colIdx; d >= 0; d -= _details.ColumnCount)
                        {
                            if (!_gems[d].MotionModule.IsMoving && i.IsCollided(_gems[d]))
                            {
                                i.MotionModule.Stop();
                                ++rowIdx;
                                break;
                            }
                        }
                        if (i.MotionModule.IsMoving && i.Y < this.Y)
                        {
                            i.MotionModule.Stop();
                        }

                        break;

                    case Direction.Rightward:
                        for (int d = rowIdx * _details.ColumnCount + colIdx; d < (rowIdx+1)*_details.ColumnCount; ++d)
                        {
                            if (!_gems[d].MotionModule.IsMoving && i.IsCollided(_gems[d]))
                            {
                                i.MotionModule.Stop();
                                break;
                            }
                        }
                        if (i.MotionModule.IsMoving && i.X + i.Width > this.X + _details.InitMarginX + (_details.FrameWidth + _details.SpaceX) * _details.ColumnCount)
                        {
                            i.MotionModule.Stop();
                        }                        

                        break;

                    case Direction.Leftward:
                        for (int d = rowIdx * _details.ColumnCount + colIdx; d >= rowIdx * _details.ColumnCount; --d)
                        {
                            if (!_gems[d].MotionModule.IsMoving && i.IsCollided(_gems[d]))
                            {
                                i.MotionModule.Stop();
                                ++colIdx;
                                break;
                            }
                        }
                        if (i.MotionModule.IsMoving && i.X < this.X)
                            i.MotionModule.Stop();

                        break;
                }

                // if gem stopped, snap it to nearest cell by its move direction
                if (!i.MotionModule.IsMoving)
                {
                    i.X = this.X + colIdx * (_details.FrameWidth + _details.SpaceX) + _details.InitMarginX;
                    i.Y = this.Y + rowIdx * (_details.FrameHeight + _details.SpaceY) + _details.InitMarginY;
                }
            }
        }

        void CheckMove(Gem gem)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_gem1 != null)      // one gem is selected, no gems are moving
            {
                _selGemEffect.Rotation -= 0.02f;
                if (_selGemEffect.Rotation < 0)
                    _selGemEffect.Rotation += 360;
            }
            else
                CheckCollision();


        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (_gem1 != null)
            {
                _selGemEffect.Draw(gameTime, spriteBatch);
            }
        }

    }
}
