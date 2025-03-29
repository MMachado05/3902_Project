using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Sprites;

namespace Project.Items
{
    public class StationaryItem : Item
    {
        public override Rectangle Location { get; set; }
        public override float Speed { get; set; }

        public StationaryItem(Rectangle position, float speed, ISprite sprite) : base(sprite)
        {
            Location = position;
            Speed = speed;
        }

        public override void Update() { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
