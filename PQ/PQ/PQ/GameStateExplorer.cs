using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public override void LoadContent()
        {
            base.LoadContent();
            _globalMap = (GlobalMap) this._game.MapManager.CreateObject(0);
            this._gameObjects.Add(_globalMap);
        }
        
    }
}
