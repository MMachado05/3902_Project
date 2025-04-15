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
        public override Direction Direction { get; set; }
        public List<Arrow> projectiles = new List<Arrow>();
        public Bow(Rectangle position, ISprite sprite) : base(sprite)
        {
            Location = position;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Arrow arrow in projectiles)
            {
                arrow.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Arrow projectile in projectiles)
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
            switch (Direction)
            {
                case Direction.Up:
                    projectiles.Add(new Arrow(Location, Direction, ItemFactory.Instance.CreateUpArrowSprite()));
                    break;
                case Direction.Right:
                    projectiles.Add(new Arrow(Location, Direction, ItemFactory.Instance.CreateRightArrowSprite()));
                    break;
                case Direction.Left:
                    projectiles.Add(new Arrow(Location, Direction, ItemFactory.Instance.CreateLeftArrowSprite()));
                    break;
                case Direction.Down:
                    projectiles.Add(new Arrow(Location, Direction, ItemFactory.Instance.CreateDownArrowSprite()));
                    break;
                default:
                    projectiles.Add(new Arrow(Location, Direction, ItemFactory.Instance.CreateRightArrowSprite()));
                    break;
            }
        }
    }
}
