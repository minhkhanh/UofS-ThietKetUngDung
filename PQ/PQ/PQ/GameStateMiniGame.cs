using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class GameStateMiniGame: GameState
    {
        #region components

        GameButton _backBtn;

        #endregion

        public override GameStateId StateId
        {
            get
            {
                return GameStateId.StateMiniGame;
            }
        }

        public GameStateMiniGame(MyGame game)
            : base(game)
        {
            //_stateId = GameStateId.StateMiniGame;
        }

        public override void LoadContent()
        {
            Texture2D bkgrImg = _game.Content.Load<Texture2D>(@"Images\Skin_Backdrop_Battle");
            //ImageSplittingDetails details = new ImageSplittingDetails(1, 1, 0, 0, bkgrImg.Width, bkgrImg.Height, 0, 0, 0, 0);
            //_bkgr = new Sprite2D(bkgrImg, 0, 0, details);
            _bkgr = new Sprite2D(new List<Texture2D> { bkgrImg }, 0, 0);

            _bkgr.Scale = new Vector2((float)_game.GraphicsDevice.PresentationParameters.Bounds.Width / bkgrImg.Width,
                (float)_game.GraphicsDevice.PresentationParameters.Bounds.Height / bkgrImg.Height);

            _sprites.Add(_bkgr);

            _backBtn = _game.ButtonManager.CreateObject(0) as GameButton;
            _backBtn.Caption = "BACK";
            _backBtn.Font = _game.FontManager.CreateObject(0);
            _backBtn.X = _backBtn.Y = 10;
            this.ManageObjects(_backBtn);
            _gameObjects.Add(_backBtn);
        }

        public override void InitEvents()
        {
            _backBtn.MouseClick += new EventHandler<GameMouseEventArgs>(_backBtn_MouseClick);
        }

        public void _backBtn_MouseClick(object o, GameMouseEventArgs e)
        {
            _game.SwitchState(new GameStateMainMenu(_game));
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            _backBtn.Dispose();
        }
    }
}
