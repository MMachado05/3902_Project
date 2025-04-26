using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Items;
using Project.Sprites;

namespace Project.Enemies.Helper
{
    public class ProjectileShooter
    {
        private readonly ItemManager itemManager;
        private readonly float speed;
        private readonly float range;

        public ProjectileShooter(ItemManager itemManager, float projectileSpeed = 5f, float projectileRange = 600f)
        {
            this.itemManager = itemManager;
            this.speed = projectileSpeed;
            this.range = projectileRange;
        }

        public List<ProjectileItem> Shoot(Rectangle origin, ISprite sprite, IEnumerable<Vector2> rawDirections)
        {
            List<ProjectileItem> projectiles = new();
            foreach (var rawDirection in rawDirections)
            {
                if (rawDirection == Vector2.Zero) continue;
                Vector2 direction = Vector2.Normalize(rawDirection);

                Rectangle projectileLocation = new Rectangle(
                    origin.Center.X - origin.Width / 4,
                    origin.Center.Y - origin.Height / 4,
                    origin.Width / 2,
                    origin.Height / 2
                );

                ProjectileItem projectile = new ProjectileItem(
                    projectileLocation,
                    direction,
                    sprite,
                    speed,
                    range,
                    true,
                    false
                );

                itemManager.AddProjectile(projectile);
                projectiles.Add(projectile);
            }
            return projectiles;
        }
    }
}
