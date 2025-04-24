using Microsoft.Xna.Framework;
using Project.Characters;
using Project.Sprites;

namespace Project.Items
{
    public class ThrownBoomerang : ProjectileItem
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

        public override void Update(GameTime gameTime)
        {
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
        public override void CollideWith(IGameObject collider) { }
    }
}
