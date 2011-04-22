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
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        CharacterManager _characterManager = new CharacterManager();
        Character _character;// = new List<Character>();

        MainMenu _mainMenu = new MainMenu();
        GameButtonManager _btnManager = new GameButtonManager();
        //SpriteFontManager _fontManager = new SpriteFontManager();
        MapManager _mapManager = new MapManager();
        //TerrainManager _terrainManager = new TerrainManager();

        TiledMap _tiledMap = new TiledMap();

        //Texture2D _bkgr;

        GlobalMap _largeMap;// = new Map();

        #region events

        bool _leftMouseLastClicked = false;

        public event EventHandler<GameMouseEventArgs> GameMouseDown;
        public event EventHandler<GameMouseEventArgs> GameMouseUp;
        public event EventHandler<GameMouseEventArgs> GameMouseHover;
        public event EventHandler<GameMouseEventArgs> GameMouseLeave;

        public event EventHandler<GameKeyEventArgs> GameKeyDown;

        void RaiseGameMouseEvent(EventHandler<GameMouseEventArgs> GameMouseEvent, object o, GameMouseEventArgs e)
        {
            EventHandler<GameMouseEventArgs> handler = GameMouseEvent;
            if (handler != null)
                handler(o, e);
        }

        void RaiseGameKeyEvent(EventHandler<GameKeyEventArgs> GameMouseEvent, object o, GameKeyEventArgs e)
        {
            EventHandler<GameKeyEventArgs> handler = GameKeyDown;
            if (handler != null)
                handler(o, e);
        }

        #endregion        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //_bkgr = Content.Load<Texture2D>(@"Images\Skin_Splash");

            LoadSampleCharacters();

            GameMouseDown += new EventHandler<GameMouseEventArgs>(OnGameMouseDown);
            //GameMouseUp += new EventHandler<GameMouseEventArgs>(_mainMenu.OnMouseUp);
            //GameMouseHover += new EventHandler<GameMouseEventArgs>(_mainMenu.OnMouseHover);
            //GameMouseLeave += new EventHandler<GameMouseEventArgs>(_mainMenu.OnMouseLeave);

            //_terrainManager.InitPrototypes(Content);

            _mapManager.InitPrototypes(Content);

            _largeMap = _mapManager.CreateObject(0) as GlobalMap;
            //_tiledMap = _mapManager.CreateObject(1) as TiledMap;

            GameKeyDown += new EventHandler<GameKeyEventArgs>(_largeMap.OnKeyDown);
            //GameKeyDown += new EventHandler<GameKeyEventArgs>(_character.OnKeyDown);
        }

        private void OnGameMouseDown(object o, GameMouseEventArgs e)
        {
            _character.Center = new Point(e.MouseState.X, e.MouseState.Y);
        }

        private void LoadSampleCharacters()
        {
            _characterManager.InitPrototypes(Content);

            _character = (Character)_characterManager.CreateObject(0);
            _character.Animate(10);
        }

        private void LoadSampleButtons()
        {
            
        }

        protected override void UnloadContent()
        {
            //MouseState
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState msState = Mouse.GetState();

            if (!_leftMouseLastClicked && msState.LeftButton == ButtonState.Pressed)
            {
                RaiseGameMouseEvent(GameMouseDown, this, new GameMouseEventArgs(msState));
                _leftMouseLastClicked = true;
            }
            if (_leftMouseLastClicked && msState.LeftButton == ButtonState.Released)
            {
                RaiseGameMouseEvent(GameMouseUp, this, new GameMouseEventArgs(msState));
                _leftMouseLastClicked = false;
            }

            RaiseGameMouseEvent(GameMouseHover, this, new GameMouseEventArgs(msState));
            RaiseGameMouseEvent(GameMouseLeave, this, new GameMouseEventArgs(msState));

            KeyboardState kbState = Keyboard.GetState();
            //kbState.GetPressedKeys()
            RaiseGameKeyEvent(GameKeyDown, this, new GameKeyEventArgs(kbState));

            _largeMap.Update(gameTime);

            _character.Update(gameTime);

            //_mainMenu.Update(gameTime);
            
            //_tiledMap.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //spriteBatch.Draw(_bkgr, GraphicsDevice.PresentationParameters.Bounds, Color.White);

            _largeMap.Draw(gameTime, spriteBatch);

            _character.Draw(gameTime, spriteBatch);

            //_mainMenu.Draw(gameTime, spriteBatch);
            
            //_tiledMap.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
