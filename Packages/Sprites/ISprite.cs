using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public interface ISprite
    {
        CharacterState State
        {
            get; set;
        }
        /// <summary>
        /// Update the visual state of this Sprite.
        /// </summary>
        void Update();

        /// <summary>
        /// Draw this sprite to the game window.
        /// </summary>
        void Draw(SpriteBatch spriteBatch, Rectangle destination);
    }
}
