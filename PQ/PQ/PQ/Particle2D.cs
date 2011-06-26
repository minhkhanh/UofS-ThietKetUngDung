using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

//////////////////////////////////////////////////////////////////////////
// Source code from http://rbwhitaker.wikidot.com/2d-particle-engine-2
// Edited by 0812239, Nguyen Anh Khoi
//////////////////////////////////////////////////////////////////////////

namespace PQ
{
    public class Particle2D
    {
        Texture2D _txt2dImg;

        public Texture2D Image
        {
            get { return _txt2dImg; }
            set { _txt2dImg = value; }
        }

        Vector2 _pos;

        public Vector2 Position
        {
            get { return _pos; }
            set { _pos = value; }
        }

        Vector2 _vel;

        public Vector2 Velocity
        {
            get { return _vel; }
            set { _vel = value; }
        }

        float _angle;

        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        float _angVel;

        public float AngularVelocity
        {
            get { return _angVel; }
            set { _angVel = value; }
        }

        Color _color;

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        float _size;

        public float Size
        {
            get { return _size; }
            set { _size = value; }
        }

        int _ttl;
        public int Ttl
        {
            get { return _ttl; }
            set { _ttl = value; }
        }

        public Particle2D(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Color color, float size, int ttl)
        {
            _txt2dImg = texture;
            _pos = position;
            _vel = velocity;
            _angle = angle;
            _angVel = angularVelocity;
            _color = color;
            _size = size;
            _ttl = ttl;
        }

        public void Update(GameTime gameTime)
        {
            _ttl -= 5;
            _pos += Velocity;
            _angle += AngularVelocity;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Image.Width, Image.Height);
            Vector2 origin = new Vector2(Image.Width / 2, Image.Height / 2);

            spriteBatch.Draw(Image, Position, sourceRectangle, Color,
                Angle, origin, Size, SpriteEffects.None, 0f);
        }
    }
}