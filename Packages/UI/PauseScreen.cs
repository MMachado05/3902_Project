using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project.UI
{
    public class PauseScreen : BasicScreen
    {
        private Button resumeButton;
        private Button toggleMusicButton;

        private MouseState previousMouseState;


        public PauseScreen(SpriteFont font, int screenWidth, int screenHeight)
            : base(font, screenWidth, screenHeight, "Game is Paused")
        {
            int bw = 200, bh = 50;
            int centerX = (screenWidth - bw) / 2;
            int centerY = screenHeight / 2;

            resumeButton = new Button("Resume",
                new Rectangle(centerX, centerY, bw, bh),
                GameStateAction.TogglePause, font);

            toggleMusicButton = new Button("Toggle Music",
                new Rectangle(centerX, centerY + 70, bw, bh),
                GameStateAction.ToggleMusic, font);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 titleSize = font.MeasureString(title);
            Vector2 titlePosition = new Vector2(
                (screenWidth - titleSize.X) / 2,
                (screenHeight - titleSize.Y) / 2 - 100
            );

            spriteBatch.DrawString(font, title, titlePosition, Color.White);

            resumeButton.Draw(spriteBatch);
            toggleMusicButton.Draw(spriteBatch);
        }

        public override GameStateAction HandleInput()
        {
            MouseState currentMouse = Mouse.GetState();
            GameStateAction action = GameStateAction.None;

            if (resumeButton.IsClicked(currentMouse, previousMouseState))
                action = GameStateAction.TogglePause;
            else if (toggleMusicButton.IsClicked(currentMouse, previousMouseState))
                action = GameStateAction.ToggleMusic;

            previousMouseState = currentMouse;
            return action;
        }

    }
}
