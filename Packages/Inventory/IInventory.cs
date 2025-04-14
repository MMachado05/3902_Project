using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Items;

namespace Project.Inventory
{
    public interface IInventory
    {
        List<(IItem,int)> Items{get; set;}
        int currentItemIndex { get; set; }
        bool Add(IItem item);
        bool Remove(IItem item);
        (IItem,int) GetCurrentItem();
        public void PlaceCurrentItem(SpriteBatch spriteBatch, Rectangle location);
        public void setIndex(int index);
    }
}