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

namespace Project.Packages.Items
{
    public class ItemManager
    {
        //ItemFactory itemFactory = new ItemFactory();
        private List<Item> itemList;
        int currentItemIndex = 0;

        public ItemManager()
        {
            itemList = new List<Item> { ItemFactory.Instance.createArrow(), ItemFactory.Instance.createHeart() };
        }

        void Update()
        {

        }

        public void SwitchItem()
        {
            //switches between 1 and 0 for now
            currentItemIndex = 1 - currentItemIndex;
        }
        public Item getCurrentItem()
        {
            return itemList[currentItemIndex];
        }
    }
}
