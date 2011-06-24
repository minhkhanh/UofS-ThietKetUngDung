using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace PQ
{
    public enum GameStateId
    {
        State,
        StateMainMenu,
        StateExplorer,
        StateMiniGame
    }

    public enum GameStateMessageId
    {
        None,
        MessageEnterState,
        MessageUpdateState,
        MessageDrawState,
        MessageExitState,
    }

    public abstract class GameState: GameDialog
    {
        protected MyGame _game;
        public MyGame Game
        {
            get { return _game; }
            set { _game = value; }
        }
        //protected GameStateManager _manager;
        //protected GameStateId _stateId;
        public virtual GameStateId StateId
        {
            get { return GameStateId.State; }
        }

        protected Sprite2D _bkgr;

        public GameState(MyGame game)
        {
            _game = game;
        }

        public virtual void LoadContent()
        {
        }

        public virtual void InitEvents()
        {
        }

        public virtual void PlaySongs()
        {
        }

        public virtual void StopSongs()
        {
        }

        public virtual void UnloadContent()
        {
            //_bkgr.Dispose();
        }

        public void StartState()
        {
            InitState();
            EnterState();
        }

        #region life cycle of a game state

        public void InitState()
        {
            LoadContent();
            InitEvents();
        }

        public void EnterState()
        {            
            PlaySongs();

            _game.ManageObjects(this);
        }

        public void ExitState()
        {
            StopSongs();

            _game.UnmanageObjects(this);
        }

        public void EndState()
        {
            ExitState();
            UnloadContent();
        }

        #endregion      
        
        public override void ManageObjects(params GameObject[] gameObjs)
        {
            base.ManageObjects(gameObjs);

            foreach (GameObject i in gameObjs)
            {
                i.Parent = this;
            }
        }
    }
}