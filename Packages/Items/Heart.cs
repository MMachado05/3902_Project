using Microsoft.Xna.Framework;

namespace Project.Packages.Items
{
    public class Heart : Item
    {
        public Heart(ISprite isprite) : base(isprite)
        {
            Position = new Vector2(50, 50);
            Speed = 0;
        }
    }
}
