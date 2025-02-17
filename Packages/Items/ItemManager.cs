using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;

namespace Project.Packages.Items
{
    public class ItemManager
    {
        //ItemFactory itemFactory = new ItemFactory();
        private List<Item> itemList;
        int currentItemIndex = 0;

        public ItemManager()
        {
            itemList = new List<Item> { ItemFactory.Instance.createArrow(), ItemFactory.Instance.createHeart(), ItemFactory.Instance.createBomb()};
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
