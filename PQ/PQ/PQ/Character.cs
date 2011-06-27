using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using Microsoft.Xna.Framework.Input;

namespace PQ
{
    public class Character : GameEntity
    {
        Texture2D _avatar;
        public Texture2D Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }

        protected CharacterStats _mainStats;

        public CharacterStats MainStats
        {
            get { return _mainStats; }
            set { _mainStats = value; }
        }

        protected MiniGameStats _miniStats = null;
        public MiniGameStats MiniStats
        {
            get { return _miniStats; }
            set { _miniStats = value; }
        }

        public void CreateMiniStats()
        {
            _miniStats = new MiniGameStats(0, 0, 0, 0, 100);
        }

        public override GameObject Clone()
        {
            Character character = new Character();

            for (int i = 0; i < _sprites.Count; ++i)
                character._sprites.Add(new Sprite2D(_sprites[i]));

            character.Avatar = _avatar;
            character.MainStats = new CharacterStats(_mainStats);

            character.GameObjectParent = this.GameObjectParent;
            character.LogicalX = this.LogicalX;
            character.LogicalY = this.LogicalY;
            character.UpdateChild();
            

            return character;
        }

        public override int DirectionSprite
        {
            get { return this.Sprites[0].Direction; }
            set { this.Sprites[0].Direction = value;}
        }
        public override int DirectionSpriteCount
        {
            get
            {
                return this.Sprites[0].DirectionCount;
            }
            //set
            //{
            //    base.DirectionSpriteCount = value;
            //}
        }

        public void GoToLogicalXY(float fX, float fY)
        {
            Vector2 v = new Vector2(fX - LogicalX, fY - LogicalY);
            Vector2 v2 = new Vector2(-Width / 2, -Height / 2);
            v = v + v2;
            float fLen = v.Length();
            v.Normalize();
            v2 = new Vector2(0, -1);
            double angle = Vector2.Dot(v, v2);            
            angle = Math.Acos(angle);            
            double c = v.X * v2.Y - v.Y * v2.X;
            if (c > 0) angle = MathHelper.TwoPi - angle;
            v *= GlobalClass.CharacterMovingSpeed;
            int iDir = (int)Math.Round(angle * DirectionSpriteCount / MathHelper.TwoPi);
            MotionModule.Stop();
            MotionModule = new MovingPlaneMotionModule(v.X, v.Y, fLen, this, iDir);
            MotionModule.Play();
            
        }
        //public void GoToPhysicalXY(float fX, float fY)
        //{
        //    Vector2 v = this.ConvertPhysical2Logical(new Vector2(fX, fY));
        //    GoToLogicalXY(v.X, v.Y);
        //}
    }
}
