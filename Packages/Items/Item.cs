using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public abstract class Item : IItem
    {
        public abstract Vector2 Position { get; set; }
        public abstract float Speed { get; set; }
        protected ISprite Sprite { get; }

        protected Item(ISprite sprite)
        {
            Sprite = sprite;
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position);
        }
    }
}
