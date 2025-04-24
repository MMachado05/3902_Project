using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Factories;
using Project.Packages.Sounds;
using Project.Sprites;

namespace Project.Items
{
    public class Boomerang : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }
        public List<ThrownBoomerang> projectiles = new List<ThrownBoomerang>();
        public Boomerang(Rectangle position, ISprite sprite) : base(sprite)
        {
            Location = position;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (ThrownBoomerang boomerang in projectiles)
            {
                boomerang.Update(gameTime);
                if (boomerang.ToBeDeleted)
                {
                    projectiles.Remove(boomerang);
                    break;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (ThrownBoomerang projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
        public override void CollideWith(IGameObject collider)
        {
            if (collider is Player && !Equipped)
            {
                ToBeDeleted = true;
            }
        }

        public override void Use(ItemManager itemManager)
        {
            SoundEffectManager.Instance.playBoomerang();
            switch (Direction)
            {
                case Direction.Up:
                    itemManager.AddProjectile(new ThrownBoomerang(Location, new Vector2(0, -5), 5f, ItemFactory.Instance.CreateBoomerangSprite(), true, true));
                    break;
                case Direction.Right:
                    itemManager.AddProjectile(new ThrownBoomerang(Location, new Vector2(5, 0), 5f, ItemFactory.Instance.CreateBoomerangSprite(), true, true));
                    break;
                case Direction.Left:
                    itemManager.AddProjectile(new ThrownBoomerang(Location, new Vector2(-5, 0), 5f, ItemFactory.Instance.CreateBoomerangSprite(), true, true));
                    break;
                case Direction.Down:
                    itemManager.AddProjectile(new ThrownBoomerang(Location, new Vector2(0, 5), 5f, ItemFactory.Instance.CreateBoomerangSprite(), true, true));
                    break;
                default:
                    break;
            }
        }
    }
}
