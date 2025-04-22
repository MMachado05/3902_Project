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
    public class Arrow : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }
        public Arrow(Rectangle position, Direction direction, ISprite sprite) : base(sprite)
        {
            Location = position;
            Direction = direction;
        }

        public override void Update(GameTime gameTime) {
            switch (Direction)
            {
                case Direction.Up:
                    Location = new Rectangle(Location.X, Location.Y - 5, Location.Width, Location.Height);
                    break;
                case Direction.Down:
                    Location = new Rectangle(Location.X, Location.Y + 5, Location.Width, Location.Height);
                    break;
                case Direction.Left:
                    Location = new Rectangle(Location.X - 5, Location.Y, Location.Width, Location.Height);
                    break;
                case Direction.Right:
                    Location = new Rectangle(Location.X + 5, Location.Y, Location.Width, Location.Height);
                    break;
                default:
                    Location = new Rectangle(Location.X + 5, Location.Y, Location.Width, Location.Height);
                    break;
            }
            
        }
        public override void CollideWith(IGameObject collider)
        {
            if (collider is Enemy || collider is SolidBlock)
            {
                ToBeDeleted = true;
            }
        }
    }
}
