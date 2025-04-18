using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters.Enums;

namespace Project.Sprites
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
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw this sprite to the game window.
        /// </summary>
        void Draw(SpriteBatch spriteBatch, Rectangle destination);
    }
}
