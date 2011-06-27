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

        //GemManager _gemManager;

        Random _rand = new Random();
        Gem[,] _gems;
        int[,] _chains;
        List<Point[]> _hints = new List<Point[]>();

        GemSelectedEffect _selGemEffect;
        GemWrongSelectedEffect _destGemEffect;

        GemWrongSelectedEffect _wrongGemEffect;        

        ParticleEngine _particles = new ParticleEngine();

        bool _playTurn = true;

        Character _computer;

        Character HeroInTurn
        {
            get { return !_playTurn ? Parent.Game.Hero: _computer;}
        }

        Character HeroNext
        {
            get { return _playTurn ? Parent.Game.Hero : _computer; }
        }

        float _originManaBarY;

        Sprite2D _barGHero;
        Sprite2D _barRHero;
        Sprite2D _barYHero;
        Sprite2D _barBHero;
        Sprite2D _barHpHero;

        Sprite2D _barGComp;
        Sprite2D _barRComp;
        Sprite2D _barYComp;
        Sprite2D _barBComp;
        Sprite2D _barHpComp;

        GemWrongSelectedEffect[] _endSigns = new GemWrongSelectedEffect[4];

        public PuzzleBoard(Character computer, SplittingDetails details, Sprite2DManager spriteManager)
        {
            _computer = computer;

            _details = details;

            _selGemEffect = spriteManager.CreateObject((int)Sprite2DName.GemSelectedEffect) as GemSelectedEffect;
            _wrongGemEffect = spriteManager.CreateObject((int)Sprite2DName.GemWrongSelectedEffect) as GemWrongSelectedEffect;
            _destGemEffect = spriteManager.CreateObject((int)Sprite2DName.GemWrongSelectedEffect) as GemWrongSelectedEffect;

            _chains = new int[_details.RowCount + 1, _details.ColumnCount];
            _gems = new Gem[_details.RowCount, _details.ColumnCount];
            for (int r = 0; r < _details.RowCount; ++r)
                for (int c = 0; c < _details.ColumnCount; ++c)
                {
                    _gems[r, c] = new Gem();
                    _chains[r, c] = 1;
                }
        }

        public void StartGame()
        {
            Parent.Game.Hero.CreateMiniStats();
            _computer.CreateMiniStats();

            _barGHero = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.ManaBarGreen) as Sprite2D;
            _barGHero.X = 101;
            _barGHero.Y = 168;
            _barRHero = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.ManaBarRed) as Sprite2D;
            _barRHero.X = 122;
            _barRHero.Y = 168;
            _barYHero = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.ManaBarYellow) as Sprite2D;
            _barYHero.X = 143;
            _barYHero.Y = 168;
            _barBHero = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.ManaBarBlue) as Sprite2D;
            _barBHero.X = 164;
            _barBHero.Y = 168;
            _barHpHero = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.HpBar) as Sprite2D;
            _barHpHero.X = 15;
            _barHpHero.Y = 123;

            _barGComp = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.ManaBarGreen) as Sprite2D;
            _barGComp.X = 924;
            _barGComp.Y = 168;
            _barRComp = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.ManaBarRed) as Sprite2D;
            _barRComp.X = 945;
            _barRComp.Y = 168;
            _barYComp = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.ManaBarYellow) as Sprite2D;
            _barYComp.X = 966;
            _barYComp.Y = 168;
            _barBComp = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.ManaBarBlue) as Sprite2D;
            _barBComp.X = 987;
            _barBComp.Y = 168;
            _barHpComp = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.HpBar) as Sprite2D;
            _barHpComp.X = 838;
            _barHpComp.Y = 123;

            _originManaBarY = 168;
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
                        _gems[r, c].Position = new Vector2(Coord2Pos(r, c).X, this.Y + (r - count1) * (_gems[r, c].Height));
                        _gems[r, c].VerticalMotionModule.FallDown(5, 0.05f);
                    }
                }
            }
        }

        public void Reset()
        {
            this.UnmanageObjects(_gameObjects.ToArray());
            _gameObjects.Clear();
            _gems = new Gem[_details.RowCount, _details.ColumnCount];
            _chains = new int[_details.RowCount + 1, _details.ColumnCount];
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

        void MoveGems()
        {
            Direction direct = IsNext4(_gem1, _gem2);

            Point coord1 = GetGemCoord(_gem1);
            Point coord2 = GetGemCoord(_gem2);

            Gem tmp = _gems[coord1.X, coord1.Y];
            _gems[coord1.X, coord1.Y] = _gems[coord2.X, coord2.Y];
            _gems[coord2.X, coord2.Y] = tmp;

            if (_playTurn)
            {
                if (direct == Direction.None)
                {
                    _gem1 = null;
                    return;
                }

                if (CheckMove(coord1, coord2) == false)
                {
                    tmp = _gems[coord1.X, coord1.Y];
                    _gems[coord1.X, coord1.Y] = _gems[coord2.X, coord2.Y];
                    _gems[coord2.X, coord2.Y] = tmp;

                    _wrongGemEffect.Refresh();
                    _wrongGemEffect.X = _gem1.X - Math.Abs((_wrongGemEffect.Width - _gem1.Width) / 2f);
                    _wrongGemEffect.Y = _gem1.Y - Math.Abs((_wrongGemEffect.Height - _gem1.Height) / 2f);

                    SoundManager.Play("IllegalMove");

                    return;
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

            _destGemEffect.Refresh();
            _destGemEffect.X = _gem2.X - Math.Abs((_destGemEffect.Width - _gem2.Width) / 2f);
            _destGemEffect.Y = _gem2.Y - Math.Abs((_destGemEffect.Height - _gem2.Height) / 2f);

            _playTurn = !_playTurn;

            _gem1 = null;
        }

        bool CheckMove(Point coord1, Point coord2)
        {
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
                            return false;
                    }
                }
            }

            return true;
        }

        void gem_MouseDown(object o, GameMouseEventArgs e)
        {
            if (!IsAllGemsStopped || !_playTurn)
                return;

            if (_gem1 == null)
            {
                _gem1 = o as Gem;

                _selGemEffect.Frames = (Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.GemSelectedEffect) as GemSelectedEffect).Frames;
                _destGemEffect.Frames = (Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.GemSelectedEffect) as GemSelectedEffect).Frames;
                
                _selGemEffect.X = _gem1.X - Math.Abs((_selGemEffect.Width - _gem1.Width) / 2f);
                _selGemEffect.Y = _gem1.Y - Math.Abs((_selGemEffect.Height - _gem1.Height) / 2f);
            }
            else
            {
                _gem2 = o as Gem;
                if (_gem1 == _gem2)
                {
                    _gem1 = null;
                    return;
                }

                MoveGems();
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
                            //SoundManager.PlayWithVolume("StopGem3", 0.3f);
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
            _chains = new int[_details.RowCount + 1, _details.ColumnCount];
            for (int r = 0; r < _details.RowCount; ++r)
                for (int c = 0; c < _details.ColumnCount; ++c)
                {
                    _chains[r, c] = 0;
                }

            _hints.Clear();

            for (int r = 0; r < _details.RowCount; ++r)
            {
                for (int c = 0; c < _details.ColumnCount; ++c)
                {
                    bool flag;
                    List<int> gemInARow = CheckRow(r, c);
                    if (gemInARow.Count >= 2)
                    {
                        flag = true;
                        gemInARow.Add(c);
                        foreach (int i in gemInARow)
                        {
                            if (_chains[r, i] == 0)
                            {
                                _particles.Generators.Add(new GemExplosion(_gems[r, i]));
                                _gems[r,i].ColorState.Consumes(HeroInTurn, HeroNext);

                                _chains[r, i] = 1;
                            }
                            else
                                flag = false;
                        }

                        if (flag)
                            _gems[r, c].ColorState.PlaySound();
                            
                    }
                    else if (gemInARow.Count == 1)
                    {
                        #region fiding hints by rows
                        
                        int next = gemInARow[0];
                        int colMin;
                        int colMax;
                        if (c < next)
                        {
                            colMin = c;
                            colMax = next;
                        }
                        else
                        {
                            colMin = next;
                            colMax = c;
                        }

                        int hintCol = colMin - 1;
                        int hintRow = r - 1;
                        if (hintRow >= 0 && hintCol >= 0)
                        {
                            if (_gems[r, c].IsSameColor(_gems[hintRow, hintCol]))
                            {
                                _hints.Add(new Point[2]{new Point(hintRow, hintCol), new Point(r, hintCol)});
                            }
                        }

                        hintRow = r + 1;
                        if (hintRow < _details.RowCount && hintCol >= 0)
                        {
                            if (_gems[r, c].IsSameColor(_gems[hintRow, hintCol]))
                                _hints.Add(new Point[2] { new Point(hintRow, hintCol), new Point(r, hintCol) });
                        }

                        hintCol = colMax + 1;
                        if (hintRow < _details.RowCount && hintCol < _details.ColumnCount)
                        {
                            if (_gems[r, c].IsSameColor(_gems[hintRow, hintCol]))
                                _hints.Add(new Point[2] { new Point(hintRow, hintCol), new Point(r, hintCol) });
                        }

                        hintRow = r - 1;
                        if (hintRow >= 0 && hintCol < _details.ColumnCount)
                        {
                            if (_gems[r, c].IsSameColor(_gems[hintRow, hintCol]))
                                _hints.Add(new Point[2] { new Point(hintRow, hintCol), new Point(r, hintCol) });
                        }

                        #endregion
                    }

                    List<int> gemInACol = CheckCol(r, c);
                    if (gemInACol.Count >= 2)
                    {
                        gemInACol.Add(r);
                        flag = true;
                        foreach (int i in gemInACol)
                        {
                            if (_chains[i, c] == 0)
                            {
                                _particles.Generators.Add(new GemExplosion(_gems[i, c]));
                                _chains[i, c] = 1;

                                _gems[i, c].ColorState.Consumes(HeroInTurn, HeroNext);
                            }
                            else
                                flag = false;
                        }
                        if (flag)
                            _gems[r, c].ColorState.PlaySound();
                    }
                    else if (gemInACol.Count == 1)
                    {
                        #region fiding hints by columns

                        int next = gemInACol[0];
                        int rowMin;
                        int rowMax;
                        if (r < next)
                        {
                            rowMin = r;
                            rowMax = next;
                        }
                        else
                        {
                            rowMin = next;
                            rowMax = r;
                        }

                        int hintCol = c - 1;
                        int hintRow = rowMin - 1;
                        if (hintRow >= 0 && hintCol >= 0)
                        {
                            if (_gems[r, c].IsSameColor(_gems[hintRow, hintCol]))
                                _hints.Add(new Point[2] { new Point(hintRow, hintCol), new Point(hintRow, c) });
                        }

                        hintRow = rowMax + 1;
                        if (hintRow < _details.RowCount && hintCol >= 0)
                        {
                            if (_gems[r, c].IsSameColor(_gems[hintRow, hintCol]))
                                _hints.Add(new Point[2] { new Point(hintRow, hintCol), new Point(hintRow, c) });
                        }

                        hintCol = c + 1;
                        if (hintRow < _details.RowCount && hintCol < _details.ColumnCount)
                        {
                            if (_gems[r, c].IsSameColor(_gems[hintRow, hintCol]))
                                _hints.Add(new Point[2] { new Point(hintRow, hintCol), new Point(hintRow, c) });
                        }

                        hintRow = rowMin - 1;
                        if (hintRow >= 0 && hintCol < _details.ColumnCount)
                        {
                            if (_gems[r, c].IsSameColor(_gems[hintRow, hintCol]))
                                _hints.Add(new Point[2] { new Point(hintRow, hintCol), new Point(hintRow, c) });
                        }

                        #endregion
                    }
                }
            }

            if (Parent.Game.Hero.MiniStats.Hp <= 0)
            {
                for (int i = 0; i < _endSigns.Count(); ++i)
                    _endSigns[i] = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.GemWrongSelectedEffect) as GemWrongSelectedEffect;
            }
            else if (_computer.MiniStats.Hp <= 0)
            {
                for (int i = 0; i < _endSigns.Count(); ++i)
                {
                    _endSigns[i] = Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.GemWrongSelectedEffect) as GemWrongSelectedEffect;
                    _endSigns[i].Frames = (Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.GemSelectedEffect) as GemSelectedEffect).Frames;
                }

                StateMiniGame.BackButton.Caption = "YOU WON!";
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

                                _gems[d, c].VerticalMotionModule.FallDown(5, 0.05f);

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

        void UpdateCharacterInfo()
        {
            Character hero = Parent.Game.Hero;

            _barGHero.Scale = new Vector2(1, (hero.MiniStats.ManaG / 50f));
            _barGHero.Y = _originManaBarY + _barGHero.Height * (1 - _barGHero.Scale.Y);
            _barRHero.Scale = new Vector2(1, (hero.MiniStats.ManaR / 50f));
            _barRHero.Y = _originManaBarY + _barRHero.Height * (1 - _barRHero.Scale.Y);
            _barYHero.Scale = new Vector2(1, (hero.MiniStats.ManaY / 50f));
            _barYHero.Y = _originManaBarY + _barYHero.Height * (1 - _barYHero.Scale.Y);
            _barBHero.Scale = new Vector2(1, (hero.MiniStats.ManaB / 50f));
            _barBHero.Y = _originManaBarY + _barBHero.Height * (1 - _barBHero.Scale.Y);
            _barHpHero.Scale = new Vector2(hero.MiniStats.Hp / 100f, 1);

            _barGComp.Scale = new Vector2(1, (_computer.MiniStats.ManaG / 50f));
            _barGComp.Y = _originManaBarY + _barGComp.Height * (1 - _barGComp.Scale.Y);
            _barRComp.Scale = new Vector2(1, (_computer.MiniStats.ManaR / 50f));
            _barRComp.Y = _originManaBarY + _barRComp.Height * (1 - _barRComp.Scale.Y);
            _barYComp.Scale = new Vector2(1, (_computer.MiniStats.ManaY / 50f));
            _barYComp.Y = _originManaBarY + _barYComp.Height * (1 - _barYComp.Scale.Y);
            _barBComp.Scale = new Vector2(1, (_computer.MiniStats.ManaB / 50f));
            _barBComp.Y = _originManaBarY + _barBComp.Height * (1 - _barBComp.Scale.Y);
            _barHpComp.Scale = new Vector2(_computer.MiniStats.Hp / 100f, 1);
        }

        int _compDelayCount = 0;
        int _moveDelayTime = 40;

        public override void Update(GameTime gameTime)
        {
            UpdateCharacterInfo();

            if (Parent.Game.Hero.MiniStats.Hp <= 0 || _computer.MiniStats.Hp <= 0)
            {
                foreach (GemWrongSelectedEffect i in _endSigns)
                {
                    if (i.AlphaColor == new Color(0,0,0,0))
                    {
                        i.Refresh();
                        i.ScaleVar = (float)_rand.NextDouble();
                        i.X = _rand.Next(Parent.Game.Width);
                        i.Y = _rand.Next(Parent.Game.Height);
                    }
                    i.Update(gameTime);
                }
            }
            else if (IsAllGemsStopped)
            {
                if (!_playTurn)
                {
                    ++_compDelayCount;

                    if (_compDelayCount == _moveDelayTime)
                    {
                        Point[] hint = _hints[0];
                        _gem1 = _gems[hint[0].X, hint[0].Y];
                        _gem2 = _gems[hint[1].X, hint[1].Y];

                        _selGemEffect.Frames = (Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.GemWrongSelectedEffect) as GemWrongSelectedEffect).Frames;
                        _destGemEffect.Frames = (Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.GemWrongSelectedEffect) as GemWrongSelectedEffect).Frames;
                        _selGemEffect.X = _gem1.X - Math.Abs((_selGemEffect.Width - _gem1.Width) / 2f);
                        _selGemEffect.Y = _gem1.Y - Math.Abs((_selGemEffect.Height - _gem1.Height) / 2f);
                    }
                    else if (_compDelayCount == 2 * _moveDelayTime)
                    {
                        _compDelayCount = 0;
                        MoveGems();
                        return;
                    }
                }

                CheckChains();

                if (_hints.Count == 0)
                {
                    Reset();
                    return;
                }

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
            _destGemEffect.Update(gameTime);

            _particles.Update(gameTime);
        }

        void DrawCharacterInfo(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Character hero = Parent.Game.Hero;
            spriteBatch.Draw(hero.Avatar, new Rectangle(23, 168, 68, 68), Color.White);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), hero.MainStats.Gold.ToString(), new Vector2(43, 273), Color.White);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), hero.MainStats.Exp.ToString(), new Vector2(119, 273), Color.White);

            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), hero.MiniStats.ManaG.ToString(), new Vector2(101, 248), Color.White);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), hero.MiniStats.ManaR.ToString(), new Vector2(122, 248), Color.White);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), hero.MiniStats.ManaY.ToString(), new Vector2(143, 248), Color.White);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), hero.MiniStats.ManaB.ToString(), new Vector2(164, 248), Color.White);
            
            _barGHero.Draw(gameTime, spriteBatch);
            _barRHero.Draw(gameTime, spriteBatch);
            _barYHero.Draw(gameTime, spriteBatch);
            _barBHero.Draw(gameTime, spriteBatch);
            _barHpHero.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), hero.MiniStats.Hp.ToString(), _barHpHero.Position, Color.White);

            spriteBatch.Draw(_computer.Avatar, new Rectangle(846, 168, 68, 68), Color.White);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), _computer.MainStats.Gold.ToString(), new Vector2(866, 273), Color.White);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), _computer.MainStats.Exp.ToString(), new Vector2(942, 273), Color.White);

            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), _computer.MiniStats.ManaG.ToString(), new Vector2(924, 248), Color.White);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), _computer.MiniStats.ManaR.ToString(), new Vector2(945, 248), Color.White);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), _computer.MiniStats.ManaY.ToString(), new Vector2(966, 248), Color.White);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), _computer.MiniStats.ManaB.ToString(), new Vector2(987, 248), Color.White);
            
            _barGComp.Draw(gameTime, spriteBatch);
            _barRComp.Draw(gameTime, spriteBatch);
            _barYComp.Draw(gameTime, spriteBatch);
            _barBComp.Draw(gameTime, spriteBatch);
            _barHpComp.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(SpriteFontManager.CreateObject((int)FontName.Tahoma_S_Bld), _computer.MiniStats.Hp.ToString(), _barHpComp.Position, Color.White);
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
            _destGemEffect.Draw(gameTime, spriteBatch);

            _particles.Draw(gameTime, spriteBatch);

            DrawCharacterInfo(gameTime, spriteBatch);

            if (Parent.Game.Hero.MiniStats.Hp <= 0 || _computer.MiniStats.Hp <= 0)
            {
                foreach (GemWrongSelectedEffect i in _endSigns)
                {
                    i.Draw(gameTime, spriteBatch);
                }
            }
        }
    }
}
