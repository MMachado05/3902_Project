using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public class DownWalk : ISprite
    {
        private Texture2D _texture; // Spritesheet

        private Rectangle SpriteDestinationRectangle; // Destination Rectangle
        private Rectangle SpriteSourceRectangle;

        GraphicsDeviceManager _graphics;

        private int _currentFrame;
        private readonly int _totalFrames;
        private readonly int _frameWidth;
        private readonly int _frameHeight;

        private float velocity = 5f; // Consider making this a const in Game1.cs, then passing to all moving sprites classes

        private readonly float _timePerFrame;
        private float _elapsedTime;

        public DownWalk(Texture2D texture, float printScale, GraphicsDeviceManager graphics, int frameCount, float animationSpeed)
        {
            _texture = texture;
            _graphics = graphics;

            _frameWidth = 30; // Width of a single animation frame (ADJUSTED FROM STATIC ONES)
            _frameHeight = 34; // Height of a single animation frame
            _totalFrames = frameCount; // Total number of animation frames
            _timePerFrame = 1f / animationSpeed; // Time per frame (in seconds)

            // Width: 16, Height: 32; set the destination rectangle to a fixed position
            SpriteDestinationRectangle = new Rectangle((_graphics.PreferredBackBufferWidth / 2) - (int)(_frameWidth * printScale) / 2,
                                   (_graphics.PreferredBackBufferHeight / 2) - ((int)(_frameHeight * printScale)) / 2, (int)(_frameWidth * printScale), (int)(_frameHeight * printScale));

            // Start from the first frame
            _currentFrame = 0;

            SpriteSourceRectangle = new Rectangle(232, 51, _frameWidth, _frameHeight); // Using the variables declared above
        }

        public void Update(GameTime gameTime)
        {
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_elapsedTime > _timePerFrame)
            {
                _currentFrame++;
                // Move to the next frame
                _currentFrame %= _totalFrames;

                // Update the source rectangle to reflect the current frame
                SpriteSourceRectangle.X = 232 + (_currentFrame * _frameWidth);

                _elapsedTime -= _timePerFrame;

            }

            SpriteDestinationRectangle.X += (int)velocity;
            if (SpriteDestinationRectangle.X > _graphics.PreferredBackBufferWidth)
            {
                SpriteDestinationRectangle.X = -50;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, SpriteDestinationRectangle, SpriteSourceRectangle, Color.White);
        }
    }
}
