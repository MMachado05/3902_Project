using Microsoft.Xna.Framework;

namespace Project.Packages.Items
{
    public class Bomb : Item
    {
        public Bomb(ISprite isprite) : base(isprite)
        {
            Position = new Vector2(400, 100);
        }
    }
}
