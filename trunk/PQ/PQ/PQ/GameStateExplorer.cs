using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Data;

namespace PQ
{
    public class GameStateExplorer : GameState
    {
        public override GameStateId StateId
        {
            get
            {
                return GameStateId.StateExplorer;
            }
        }
        public GameStateExplorer(MyGame game)
            : base(game)
        {

        }

        private Map _globalMap;
        List<GameBuilding> _listBuilding;
        Character _character;

        public override void LoadContent()
        {
            _globalMap = (GlobalMap) this.MyGame.MapManager.CreateObject(0);
            _globalMap.GameObjectParent = this;
            this.ManageObjects(_globalMap);
            this._gameObjects.Add(_globalMap);

            _listBuilding = new List<GameBuilding>();

            DataSet vdDataSet = new DataSet();
            string database = @"Content\Maps\Map.xml";
            vdDataSet.ReadXml(database);
            DataRow[] drs = vdDataSet.Tables["Building"].Select();
            foreach (DataRow dr in drs)
            {
                int i;
                int.TryParse(dr["template"].ToString(), out i);
                GameBuilding tmp = (GameBuilding)this.MyGame.GameBuildingManager.CreateObject(i);
                tmp.GameObjectParent = this;
                int.TryParse(dr["x"].ToString(), out i);
                tmp.LogicalX = i;
                int.TryParse(dr["y"].ToString(), out i);
                tmp.LogicalY = i;
                _listBuilding.Add(tmp);
                this._gameObjects.Add(tmp);
                int.TryParse(dr["fps"].ToString(), out i);
                tmp.Animate(i);
                tmp.MouseUp += new EventHandler<GameMouseEventArgs>(building_MouseUp);
                this.ManageObjects(tmp);
            }

            _character = (Character)this.MyGame.CharacterManager.CreateObject((int)CharacterName.HeroKnightMale1);
            _character.GameObjectParent = this;
            this._gameObjects.Add(_character);            
            _character.Animate(12);
            _character.DirectionSprite = 4;
            MusicManager.Play("Carl Orff - O Fortuna");
        }

        void building_MouseUp(object sender, GameMouseEventArgs e)
        {
            Vector2 v = this.ConvertPhysical2Logical(new Vector2(e.MouseState.X, e.MouseState.Y));
            Vector2 vC = new Vector2(_character.LogicalX, _character.LogicalY);
            float fDis;
            Vector2.Distance(ref vC, ref v, out fDis);
            if (fDis<50)
            {
                Game.SwitchState(new GameStateMiniGame(
                    Game.CharacterManager.RandomObject() as Character
                    , Game));
                return;
            }
            _character.GoToLogicalXY(v.X, v.Y);
        }

        public override void InitEvents()
        {
            this.MouseUp += new EventHandler<GameMouseEventArgs>(GameStateExplorer_MouseUp);
            this.KeyDown += new EventHandler<GameKeyEventArgs>(GameStateExplorer_KeyDown);
        }

        void GameStateExplorer_KeyDown(object sender, GameKeyEventArgs e)
        {
            Rectangle gameBound = MyGame.GraphicsDevice.PresentationParameters.Bounds;
            Rectangle mapBound = Bounds;
            if (e.KeyboardState.IsKeyDown(Keys.Down))
            {
                if (mapBound.Bottom - 10 >= gameBound.Bottom)
                {
                    LogicalY -= 10;
                }
            }
            if (e.KeyboardState.IsKeyDown(Keys.Up))
            {
                if (mapBound.Top + 10 <= gameBound.Top)
                {
                    LogicalY += 10;
                }
            }
            if (e.KeyboardState.IsKeyDown(Keys.Left))
            {
                if (mapBound.Left + 10 <= gameBound.Left)
                {
                    LogicalX += 10;
                }
            }
            if (e.KeyboardState.IsKeyDown(Keys.Right))
            {
                if (mapBound.Right - 10 >= gameBound.Right)
                {
                    LogicalX -= 10;
                }
            }
        }

        void GameStateExplorer_MouseUp(object sender, GameMouseEventArgs e)
        {
            
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            _globalMap.Dispose();
        }
    }
}
