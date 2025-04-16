using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
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
            Sword sword = new Sword(new Rectangle(1, 2, 3, 4), ItemFactory.Instance.CreateSwordSprite());
            sword.Equipped = true;
            this.Items = new List<(IItem, int)> { (sword, 1) };
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
            }
            else if (Items.Count<10){
                added = true;
                (IItem,int) targetPair = new (item,1);
                Items.Add(targetPair);
            }

            return added;
        }

        public (IItem,int) GetCurrentItem()
        {
            if (currentItemIndex >= Items.Count)
            {
                currentItemIndex = Items.Count - 1;
            }
            if (currentItemIndex < 0)
            {
                currentItemIndex = 0;
            }
            return Items[currentItemIndex];
        }

        public bool Remove(IItem item)
        {
            int itemCount;
            int counter = -1;
            bool found = false;
            bool removed = false;
            foreach (var pair in Items)
            {
                var temp = pair.Item1;
                counter++;
                if (temp == item)
                {
                    itemCount = pair.Item2;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                removed = true;
                var targetPair = Items.ElementAt(counter);
                Items.Remove(targetPair);
                targetPair.Item2--;
                Items.Add(targetPair);

                if (targetPair.Item2 == 0)
                {
                    Items.Remove(targetPair);
                }
            }
            return removed;
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
            IItem CurrentItem = GetCurrentItem().Item1;
            CurrentItem.Draw(spriteBatch);
            CurrentItem.Location = offsetLocation(location, direction);
            CurrentItem.Direction = direction;


            if (!CurrentItem.Equipped) {
                Remove(CurrentItem);
            }
            // For debugging Inventory
            //System.Diagnostics.Debug.WriteLine("\nCurent Inventory: ");
            //for (int i = 0; i < Items.Count; i++)
            //{
            //    System.Diagnostics.Debug.Write(" index: " + i + ": " + Items[i].Item1.GetType() + " count : " + Items[i].Item2 + " ");
            //}
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