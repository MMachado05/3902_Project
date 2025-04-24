using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;

namespace Project.Items
{
    public interface IItem : IGameObject
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        virtual void Use(ItemManager itemManager) { }
        public Direction Direction { get; set; }
        public bool ToBeDeleted { get; set; }
        public bool Equipped { get; set; }
    }
}
