using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public class Item : IItem
    {
        public Vector2 Position { get; set; }
        public float Speed { get; set; }

        public ISprite isprite;

        public Item(ISprite isprite)
        {
            this.isprite = isprite;
        }
        public void Update()
        {
            Position = new Vector2(Position.X + Speed, Position.Y);
            if (Position.X > 800 || Position.X < 0)
            {
                bounceOffWall();
            }
        }

        void bounceOffWall()
        {
            Speed = -Speed;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            isprite.Draw(spriteBatch, Position);
        }

    }
}
