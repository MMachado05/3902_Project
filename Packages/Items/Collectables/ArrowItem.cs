using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Inventory;
using Project.Packages.Sounds;
using Project.Sprites;

namespace Project.Items
{
    public class ArrowItem : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }

        public ArrowItem(Rectangle location, ISprite sprite) : base(sprite)
        {
            this.Location = location;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void CollideWith(IGameObject collider, Vector2 from)
        {
            if (collider is Player && !Equipped)
            {
                ToBeDeleted = true;
            }
        }

    }
}
