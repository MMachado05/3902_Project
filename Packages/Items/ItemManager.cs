using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project.Items
{
    public class ItemManager
    {
        private readonly List<Item> worldItems;
        private readonly List<IItem> projectiles;
        private int currentItemIndex = 0;

        public ItemManager()
        {
            projectiles = new List<IItem>();
            worldItems = new List<Item>();
        }

        public void Update()
        {
            foreach (Item item in worldItems)
            {
                if (item.ToBeDeleted)
                {
                    worldItems.Remove(item);
                    break;
                }
            }
        }
        public List<Item> GetWorldItems() => worldItems;

        public void AddProjectile(ProjectileItem projectile)
        {
            this.projectiles.Add(projectile);
        }
        public void RemoveProjectile(ProjectileItem projectile)
        {
            this.projectiles.Remove(projectile);
        }

        public void addItem(Item item)
        {
            worldItems.Add(item);
        }
        public void removeItem(Item item)
        {
            worldItems.Remove(item);
        }
    }
}
