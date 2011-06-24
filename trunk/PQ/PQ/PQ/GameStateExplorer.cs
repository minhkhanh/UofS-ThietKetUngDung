using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PQ
{
    public class GameStateExplorer : GameState
    {
        public override GameStateId StateId
        {
            get
            {
                return GameStateId.StateExplorer;
            }
        }
        public GameStateExplorer(MyGame game)
            : base(game)
        {

        }

        private Map _globalMap;
        List<GameBuilding> _listBuilding;
        Character _character;
        public override void LoadContent()
        {
            _globalMap = (GlobalMap) this._game.MapManager.CreateObject(0);
            this.ManageObjects(_globalMap);
            this._gameObjects.Add(_globalMap);
            _listBuilding = new List<GameBuilding>();
            GameBuilding tmp = (GameBuilding)this._game.GameBuildingManager.CreateObject(0);
            _listBuilding.Add(tmp);
            this._gameObjects.Add(tmp);
            tmp.Animate(25);
            _character = (Character)this._game.CharacterManager.CreateObject((int)CharacterName.MaleHeroKnight1);
            _character.X = 100;
            _character.Y = 100;
            this._gameObjects.Add(_character);            
            _character.Animate(12);
            _character.Direction = 5;
        }

        public override void InitEvents()
        {
            _globalMap.KeyDown += new EventHandler<GameKeyEventArgs>(_globalMap_KeyDown);
        }

        void _globalMap_KeyDown(object o, GameKeyEventArgs e)
        {
            Rectangle gameBound = _game.GraphicsDevice.PresentationParameters.Bounds;
            Rectangle mapBound = _globalMap.Bounds;
            if (e.KeyboardState.IsKeyDown(Keys.Down))
            {
                if (mapBound.Bottom - 10 >= gameBound.Bottom)
                {
                    _globalMap.Y -= 10;
                    foreach (GameObject obj in _listBuilding)
                    {
                        obj.Y -= 10;
                    }
                }
            }
            if (e.KeyboardState.IsKeyDown(Keys.Up))
            {
                if (mapBound.Top + 10 <= gameBound.Top)
                {
                    _globalMap.Y += 10;
                    foreach (GameObject obj in _listBuilding)
                    {
                        obj.Y += 10;
                    }
                }
            }
            if (e.KeyboardState.IsKeyDown(Keys.Left))
            {
                if (mapBound.Left + 10 <= gameBound.Left)
                {
                    _globalMap.X += 10;
                    foreach (GameObject obj in _listBuilding)
                    {
                        obj.X += 10;
                    }
                }
            }
            if (e.KeyboardState.IsKeyDown(Keys.Right))
            {
                if (mapBound.Right - 10 >= gameBound.Right)
                {
                    _globalMap.X -= 10;
                    foreach (GameObject obj in _listBuilding)
                    {
                        obj.X -= 10;
                    }
                }
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            _globalMap.Dispose();
        }
    }
}
