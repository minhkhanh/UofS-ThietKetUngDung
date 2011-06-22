using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PQ
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MyGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region object managers

        GameButtonManager _buttonManager = new GameButtonManager();
        public GameButtonManager ButtonManager
        {
            get { return _buttonManager; }
            set { _buttonManager = value; }
        }

        SpriteFontManager _fontManager = new SpriteFontManager();
        public SpriteFontManager FontManager
        {
            get { return _fontManager; }
            set { _fontManager = value; }
        }

        MapManager _mapManager = new MapManager();
        public PQ.MapManager MapManager
        {
            get { return _mapManager; }
            set { _mapManager = value; }
        }
        //GameStateManager _gameStateManager;

        GemManager _gemManager = new GemManager();

        public GemManager GemManager
        {
            get { return _gemManager; }
            set { _gemManager = value; }
        }

        GameBuildingManager _gameBuildingManager = new GameBuildingManager();
        public PQ.GameBuildingManager GameBuildingManager
        {
            get { return _gameBuildingManager; }
            set { _gameBuildingManager = value; }
        }
        #endregion

        #region events

        bool _leftMouseLastClicked = false;

        public event EventHandler<GameMouseEventArgs> GameMouseDown;
        public event EventHandler<GameMouseEventArgs> GameMouseUp;
        public event EventHandler<GameMouseEventArgs> GameMouseHover;
        public event EventHandler<GameMouseEventArgs> GameMouseLeave;

        public event EventHandler<GameKeyEventArgs> GameKeyDown;

        void RaiseMouseEvent(EventHandler<GameMouseEventArgs> GameMouseEvent, object o, GameMouseEventArgs e)
        {
            EventHandler<GameMouseEventArgs> handler = GameMouseEvent;
            if (handler != null)
                handler(o, e);
        }

        void RaiseKeyEvent(EventHandler<GameKeyEventArgs> GameKeyEvent, object o, GameKeyEventArgs e)
        {
            EventHandler<GameKeyEventArgs> handler = GameKeyEvent;
            if (handler != null)
                handler(o, e);
        }

        void RaiseGameMouseEvents(MouseState msState)
        {
            if (!_leftMouseLastClicked && msState.LeftButton == ButtonState.Pressed)
            {
                RaiseMouseEvent(GameMouseDown, this, new GameMouseEventArgs(msState));
                _leftMouseLastClicked = true;
            }
            if (_leftMouseLastClicked && msState.LeftButton == ButtonState.Released)
            {
                RaiseMouseEvent(GameMouseUp, this, new GameMouseEventArgs(msState));
                _leftMouseLastClicked = false;
            }

            RaiseMouseEvent(GameMouseHover, this, new GameMouseEventArgs(msState));
            RaiseMouseEvent(GameMouseLeave, this, new GameMouseEventArgs(msState));

            //msState.
        }

        void RaiseGameKeyEvents(KeyboardState kbState)
        {
            Keys[] keys = kbState.GetPressedKeys();
            if (keys.Count() == 0)
                return;     // no keys down

            RaiseKeyEvent(GameKeyDown, this, new GameKeyEventArgs(kbState));
        }

        #endregion        

        #region game states

        GameState _currState;

        public void SwitchState(GameState nextState)
        {
            _currState.EndState();
            _currState = nextState;
            _currState.StartState();
        }

        #endregion

        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public void ManageObjects(params GameObject[] gameObjs)
        {
            for (int i = 0; i < gameObjs.Count(); ++i)
            {
                this.GameMouseDown += new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseDown);
                this.GameMouseUp += new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseUp);
                this.GameMouseHover += new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseHover);
                this.GameMouseLeave += new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseLeave);
                this.GameKeyDown += new EventHandler<GameKeyEventArgs>(gameObjs[i].OnKeyDown);
            }
        }

        public void UnmanageObjects(params GameObject[] gameObjs)
        {
            for (int i = 0; i < gameObjs.Count(); ++i)
            {
                this.GameMouseDown -= new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseDown);
                this.GameMouseUp -= new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseUp);
                this.GameMouseHover -= new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseHover);
                this.GameMouseLeave -= new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseLeave);
                this.GameKeyDown -= new EventHandler<GameKeyEventArgs>(gameObjs[i].OnKeyDown);
            }
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = (int)(1024 * GlobalClass.SCALE);
            graphics.PreferredBackBufferHeight = (int)(768 * GlobalClass.SCALE);
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            //_gameStateManager = new GameStateManager(this);
            //_currState = new GameStateMiniGame(this);
            _currState = new GameStateExplorer(this);

            base.Initialize();
        }

        void LoadManagers()
        {
            _buttonManager.LoadPrototypes(Content);
            _fontManager.LoadPrototypes(Content);
            _gemManager.LoadPrototypes(Content);
            _mapManager.LoadPrototypes(Content);
            _gameBuildingManager.LoadPrototypes(Content);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadManagers();

            //_gameStateManager.EnterCurrentState();
            //_gameStateManager.CurrentGameState.StartState();
            _currState.StartState();
        }

        protected override void UnloadContent()
        {
            
        }        

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState msState = Mouse.GetState();
            //MouseState newMs = new MouseState((int)(msState.X * GlobalClass.SCALE), (int)(msState.Y * GlobalClass.SCALE), msState.ScrollWheelValue,
            //    msState.LeftButton, msState.MiddleButton, msState.RightButton, msState.XButton1, msState.XButton2
            //    );
            RaiseGameMouseEvents(msState);

            KeyboardState kbState = Keyboard.GetState();
            RaiseGameKeyEvents(kbState);

            //_gameStateManager.CurrentGameState.Update(gameTime);
            _currState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            _currState.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}