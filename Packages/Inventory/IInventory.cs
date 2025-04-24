using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Items;

namespace Project.Inventory
{
    public interface IInventory
    {
        int ActiveSlot { get; set; }
        bool Add(IItem item);
        bool Remove(IItem item);
        IItem GetCurrentItem();
        public void PlaceCurrentItem(SpriteBatch spriteBatch, Rectangle location, Direction direction);
        public void setIndex(int index);

    }
}
