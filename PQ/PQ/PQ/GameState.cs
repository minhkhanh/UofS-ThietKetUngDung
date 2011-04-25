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
        None,
        StateMainMenu,
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
        protected GameStateManager _manager;

        protected GameStateId _stateId;
        public GameStateId StateId
        {
            get { return _stateId; }
        }

        protected Sprite2D _bkgr;

        public GameState(GameStateManager manager)
        {
            _manager = manager;
            _stateId = GameStateId.None;
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
        }

        public void ExitState()
        {
            StopSongs();
        }

        public void EndState()
        {
            UnloadContent();
        }

        #endregion                
    }
}