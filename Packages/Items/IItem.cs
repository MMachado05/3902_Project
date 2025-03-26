using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public interface IItem
    {
        float Speed { get; set; }
        void Update();
        void Draw(SpriteBatch spriteBatch, Rectangle dest);
    }
}
