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
        PuzzleBoard _board;

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
            Texture2D bkgrImg = Game.Content.Load<Texture2D>(@"Images\Skin_Backdrop_Battle");
            _bkgr = new Sprite2D(new List<Texture2D> { bkgrImg }, 0, 0);

            //_bkgr.Scale = new Vector2((float)_game.GraphicsDevice.PresentationParameters.Bounds.Width / bkgrImg.Width,
            //    (float)_game.GraphicsDevice.PresentationParameters.Bounds.Height / bkgrImg.Height);

            _sprites.Add(_bkgr);

            _backBtn = Game.ButtonManager.CreateObject(0) as GameButton;
            _backBtn.Caption = "BACK";
            _backBtn.Font = Game.FontManager.CreateObject(0) as SpriteFont;
            _backBtn.X = _backBtn.Y = 10;
            this.ManageObjects(_backBtn);
            _gameObjects.Add(_backBtn);

            SplittingDetails details = new SplittingDetails(8, 8, -1, -1, 71, 66, 3, 8, 3, 5);
            _board = new PuzzleBoard(details, Game.SpriteManager);
            this.ManageObjects(_board);
            _gameObjects.Add(_board);
            _board.X = 215;
            _board.Y = 128;
            _board.Reset();            
        }

        public override void InitEvents()
        {
            _backBtn.MouseClick += new EventHandler<GameMouseEventArgs>(_backBtn_MouseClick);
        }

        public void _backBtn_MouseClick(object o, GameMouseEventArgs e)
        {
            Game.SwitchState(new GameStateMainMenu(Game));
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            _backBtn.Dispose();
        }
    }
}
