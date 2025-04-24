using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Factories;
using Project.Items;

namespace Project.Inventory
{
    public class Inventory : IInventory
    {

        public Dictionary<IItem, int> Items { get; set; }
        private List<IItem> _itemsOrdered;
        public int ActiveSlot { get; set; }

        public Inventory()
        {
            ActiveSlot = 0;
            _itemsOrdered = new List<IItem>();
            Sword sword = new Sword(new Rectangle(1, 2, 3, 4), ItemFactory.Instance.CreateSwordSprite());
            sword.Equipped = true;
            this.Items = new Dictionary<IItem, int>();
            Items.Add(sword, 1);
            _itemsOrdered.Add(sword);
        }

        public bool Add(IItem item)
        {
            bool added = true;
            Type itemType = item.GetType();

            foreach (KeyValuePair<IItem, int> itemCount in Items)
            {
                if (itemCount.Key.GetType() == itemType)
                {
                    Items[itemCount.Key] += 2;
                    if (itemCount.Key is Bow brow)
                        brow.Count += 2;
                }
            }
            Items.Add(item, 2);
            _itemsOrdered.Add(item);
            if (item is Bow bow)
                bow.Count += 2;

            return added;
        }

        public bool Remove(IItem item)
        {
            if (!Items.ContainsKey(item))
                return false;

            Items[item]--;

            if (Items[item] == 0 && !(item is Bow))
            {
                Items.Remove(item);
                _itemsOrdered.Remove(item);
                ActiveSlot = 0;
            }
            else if (item is Bow)
            {
                Items[item]++;
            }
            return true;
        }

        public IItem GetCurrentItem()
        {
            return _itemsOrdered[ActiveSlot];
        }


        private Rectangle offsetLocation(Rectangle location, Direction direction)
        {
            Rectangle offset = new Rectangle(location.X, location.Y, location.Width, location.Height);
            switch (direction)
            {
                case Direction.Up:
                    offset.Y -= 25;
                    break;
                case Direction.Down:
                    offset.Y += 25;
                    break;
                case Direction.Left:
                    offset.X -= 25;
                    break;
                case Direction.Right:
                    offset.X += 25;
                    break;
            }
            return offset;
        }
        public void PlaceCurrentItem(SpriteBatch spriteBatch, Rectangle location, Direction direction)
        {
            IItem CurrentItem = _itemsOrdered[ActiveSlot];
            CurrentItem.Draw(spriteBatch);
            CurrentItem.Location = offsetLocation(location, direction);
            CurrentItem.Direction = direction;


            if (!CurrentItem.Equipped)
            {
                Remove(CurrentItem);
            }
        }

        public void setIndex(int index)
        {
            ActiveSlot = Math.Min(index, _itemsOrdered.Count);
        }
    }
}
