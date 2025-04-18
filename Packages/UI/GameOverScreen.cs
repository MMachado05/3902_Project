using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.UI
{
    public class GameOverScreen
    {
        private SpriteFont font;
        private int screenWidth;
        private int screenHeight;

        public GameOverScreen(SpriteFont font, int screenWidth, int screenHeight)
        {
            this.font = font;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string title = "Game Over";
            Vector2 titleSize = font.MeasureString(title);
            Vector2 titlePosition = new Vector2(
                (screenWidth - titleSize.X) / 2,
                (screenHeight - titleSize.Y) / 2
            );

            spriteBatch.DrawString(font, title, titlePosition, Color.White);
        }
    }
}
