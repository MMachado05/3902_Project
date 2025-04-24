using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project.UI
{
    public class GameOverScreen : BasicScreen
    {
        private Button restartButton;
        private Button exitButton;

        public GameOverScreen(SpriteFont font, int screenWidth, int screenHeight)
            : base(font, screenWidth, screenHeight, "Game Over")
        {
            int bw = 200, bh = 50;
            int centerX = (screenWidth - bw) / 2;
            int centerY = screenHeight / 2;

            restartButton = new Button("Restart",
                new Rectangle(centerX, centerY, bw, bh),
                GameStateAction.Restart, font);

            exitButton = new Button("Exit",
                new Rectangle(centerX, centerY + 70, bw, bh),
                GameStateAction.Exit, font);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 titleSize = font.MeasureString(title);
            Vector2 titlePosition = new Vector2(
                (screenWidth - titleSize.X) / 2,
                (screenHeight - titleSize.Y) / 2 - 100
            );

            spriteBatch.DrawString(font, title, titlePosition, Color.White);

            restartButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
        }

        public override GameStateAction HandleInput()
        {
            MouseState mouse = Mouse.GetState();
            if (restartButton.IsClicked(mouse)) return GameStateAction.Restart;
            if (exitButton.IsClicked(mouse)) return GameStateAction.Exit;
            return GameStateAction.None;
        }
    }
}
