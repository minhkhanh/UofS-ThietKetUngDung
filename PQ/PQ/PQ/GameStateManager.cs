//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace PQ
//{
//    public class GameStateManager
//    {
//        //List<GameState> _gameStates = new List<GameState>();
//        GameState _currGameState;
//        //GameStateId _currGameStateId = GameStateId.None;

//        MyGame _myGame;
//        public MyGame MyGame
//        {
//            get { return _myGame; }
//            set { _myGame = value; }
//        }

//        public GameState CurrentGameState
//        {
//            get
//            {
//                //return _gameStates.Find(i => i.StateId == _currGameStateId);
//                return _currGameState;
//            }
//        }

//        public GameStateManager(MyGame myGame)
//        {
//            _myGame = myGame;

//            _currGameState = new GameStateMainMenu(this);
//            _myGame.ManageObjects(_currGameState);

//            //_gameStates.Add(new GameStateMainMenu(this));
//            //_gameStates.Add(new GameStateMiniGame(this));

//            //myGame.ManageObjects(_gameStates.ToArray());

//            //_currGameStateId = GameStateId.StateMainMenu;
//        }

//        public void SwitchState(GameState nextState)
//        {
//            CurrentGameState.EndState();
//            //_currGameStateId = nextState;
//            _currGameState = nextState;
//            CurrentGameState.StartState();
//            //CurrentGameState.EnterState();
//        }
//    }
//}
