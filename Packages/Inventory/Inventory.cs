using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Factories;
using Project.Inventory;
using Project.Items;

namespace Project.Inventory
{
    public class Inventory : IInventory
    {

        public List<(IItem, int)> Items { get; set; }
        public int currentItemIndex { get; set; }

        public Inventory()
        {
            currentItemIndex = 0;
            this.Items = new List<(IItem, int)> { (new Sword(new Rectangle(1, 2, 3, 4), ItemFactory.Instance.CreateSwordSprite()), 1) };
        }

        public bool Add(IItem item)
        {
            int itemCount;
            int counter = 0;
            bool found = false;
            bool added = false;
            foreach (var pair in Items)
            {
                var temp = pair.Item1;
                if (temp == item)
                {
                    itemCount = pair.Item2;
                    found = true;
                }
                counter++;
            }
            if (found)
            {
                added = true;
                var targetPair = Items.ElementAt(counter);
                Items.Remove(targetPair);
                targetPair.Item2++;
                                Items.Add(targetPair);


            }else if (Items.Count<10){
                added = true;
                (IItem,int) targetPair = new (item,1);
                Items.Add(targetPair);


            }

            return added;
        }

        public (IItem,int) GetCurrentItem()
        { 
            return Items[currentItemIndex];
        }

        public bool Remove(IItem item)
        {
            int itemCount;
            int counter = 0;
            bool found = false;
            bool removed = false;
            foreach (var pair in Items)
            {
                var temp = pair.Item1;
                if (temp == item)
                {
                    itemCount = pair.Item2;
                    found = true;
                }
                counter++;
            }
            if (found)
            {
                removed = true;
                var targetPair = Items.ElementAt(counter);
                Items.Remove(targetPair);
                targetPair.Item2--;
                Items.Add(targetPair);

            }
            return removed;
        }

        public void PlaceCurrentItem(SpriteBatch spriteBatch, Rectangle location)
        {
            Items[currentItemIndex].Item1.Draw(spriteBatch);
            Items[currentItemIndex].Item1.Location = location;

            
            //for (int i = 0; i<Items.Count; i++)
            //{
            //    System.Diagnostics.Debug.Write(i + " " + Items[i].Item1.GetType());
            //}
            //System.Diagnostics.Debug.WriteLine("\nindex: " + currentItemIndex + "Current Inventory:");
        }
        
        public void setIndex(int index)
        {
            if (Items.Count > index)
            {
                currentItemIndex = index;
            }
        }
    }
}