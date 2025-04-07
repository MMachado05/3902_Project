using Microsoft.Xna.Framework;
using Project.Sprites;

namespace Project.Items
{
    public class BasicAttack : Item
    {
        public BasicAttack(ISprite sprite) : base(sprite)
        {
        }

        public override float Speed { get; set; }
        public override Rectangle Location { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Sprite.Update(gameTime);
        }
    }
}
