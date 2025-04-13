using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Sprites;

namespace Project.Items
{
    public abstract class Item : IItem
    {
        public abstract Rectangle Location { get; set; }
        public virtual int PlayerHealthEffect { get => 2; }
        public bool IsPassable { get => true; }

        public abstract float Speed { get; set; }

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
            if (collider is Player)
            {
                //For now we will just set the location to a far away place
                //I need to figure out how to make an item delete itself without access to the item manager?
                Location = new Rectangle(1000, 1000, 1000, 1000);
            }
        }
    }
}
