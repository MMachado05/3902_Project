using Microsoft.Xna.Framework;

namespace Project.Packages.Items
{
    public class Arrow : Item
    {
        public Arrow(ISprite isprite) : base(isprite)
        {
            Position = new Vector2(100, 50);
            Speed = 5;
        }
    }
}
