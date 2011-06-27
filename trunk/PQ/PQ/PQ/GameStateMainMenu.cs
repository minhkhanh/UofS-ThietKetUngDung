using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace PQ
{
    public class GameStateMainMenu: GameState
    {
        #region components

        MainMenu _mainMenu;

        #endregion

        public override GameStateId StateId
        {
            get
            {
                return GameStateId.StateMainMenu;
            }
        }

        public GameStateMainMenu(MyGame game)
            : base(game)
        {
            //_stateId = GameStateId.StateMainMenu;
        }

        public override void LoadContent()
        {
            _mainMenu = new MainMenu(Game.GraphicsDevice.PresentationParameters.Bounds.Width, 300);
            _mainMenu.Init(Game.ButtonManager);
            this.ManageObjects(_mainMenu);
            _gameObjects.Add(_mainMenu);

            Texture2D bkgrImg = Game.Content.Load<Texture2D>(@"Images\Skin_Backdrop_Standard");
            //ImageSplittingDetails details = new ImageSplittingDetails(1, 1, 0, 0, bkgrImg.Width, bkgrImg.Height, 0, 0, 0, 0);
            //_bkgr = new Sprite2D(bkgrImg, 0, 0, details);
            _bkgr = new Sprite2D(new List<Texture2D> { bkgrImg }, 0, 0);

            //_bkgr.Scale = new Vector2((float)_game.GraphicsDevice.PresentationParameters.Bounds.Width / bkgrImg.Width,
            //    (float)_game.GraphicsDevice.PresentationParameters.Bounds.Height / bkgrImg.Height);

            _sprites.Add(_bkgr);
        }

        public override void InitEvents()
        {
            _mainMenu.OKButton.MouseClick += new EventHandler<GameMouseEventArgs>(OKButton_MouseClick);
            _mainMenu.ExitButton.MouseClick += new EventHandler<GameMouseEventArgs>(ExitButton_MouseClick);
        }

        public void ExitButton_MouseClick(object o, GameMouseEventArgs e)
        {
            Game.Exit();
        }

        public void OKButton_MouseClick(object o, GameMouseEventArgs e)
        {
            Game.SwitchState(new GameStateExplorer(Game));
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            _mainMenu.Dispose();
        }
    }
}
