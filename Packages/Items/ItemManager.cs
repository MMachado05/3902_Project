using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Packages.Items
{
    public class ItemManager
    {
        public List<Item> itemList;
        private List<Item> inventory;
        public int currentItemIndex = 0;

        public ItemManager()
        {
            
            inventory = new List<Item> { ItemFactory.Instance.createSword(), ItemFactory.Instance.createBomb(),ItemFactory.Instance.createBow()};
            //will replace with level maker
            itemList = new List<Item> {ItemFactory.Instance.createHeart(), ItemFactory.Instance.createCoin(), ItemFactory.Instance.createKey()};
        }
        public void nextItem()
        {
            currentItemIndex = (currentItemIndex + 1) % inventory.Count();
        }
        public void previousItem()
        {
            if (currentItemIndex > 0)
            {
                currentItemIndex--;
            }
            else
            {
                currentItemIndex = inventory.Count() - 1;
            }
        }
        public Item getCurrentItem()
        {
            return inventory[currentItemIndex];
        }
        
        public void removeItem(Item item)
        {
            itemList.Remove(item);
        }
    }
}
