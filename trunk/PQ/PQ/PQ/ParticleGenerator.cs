using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//////////////////////////////////////////////////////////////////////////
// Source code from http://rbwhitaker.wikidot.com/2d-particle-engine-3
// Edited by 0812239, Nguyen Anh Khoi
//////////////////////////////////////////////////////////////////////////

namespace PQ
{
    public class ParticleGenerator
    {
        protected Random _rand;
        public Vector2 EmitterLocation { get; set; }
        protected List<Particle2D> _particles;
        protected List<Texture2D> _textures;

        protected int _ttl;

        public int Ttl
        {
            get { return _ttl; }
            set { _ttl = value; }
        }

        public ParticleGenerator(int ttl, List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this._textures = textures;
            this._particles = new List<Particle2D>();
            _rand = new Random();

            _ttl = ttl;
        }

        public void Update(GameTime gameTime)
        {
            _ttl -= 5;

            int total = 2;

            for (int i = 0; i < total; i++)
            {
                _particles.Add(GenerateNewParticle());
            }

            for (int particle = 0; particle < _particles.Count; particle++)
            {
                _particles[particle].Update(gameTime);
                if (_particles[particle].Ttl <= 0)
                {
                    _particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public virtual Particle2D GenerateNewParticle()
        {
            //Texture2D texture = textures[random.Next(textures.Count)];
            //Vector2 position = EmitterLocation;
            //Vector2 velocity = new Vector2(
            //                        1f * (float)(random.NextDouble() * 2 - 1),
            //                        1f * (float)(random.NextDouble() * 2 - 1));
            //float angle = 0;
            //float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            //Color color = new Color(
            //            (float)random.NextDouble(),
            //            (float)random.NextDouble(),
            //            (float)random.NextDouble());
            //float size = (float)random.NextDouble();
            //int ttl = 20 + random.Next(40);

            //return new Particle2D(texture, position, velocity, angle, angularVelocity, color, size, ttl);

            return null;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            for (int index = 0; index < _particles.Count; index++)
            {
                _particles[index].Draw(gameTime, spriteBatch);
            }
            //spriteBatch.End();
        }
    }
}