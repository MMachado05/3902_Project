using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.UI
{
    public class GameOverScreen
    {
        private SpriteFont font;

        public GameOverScreen(SpriteFont font)
        {
            this.font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Game Over", new Vector2(360, 320), Color.White);
        }
    }
}
