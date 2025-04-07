using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Sprites;

namespace Project.Items
{
    public abstract class Item : IItem
    {
        public abstract float Speed { get; set; }
        public abstract Rectangle Location { get; set; }

        public ISprite Sprite { get; }

        protected Item(ISprite sprite)
        {
            Sprite = sprite;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Location);
        }

        public virtual void CollideWith(IGameObject collider)
        {
            // TODO: Implement this
        }
    }
}
