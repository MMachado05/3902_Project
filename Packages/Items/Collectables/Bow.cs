using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Factories;
using Project.Sprites;

namespace Project.Items
{
    public class Bow : Item
    {
        public override Rectangle Location { get; set; }
        private List<ProjectileItem> projectiles = new List<ProjectileItem>();
        public Bow(Rectangle position, ISprite sprite) : base(sprite)
        {
            Location = position;
        }

        public override void Update(GameTime gameTime) {
            foreach (ProjectileItem projectile in projectiles) {
                projectile.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (ProjectileItem projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
        public override void CollideWith(IGameObject collider)
        {
            if (collider is Player)
            {
                ToBeDeleted = true;
            }
        }
        
        public override void Use()
        {
            //make direction later
            System.Diagnostics.Debug.Write("arrow!");
            projectiles.Add(new ProjectileItem(Location, new Vector2(0,1), ItemFactory.Instance.CreateArrowSprite(), 150.0f));
        }
    }
}
