using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public abstract class Item : IItem
    {
        public abstract float Speed { get; set; }
        public abstract Rectangle PositionRect { get; set; }

        public ISprite Sprite { get; }

        protected Item(ISprite sprite)
        {
            Sprite = sprite;
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, PositionRect);
        }

        public virtual void CollideWith(IGameObject collider)
        {
            // TODO: Implement this
        }
    }
}
