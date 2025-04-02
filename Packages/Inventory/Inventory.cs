using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Project.Inventory;
using Project.Items;

namespace Project.Inventory
{
    public class Inventory : IInventory
    {

        public List<(IItem, int)> Items { get; set; }


        public Inventory()
        {
            this.Items = new List<(IItem, int)>();
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

        public IItem GetItem()
        {
            throw new NotImplementedException();
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
    }
}