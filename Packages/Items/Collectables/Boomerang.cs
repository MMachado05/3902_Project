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

        public override void Use()
        {
            if (projectiles.Count <= 1)
            {
                switch (Direction)
                {
                    case Direction.Up:
                        projectiles.Add(new ThrownBoomerang(Location, Direction, ItemFactory.Instance.CreateBoomerangSprite()));
                        break;
                    case Direction.Right:
                        projectiles.Add(new ThrownBoomerang(Location, Direction, ItemFactory.Instance.CreateBoomerangSprite()));
                        break;
                    case Direction.Left:
                        projectiles.Add(new ThrownBoomerang(Location, Direction, ItemFactory.Instance.CreateBoomerangSprite()));
                        break;
                    case Direction.Down:
                        projectiles.Add(new ThrownBoomerang(Location, Direction, ItemFactory.Instance.CreateBoomerangSprite()));
                        break;
                    default:
                        projectiles.Add(new ThrownBoomerang(Location, Direction, ItemFactory.Instance.CreateBoomerangSprite()));
                        break;
                }
            }
        }
    }
}
