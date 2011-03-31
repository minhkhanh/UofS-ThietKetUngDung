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
        List<Character> _characters = new List<Character>();

        MainMenu _mainMenu = new MainMenu();
        GameButtonManager _btnManager = new GameButtonManager();

        SpriteFontManager _fontManager = new SpriteFontManager();

        Texture2D _bkgr;

        #region events

        bool _leftMouseLastClicked = false;

        public event EventHandler<GameMouseEventArgs> GameMouseDown;
        public event EventHandler<GameMouseEventArgs> GameMouseUp;
        public event EventHandler<GameMouseEventArgs> GameMouseHover;
        public event EventHandler<GameMouseEventArgs> GameMouseLeave;

        void RaiseGameMouseEvent(EventHandler<GameMouseEventArgs> GameMouseEvent, object o, GameMouseEventArgs e)
        {
            EventHandler<GameMouseEventArgs> handler = GameMouseEvent;
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

            _bkgr = Content.Load<Texture2D>(@"Images\Skin_Splash");

            GameMouseDown += new EventHandler<GameMouseEventArgs>(_mainMenu.OnMouseDown);
            GameMouseUp += new EventHandler<GameMouseEventArgs>(_mainMenu.OnMouseUp);
            GameMouseHover += new EventHandler<GameMouseEventArgs>(_mainMenu.OnMouseHover);
            GameMouseLeave += new EventHandler<GameMouseEventArgs>(_mainMenu.OnMouseLeave);

            

            _fontManager.InitPrototypes(Content);
            _btnManager.InitPrototypes(Content);

            _mainMenu.Init(_btnManager, _fontManager);
        }

        private void LoadSampleCharacters()
        {
            _characterManager.InitPrototypes(this.Content);

            _characters.Add((Character)_characterManager.CreateObject(0));
            _characters[0].Animate(10);
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

            int i = 0;
            for (i = 0; i < _characters.Count; ++i)
                _characters[i].Update(gameTime);

            _mainMenu.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(_bkgr, GraphicsDevice.PresentationParameters.Bounds, Color.White);

            for (int i = 0; i < _characters.Count; ++i)           
                _characters[i].Draw(gameTime, spriteBatch);

            _mainMenu.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
