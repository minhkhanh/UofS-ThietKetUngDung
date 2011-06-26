using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    public class ParticleEngine
    {
        List<ParticleGenerator> _listEngine = new List<ParticleGenerator>();
        public List<ParticleGenerator> Generators
        {
            get { return _listEngine; }
            set { _listEngine = value; }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _listEngine.Count; ++i)
            {
                _listEngine[i].Update(gameTime);
                if (_listEngine[i].Ttl <= 0)
                {
                    _listEngine.RemoveAt(i);
                    --i;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (ParticleGenerator i in _listEngine)
            {
                i.Draw(gameTime, spriteBatch);
            }
        }
    }
}
