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
                ItemFactory.Instance.CreateBow(new Vector2(100, 100)),
                ItemFactory.Instance.CreateArrow(new Vector2(100, 100))
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

        public void PlaceItem(int index, Vector2 position)
        {
            if (index < 0 || index >= inventory.Count) return; // Prevent out-of-bounds errors

            IItem itemToPlace = inventory[index];

            if (itemToPlace is ProjectileItem proj)
            {
                worldItems.Add(new ProjectileItem(position, proj.Direction, proj.Sprite, proj.Speed, 100));
            }
            else if (itemToPlace is StationaryItem stat)
            {
                worldItems.Add(new StationaryItem(position, stat.Speed, stat.Sprite));
            }
        }

    }
}
