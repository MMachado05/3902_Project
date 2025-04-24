using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Sprites;

namespace Project.Items
{
    public abstract class Item : IItem
    {
        public abstract Rectangle Location { get; set; }
        public abstract Direction Direction { get; set; }
        public virtual int PlayerHealthEffect { get; set; }
        public bool IsPassable { get => true; }
        public bool ToBeDeleted { get; set; }
        public bool Equipped { get; set; }
        public ISprite Sprite { get; }

        protected Item(ISprite sprite)
        {
            Sprite = sprite;
            ToBeDeleted = false;
            Equipped = false;
        }

        public virtual void Use(ItemManager itemManager) { }
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
                //Location = new Rectangle(1000, 1000, 1000, 1000);
            }
        }
    }
}
