using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.UI
{
    public class BasicScreen : IScreen
    {
        protected SpriteFont font;
        protected int screenWidth;
        protected int screenHeight;
        protected string title;
        protected Texture2D? backgroundTexture;

        public BasicScreen(SpriteFont font, int screenWidth, int screenHeight, string title, Texture2D? backgroundTexture = null)
        {
            this.font = font;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.title = title;
            this.backgroundTexture = backgroundTexture;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Vector2 titleSize = font.MeasureString(title);
            Vector2 titlePosition = new Vector2(
                (screenWidth - titleSize.X) / 2,
                (screenHeight - titleSize.Y) / 2
            );

            spriteBatch.DrawString(font, title, titlePosition, Color.White);
        }

        public virtual void Update()
        {
            // Optional for derived classes
        }

        public virtual GameStateAction HandleInput()
        {
            return GameStateAction.None;
        }
    }
}
