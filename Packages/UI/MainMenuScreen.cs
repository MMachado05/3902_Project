using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project.UI
{
    public class MainMenuScreen : BasicScreen
    {
        private Button startGameButton;

        public MainMenuScreen(SpriteFont font, int screenWidth, int screenHeight)
            : base(font, screenWidth, screenHeight, "")
        {
            int bw = 200, bh = 50;
            int centerX = (screenWidth - bw) / 2;
            int centerY = screenHeight / 2;

            startGameButton = new Button("Start Game",
                new Rectangle(centerX, centerY, bw, bh),
                GameStateAction.StartGame, font);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 titleSize = font.MeasureString(title);
            Vector2 titlePosition = new Vector2(
                (screenWidth - titleSize.X) / 2,
                (screenHeight - titleSize.Y) / 2 - 100
            );

            spriteBatch.DrawString(font, title, titlePosition, Color.White);

            startGameButton.Draw(spriteBatch);
        }

        public override GameStateAction HandleInput()
        {
            MouseState mouse = Mouse.GetState();
            if (startGameButton.IsClicked(mouse)) return GameStateAction.StartGame;
            return GameStateAction.None;
        }
    }
}
