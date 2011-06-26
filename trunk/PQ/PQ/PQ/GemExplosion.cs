using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PQ
{
    public class GemExplosion: ParticleGenerator
    {
        Color _colorGem;
        public GemExplosion(Gem gem)
            : base(100, (gem.Parent.Game.SpriteManager.CreateObject((int)Sprite2DName.Sparkle) as Sprite2D).Frames, new Vector2(gem.Bounds.Center.X, gem.Bounds.Center.Y))
        {
            _colorGem = gem.ColorState.Color;
            _colorGem.A = 0;
        }

        public override Particle2D GenerateNewParticle()
        {
            Texture2D texture = _textures[_rand.Next(_textures.Count)];
            Vector2 position = EmitterLocation + new Vector2(_rand.Next(100) - 50, _rand.Next(100) - 50);
            
            float size = (float)_rand.NextDouble();
            int ttl = 60;

            return new Particle2D(texture, position, Vector2.Zero, 0, 0, _colorGem, size, ttl);
        }
    }
}
