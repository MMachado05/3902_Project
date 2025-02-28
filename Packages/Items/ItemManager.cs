using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project.Packages.Items
{
    public class ItemManager
    {
        private readonly List<IItem> inventory;
        private readonly List<IItem> worldItems;
        private int currentItemIndex = 0;

        public ItemManager()
        {
            inventory = new List<IItem>
            {
                ItemFactory.Instance.CreateSword(new Vector2(100, 100)),
                ItemFactory.Instance.CreateBomb(new Vector2(100, 100)),
                ItemFactory.Instance.CreateBow(new Vector2(100, 100))
            };

            worldItems = new List<IItem>
            {
                ItemFactory.Instance.CreateHeart(new Vector2(200, 300)),
                ItemFactory.Instance.CreateCoin(new Vector2(450, 200)),
                ItemFactory.Instance.CreateKey(new Vector2(700, 250))
            };
        }

        public void NextItem()
        {
            currentItemIndex = (currentItemIndex + 1) % inventory.Count;
        }

        public void PreviousItem()
        {
            currentItemIndex = (currentItemIndex - 1 + inventory.Count) % inventory.Count;
        }

        public IItem GetCurrentItem()
        {
            return inventory[currentItemIndex];
        }

        public List<IItem> GetWorldItems()
        {
            return worldItems;
        }

        public void SelectItem(int index)
        {
            if (index >= 0 && index < inventory.Count)
            {
                currentItemIndex = index;
            }
        }

    }
}
