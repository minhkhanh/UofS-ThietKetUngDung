using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public abstract class GameDialog : GameWindow
    {
        List<GameControl> _controls = new List<GameControl>();

        public override List<Rectangle> Regions
        {
            get
            {
                List<Rectangle> regs = new List<Rectangle>();

                for (int i = 0; i < _sprites.Count; ++i)
                    regs.Add(_sprites[i].Bound);
                for (int i = 0; i < _controls.Count; ++i )
                    regs.AddRange(_controls[i].Regions);

                return regs;
            }
        }

        public void ManageControls(params GameControl[] controls)
        {
            _controls.AddRange(controls);

            for (int i = 0; i < controls.Count(); ++i)
            {
                this.MouseDown += new EventHandler<GameMouseEventArgs>(controls[i].OnMouseDown);
                this.MouseUp += new EventHandler<GameMouseEventArgs>(controls[i].OnMouseUp);
                this.MouseHover += new EventHandler<GameMouseEventArgs>(controls[i].OnMouseHover);
                this.MouseLeave += new EventHandler<GameMouseEventArgs>(controls[i].OnMouseLeave);
            }
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _controls.Count; ++i)
                _controls[i].Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _controls.Count; ++i)
                _controls[i].Draw(gameTime, spriteBatch);
        }

        public override void OnMouseHover(object o, GameMouseEventArgs e)
        {
            base.OnMouseHover(o, e);

            for (int i = 0; i < _controls.Count; ++i)
                _controls[i].OnMouseHover(o, e);
        }

        public override void OnMouseLeave(object o, GameMouseEventArgs e)
        {
            base.OnMouseLeave(o, e);

            for (int i = 0; i < _controls.Count; ++i)
                _controls[i].OnMouseLeave(o, e);
        }
    }
}
