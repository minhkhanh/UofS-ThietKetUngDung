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
        //protected MyGame _game;

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

                List<GameObject> allObjs = GetAllObjs();
                foreach (GameObject i in allObjs)
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

                List<GameObject> allObjs = GetAllObjs();
                foreach (GameObject i in allObjs)
                    i.Y += dy;

                base.Y = value;
            }
        }

        //public GameDialog() { }

        /// <summary>
        /// Get all child objects that being held by this object
        /// </summary>
        /// <returns></returns>
        public virtual List<GameObject> GetAllObjs()
        {
            return _gameObjects;
        }

        public override Rectangle Bounds
        {
            get
            {
                Rectangle bound = base.Bounds;

                List<GameObject> allObjs = GetAllObjs();
                if (bound.IsEmpty && allObjs.Count > 0)
                    bound = allObjs[0].Bounds;

                foreach (GameObject i in allObjs)
                {
                    bound = Rectangle.Union(bound, i.Bounds);
                }

                return bound;
            }
        }

        public override List<Rectangle> Regions
        {
            get
            {
                List<Rectangle> regs = base.Regions;

                List<GameObject> allObjs = GetAllObjs();

                for (int i = 0; i < allObjs.Count; ++i)
                    regs.AddRange(allObjs[i].Regions);

                return regs;
            }
        }

        public virtual void Initialize()
        {

        }

        public virtual void ManageObjects(params GameObject[] gameObjs)
        {
            //_gameObjects.AddRange(gameObjs);

            for (int i = 0; i < gameObjs.Count(); ++i)
            {
                gameObjs[i].Parent = _parent;

                this.MouseDown += new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseDown);
                this.MouseUp += new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseUp);
                this.KeyDown += new EventHandler<GameKeyEventArgs>(gameObjs[i].OnKeyDown);
            }
        }

        public void UnmanageObjects(params GameObject[] gameObjs)
        {
            for (int i = 0; i < gameObjs.Count(); ++i)
            {
                gameObjs[i].Parent = null;

                this.MouseDown -= new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseDown);
                this.MouseUp -= new EventHandler<GameMouseEventArgs>(gameObjs[i].OnMouseUp);
                this.KeyDown -= new EventHandler<GameKeyEventArgs>(gameObjs[i].OnKeyDown);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            List<GameObject> allObjs = GetAllObjs();
            for (int i = 0; i < allObjs.Count; ++i)
                allObjs[i].Update(gameTime);

            //CheckCollision();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            List<GameObject> allObjs = GetAllObjs();
            for (int i = 0; i < allObjs.Count; ++i)
                allObjs[i].Draw(gameTime, spriteBatch);
        }

        public override void OnMouseHover(object o, GameMouseEventArgs e)
        {
            base.OnMouseHover(o, e);

            List<GameObject> allObjs = GetAllObjs();
            for (int i = 0; i < allObjs.Count; ++i)
                allObjs[i].OnMouseHover(o, e);
        }

        public override void OnMouseLeave(object o, GameMouseEventArgs e)
        {
            base.OnMouseLeave(o, e);

            List<GameObject> allObjs = GetAllObjs();
            for (int i = 0; i < allObjs.Count; ++i)
                allObjs[i].OnMouseLeave(o, e);
        }

        public override void Dispose()
        {
            base.Dispose();

            List<GameObject> allObjs = GetAllObjs();
            for (int i = 0; i < allObjs.Count; ++i)
            {
                UnmanageObjects(allObjs[i]);
                allObjs[i].Dispose();
            }

            _gameObjects.Clear();
        }
    }
}
