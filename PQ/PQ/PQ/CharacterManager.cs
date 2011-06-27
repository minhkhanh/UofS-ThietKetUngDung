using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PQ
{
    enum CharacterName
    {
        HeroKnightMale1,
        HeroKnightMale2
    }

    public class CharacterManager : AbstractManager
    {
        public override void LoadPrototypes(ContentManager content)
        {
            _prototypes = new Dictionary<int, object>();

            //////////////////////////////////////////////////////////////////////////
            // Portraits
            //////////////////////////////////////////////////////////////////////////

            Texture2D txt2dAvatar;
            Texture2D txt2dSprites;

            SplittingDetails details = new SplittingDetails(4, 8, 0, 0, 49, 49, 0, 0, 0, 0);

            Character character = new HeroKnightMale1();
            txt2dSprites = content.Load<Texture2D>(@"Images\MaleHeroKnight1_49x49");
            character.Sprites.Add(new Sprite2D(txt2dSprites, 0, 0, details, 8));

            txt2dAvatar = content.Load<Texture2D>(@"Images\Portraits\Portrait_PC_Knight0");
            character.Avatar = txt2dAvatar;

            character.UpdateChild();
            _prototypes.Add((int)CharacterName.HeroKnightMale1, character);

            txt2dSprites = content.Load<Texture2D>(@"Images\MaleHeroKnight2_49x49");
            character = new HeroKnightMale2();
            character.Sprites.Add(new Sprite2D(txt2dSprites, 0, 0, details, 8));

            txt2dAvatar = content.Load<Texture2D>(@"Images\Portraits\Portrait_PC_Knight1");
            character.Avatar = txt2dAvatar;

            character.UpdateChild();
            _prototypes.Add((int)CharacterName.HeroKnightMale2, character);
        }

        public override object CreateObject(int idx)
        {
            return (_prototypes[idx] as Character).Clone();
        }
    }
}
