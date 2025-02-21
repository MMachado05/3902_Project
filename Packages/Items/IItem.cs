using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{  
    public interface IItem
    {
        Vector2 Position { get; set; }
        float Speed { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}