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

        public void Shoot(Rectangle location, ISprite sprite, IEnumerable<Vector2> directions)
        {
            foreach (var direction in directions)
            {
                var projLocation = new Rectangle(location.X, location.Y, location.Width / 2, location.Height / 2);
                itemManager.AddProjectile(new ProjectileItem(projLocation, direction, sprite, speed, range, true, false));
            }
        }
    }
}
