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

        public GameStateMiniGame(GameStateManager manager)
            : base(manager)
        {
            _stateId = GameStateId.StateMiniGame;
        }

        public override void LoadContent()
        {
            Texture2D bkgrImg = _manager.MyGame.Content.Load<Texture2D>(@"Images\Skin_Backdrop_Battle");
            //ImageSplittingDetails details = new ImageSplittingDetails(1, 1, 0, 0, bkgrImg.Width, bkgrImg.Height, 0, 0, 0, 0);
            //_bkgr = new Sprite2D(bkgrImg, 0, 0, details);
            _bkgr = new Sprite2D(new Texture2D[] { bkgrImg }, 0, 0);

            _bkgr.Scale = new Vector2((float)_manager.MyGame.GraphicsDevice.PresentationParameters.Bounds.Width / bkgrImg.Width,
                (float)_manager.MyGame.GraphicsDevice.PresentationParameters.Bounds.Height / bkgrImg.Height);

            _sprites.Add(_bkgr);

            _backBtn = _manager.MyGame.ButtonManager.CreateObject(0) as GameButton;
            _backBtn.Caption = "BACK";
            _backBtn.Font = _manager.MyGame.FontManager.CreateObject(0);
            _backBtn.X = _backBtn.Y = 10;
            this.ManageObjects(_backBtn);
            this.Items.Add(_backBtn);
        }

        public override void InitEvents()
        {
            _backBtn.MouseClick += new EventHandler<GameMouseEventArgs>(_backBtn_MouseClick);
        }

        public void _backBtn_MouseClick(object o, GameMouseEventArgs e)
        {
            _manager.SwitchState(GameStateId.StateMainMenu);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            _backBtn.Dispose();
        }
    }
}
