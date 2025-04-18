using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.UI
{
    public class GameOverScreen : BasicScreen
    {
        public GameOverScreen(SpriteFont font, int screenWidth, int screenHeight)
            : base(font, screenWidth, screenHeight, "Game Over") { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
