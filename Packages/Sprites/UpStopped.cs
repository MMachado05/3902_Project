using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public class UpStopped : ISprite
    {
        private readonly Texture2D _texture;
        private Rectangle SpriteDestinationRectangle; // Destination Rectangle
        private Rectangle SpriteSourceRectangle;
        GraphicsDeviceManager _graphics;

        private readonly int _frameWidth;
        private readonly int _frameHeight;

        public UpStopped(Texture2D texture, float printScale, GraphicsDeviceManager graphics)
        {
            _texture = texture;
            _graphics = graphics;

            // Width: 30, Height: 30
            _frameWidth = 30; // Width of a single animation frame (ADJUSTED FROM STATIC ONES)
            _frameHeight = 30; // Height of a single animation frame

            SpriteDestinationRectangle = new Rectangle((_graphics.PreferredBackBufferWidth / 2) - (16 * 3) / 2,
                                   (_graphics.PreferredBackBufferHeight / 2) - ((int)(32 * printScale)) / 2, (int)(16 * printScale), (int)(32 * printScale));
            SpriteSourceRectangle = new Rectangle(1, 1, _frameWidth, _frameHeight);
        }

        public void Update(GameTime gameTime)
        {
            // No update required for static sprites....i think
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, SpriteDestinationRectangle, SpriteSourceRectangle, Color.White);
        }
    }
}
