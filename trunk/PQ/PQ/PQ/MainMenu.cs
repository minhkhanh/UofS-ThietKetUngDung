using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQ
{
    public class MainMenu: GameMenu
    {
        public void Init(GameButtonManager btnManager, SpriteFontManager fontManager)
        {
            GameButton btn = btnManager.CreateObject(0) as GameButton;
            btn.X = btn.Y = 100;
            btn.Caption = "Button 01";
            btn.Font = fontManager.CreateObject(0);

            this.ManageControls((GameControl)btn);

            GameButton btn02 = btnManager.CreateObject(0) as GameButton;
            btn02.X = 100;
            btn02.Y = btn.Bound.Bottom;
            btn02.Caption = "Button 02";
            btn02.Font = fontManager.CreateObject(0);

            this.ManageControls((GameControl)btn02);
        }
    }
}
