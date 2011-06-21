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
        protected List<GameObject> _gameObjects = new List<GameObject>();
        public virtual List<GameObject> Items
        {
            get { return _gameObjects; }
            set { _gameObjects = value; }
        }

        public override float X
        {
            get
            {
                return base.X;
            }
            set
            {
                float dx = value - base.X;

                foreach (GameObject i in _gameObjects)
                    i.X += dx;

                base.X = value;
            }
        }

        public override float Y
        {
            get
            {
                return base.Y;
            }
            set
            {
                float dy = value - base.Y;

                foreach (GameObject i in _gameObjects)
                    i.Y += dy;

                base.Y = value;
            }
        }

        public override List<Rectangle> Regions
        {
            get
            {
                List<Rectangle> regs = new List<Rectangle>();

                for (int i = 0; i < _sprites.Count; ++i)
                    regs.Add(_sprites[i].Bounds);
                for (int i = 0; i < _gameObjects.Count; ++i)
                    regs.AddRange(_gameObjects[i].Regions);

                return regs;
            }
        }

        public void ManageObjects(params GameObject[] gameObjs)
        {
            //_gameObjects.AddRange(gameObjs);

            for (int i = 0; i < gameObjs.Count(); ++i)
            {
                //_gameObjects.Add(gameObjs[i]);

                this.MouseDown += new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseDown);
                this.MouseUp += new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseUp);
            }
        }

        public void UnmanageObjects(params GameObject[] gameObjs)
        {
            for (int i = 0; i < gameObjs.Count(); ++i)
            {
                this.MouseDown -= new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseDown);
                this.MouseUp -= new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseUp);                
            }
        }

        public virtual void CheckCollision()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CheckCollision();

            for (int i = 0; i < _gameObjects.Count; ++i)
                _gameObjects[i].Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            for (int i = 0; i < _gameObjects.Count; ++i)
                _gameObjects[i].Draw(gameTime, spriteBatch);            
        }

        public override void OnMouseHover(object o, GameMouseEventArgs e)
        {
            base.OnMouseHover(o, e);

            for (int i = 0; i < _gameObjects.Count; ++i)
                _gameObjects[i].OnMouseHover(o, e);
        }

        public override void OnMouseLeave(object o, GameMouseEventArgs e)
        {
            base.OnMouseLeave(o, e);

            for (int i = 0; i < _gameObjects.Count; ++i)
                _gameObjects[i].OnMouseLeave(o, e);
        }

        public override void Dispose()
        {
            base.Dispose();

            for (int i = 0; i < _gameObjects.Count; ++i)
            {
                UnmanageObjects(_gameObjects[i]);
                _gameObjects[i].Dispose();
            }

            _gameObjects.Clear();
        }
    }
}
