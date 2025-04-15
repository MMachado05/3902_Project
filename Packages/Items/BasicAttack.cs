using Microsoft.Xna.Framework;
using Project.Characters;
using Project.Sprites;

namespace Project.Items
{
    public class BasicAttack : Item
    {
        public BasicAttack(ISprite sprite, Rectangle location) : base(sprite)
        {
            Location = location;
        }

        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Sprite.Update(gameTime);
        }
    }
}
