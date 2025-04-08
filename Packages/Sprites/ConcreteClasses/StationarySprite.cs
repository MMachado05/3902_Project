using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters.Enums;

namespace Project.Sprites.ConcreteClasses
{
    public class StationarySprite : AbstractSprite
    {
        public StationarySprite(Texture2D texture, Rectangle source, CharacterState state)
          : base(texture, source, state)
        {
        }

        public override void Update(GameTime gameTime)
        {
            // Empty method, stationary sprite
        }
    }
}
