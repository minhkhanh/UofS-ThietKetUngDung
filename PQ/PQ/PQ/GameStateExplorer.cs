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
            _globalMap = (GlobalMap) this.MyGame.MapManager.CreateObject(0);
            _globalMap.GameObjectParent = this;
            this.ManageObjects(_globalMap);
            this._gameObjects.Add(_globalMap);
            _listBuilding = new List<GameBuilding>();
            GameBuilding tmp = (GameBuilding)this.MyGame.GameBuildingManager.CreateObject(0);
            tmp.GameObjectParent = this;
            _listBuilding.Add(tmp);
            this._gameObjects.Add(tmp);
            tmp.Animate(25);
            _character = (Character)this.MyGame.CharacterManager.CreateObject((int)CharacterName.MaleHeroKnight1);
            _character.LogicalX = 100;
            _character.LogicalY = 100;
            _character.GameObjectParent = this;
            this._gameObjects.Add(_character);            
            _character.Animate(12);
            _character.Direction = 5;
        }

        public override void InitEvents()
        {
            //_globalMap.KeyDown += new EventHandler<GameKeyEventArgs>(_globalMap_KeyDown);
        }
        public override void OnKeyDown(object o, GameKeyEventArgs e)
        {
            Rectangle gameBound = MyGame.GraphicsDevice.PresentationParameters.Bounds;
            Rectangle mapBound = Bounds;
            if (e.KeyboardState.IsKeyDown(Keys.Down))
            {
                if (mapBound.Bottom - 10 >= gameBound.Bottom)
                {
                    LogicalY -= 10;
                }
            }
            if (e.KeyboardState.IsKeyDown(Keys.Up))
            {
                if (mapBound.Top + 10 <= gameBound.Top)
                {
                    LogicalY += 10;
                }
            }
            if (e.KeyboardState.IsKeyDown(Keys.Left))
            {
                if (mapBound.Left + 10 <= gameBound.Left)
                {
                    LogicalX += 10;
                }
            }
            if (e.KeyboardState.IsKeyDown(Keys.Right))
            {
                if (mapBound.Right - 10 >= gameBound.Right)
                {
                    LogicalX -= 10;
                }
            }
        }
        void _globalMap_KeyDown(object o, GameKeyEventArgs e)
        {
            Rectangle gameBound = MyGame.GraphicsDevice.PresentationParameters.Bounds;
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
