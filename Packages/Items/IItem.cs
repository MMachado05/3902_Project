using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public interface IItem
    {
        Vector2 Position { get; set; }
        float Speed { get; set; }
        /// <summary>
        /// Draw this item to the game window.
        /// </summary>
        void Draw(SpriteBatch spriteBatch);
    }
}
