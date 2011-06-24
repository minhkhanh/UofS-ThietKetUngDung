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
        protected CharacterStats _stats;

        public override GameObject Clone()
        {
            Character character = new Character();

            for (int i = 0; i < _sprites.Count; ++i)
                character._sprites.Add(new Sprite2D(_sprites[i]));

            character.GameObjectParent = this.GameObjectParent;
            character.LogicalX = this.LogicalX;
            character.LogicalY = this.LogicalY;
            character.UpdateChild();

            return character;
        }

        public int Direction
        {
            get { return this.Sprites[0].Direction; }
            set { this.Sprites[0].Direction = value;}
        }

        public void GoToLogicalXY(float fX, float fY)
        {
            Vector2 v = new Vector2(fX - LogicalX, fY - LogicalY);
            float fTime = v.Length() / GlobalClass.CharacterMovingSpeed;
            v.Normalize();
            v *= GlobalClass.CharacterMovingSpeed;
            MotionModule = new MovingPlaneMotionModule(v.X, v.Y, fTime);
            MotionModule.Play();
        }
    }
}
