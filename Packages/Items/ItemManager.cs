using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Packages.Items
{
    public class ItemManager
    {
        //ItemFactory itemFactory = new ItemFactory();
        private List<Item> itemList;
        public int currentItemIndex = 0;

        public ItemManager()
        {
            itemList = new List<Item> { ItemFactory.Instance.createArrow(), ItemFactory.Instance.createHeart(), ItemFactory.Instance.createBomb(), ItemFactory.Instance.createSword(), ItemFactory.Instance.createBow()};
        }
        public void nextItem()
        {
            currentItemIndex = (currentItemIndex + 1) % itemList.Count();
        }
        public void previousItem()
        {
            if (currentItemIndex > 0)
            {
                currentItemIndex--;
            }
            else
            {
                currentItemIndex = itemList.Count() - 1;
            }
        }
        public Item getCurrentItem()
        {
            return itemList[currentItemIndex];
        }
    }
}
