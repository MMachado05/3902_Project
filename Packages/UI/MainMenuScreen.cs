using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Packages.Sounds;

namespace Project.UI
{
    public class MainMenuScreen : BasicScreen
    {
        private Button startGameButton;
        private Button exitButton;

        public MainMenuScreen(SpriteFont font, int screenWidth, int screenHeight, Texture2D backgroundTexture)
            : base(font, screenWidth, screenHeight, "", backgroundTexture)
        {
            // Play sound immediately
            SoundEffectManager.Instance.PlayMenuMusic();

            int bw = 200, bh = 50;
            int centerX = (screenWidth - bw) / 2;
            int centerY = screenHeight / 2;

            startGameButton = new Button("Begin Journey",
                new Rectangle(centerX, centerY, bw, bh),
                GameStateAction.StartGame, font);

            exitButton = new Button("Return Home",
                new Rectangle(centerX, centerY + 70, bw, bh),
                GameStateAction.Exit, font);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

            Vector2 titleSize = font.MeasureString(title);
            Vector2 titlePosition = new Vector2(
                (screenWidth - titleSize.X) / 2,
                (screenHeight - titleSize.Y) / 2 - 100
            );

            spriteBatch.DrawString(font, title, titlePosition, Color.White);

            startGameButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
        }

        public override GameStateAction HandleInput()
        {
            MouseState mouse = Mouse.GetState();
            if (startGameButton.IsClicked(mouse)) return GameStateAction.StartGame;
            if (exitButton.IsClicked(mouse)) return GameStateAction.Exit;
            return GameStateAction.None;

            
        }
    }
}
