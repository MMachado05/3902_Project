using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public class StationaryItem : Item
    {
        public override Vector2 Position { get; set; }
        public override float Speed { get; set; }

        public StationaryItem(Vector2 position, float speed, ISprite sprite) : base(sprite)
        {
            Position = position;
            Speed = speed;
        }

        public override void Update() { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
