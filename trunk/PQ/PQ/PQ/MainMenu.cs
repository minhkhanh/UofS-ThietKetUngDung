using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class MainMenu: GameMenu
    {
        GameButton _okBtn;

        public GameButton OKButton
        {
            get { return _okBtn; }
            set { _okBtn = value; }
        }

        GameButton _exitBtn;

        public GameButton ExitButton
        {
            get { return _exitBtn; }
            set { _exitBtn = value; }
        }

        public MainMenu(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Init(GameButtonManager btnManager, SpriteFontManager fontManager)
        {
            _okBtn = btnManager.CreateObject(0) as GameButton;
            _okBtn.X = X;
            _okBtn.Y = Y;
            _okBtn.Caption = "ENTER";
            _okBtn.Font = fontManager.CreateObject(0);

            this.ManageObjects(_okBtn);
            this.Items.Add(_okBtn);

            _exitBtn = btnManager.CreateObject(0) as GameButton;
            _exitBtn.X = X;
            _exitBtn.Y = _okBtn.Bounds.Bottom + 5;
            _exitBtn.Caption = "EXIT";
            _exitBtn.Font = fontManager.CreateObject(0);

            this.ManageObjects(_exitBtn);
            this.Items.Add(_exitBtn);

            _motionModule = new VerticalPlaneMotionModule(-0.05f, 0, -1f, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (X <= 600)
                _motionModule.Stop();
        }

        public override void Dispose()
        {
            base.Dispose();

            _okBtn.Dispose();
            _exitBtn.Dispose();
        }
    }
}
