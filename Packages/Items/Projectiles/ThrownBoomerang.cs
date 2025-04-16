using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Enemies.EnemyClasses;
using Project.Factories;
using Project.Rooms.Blocks.ConcreteClasses;
using Project.Sprites;

namespace Project.Items
{
    public class ThrownBoomerang : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }
        float TimeAlive;

        public ThrownBoomerang(Rectangle position, Direction direction, ISprite sprite) : base(sprite)
        {
            Location = position;
            Direction = direction;
            TimeAlive = 0;
        }

        public override void Update(GameTime gameTime) {
            TimeAlive += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (TimeAlive < .4)
            {
                switch (Direction)
                {
                    case Direction.Up:
                        Location = new Rectangle(Location.X, Location.Y - 10, Location.Width, Location.Height);
                        break;
                    case Direction.Down:
                        Location = new Rectangle(Location.X, Location.Y + 10, Location.Width, Location.Height);
                        break;
                    case Direction.Left:
                        Location = new Rectangle(Location.X - 10, Location.Y, Location.Width, Location.Height);
                        break;
                    case Direction.Right:
                        Location = new Rectangle(Location.X + 10, Location.Y, Location.Width, Location.Height);
                        break;
                    default:
                        Location = new Rectangle(Location.X + 10, Location.Y, Location.Width, Location.Height);
                        break;
                }
            }
            else
            {
                switch (Direction)
                {
                    case Direction.Up:
                        Location = new Rectangle(Location.X, Location.Y + 10, Location.Width, Location.Height);
                        break;
                    case Direction.Down:
                        Location = new Rectangle(Location.X, Location.Y - 10, Location.Width, Location.Height);
                        break;
                    case Direction.Left:
                        Location = new Rectangle(Location.X + 10, Location.Y, Location.Width, Location.Height);
                        break;
                    case Direction.Right:
                        Location = new Rectangle(Location.X - 10, Location.Y, Location.Width, Location.Height);
                        break;
                    default:
                        Location = new Rectangle(Location.X - 10, Location.Y, Location.Width, Location.Height);
                        break;
                }
            }
            if (TimeAlive > .8)
            {
                ToBeDeleted = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public override void CollideWith(IGameObject collider ){ }
    }
}
