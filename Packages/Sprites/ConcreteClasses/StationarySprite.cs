using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public class StationarySprite : AbstractSprite
    {
        public StationarySprite(Texture2D texture, Rectangle source, CharacterState state)
          : base(texture, source, state)
        {
        }

        public override void Update()
        {
            // Empty method, stationary sprite
        }
    }
}
