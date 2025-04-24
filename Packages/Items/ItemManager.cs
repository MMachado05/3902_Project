using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Project.Items
{
    public class ItemManager
    {
        private readonly List<Item> worldItems;
        private readonly List<ProjectileItem> projectiles;
        private int currentItemIndex = 0;

        public ItemManager()
        {
            projectiles = new List<ProjectileItem>();
            worldItems = new List<Item>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Item item in worldItems)
            {
                if (item.ToBeDeleted)
                {
                    worldItems.Remove(item);
                    break;
                }
            }

            foreach (ProjectileItem projectile in projectiles)
            {
                projectile.Update(gameTime);
                if (projectile.HasReturned() || projectile.ToBeDeleted)
                {
                    projectiles.Remove(projectile);
                    break;
                }
            }
        }
        public List<Item> GetWorldItems() => worldItems;
        public List<ProjectileItem> GetProjectiles => projectiles;

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
