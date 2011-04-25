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



        #endregion

        public GameStateMiniGame(GameStateManager manager)
            : base(manager)
        {
            _stateId = GameStateId.StateMiniGame;
        }

        public override void LoadContent()
        {
            Texture2D bkgrImg = _manager.MyGame.Content.Load<Texture2D>(@"Images\Skin_Backdrop_Battle");
            ImageSplittingDetails details = new ImageSplittingDetails(1, 1, 0, 0, bkgrImg.Width, bkgrImg.Height, 0, 0, 0, 0);
            _bkgr = new Sprite2D(bkgrImg, 0, 0, details);

            _bkgr.Scale = new Vector2((float)_manager.MyGame.GraphicsDevice.PresentationParameters.Bounds.Width / bkgrImg.Width,
                (float)_manager.MyGame.GraphicsDevice.PresentationParameters.Bounds.Height / bkgrImg.Height);

            _sprites.Add(_bkgr);
        }
    }
}
