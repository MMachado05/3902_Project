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
            restartButton = new Button("Restart",
                new Rectangle((screenWidth - bw) / 2, screenHeight / 2 + 60, bw, bh),
                GameOverAction.Restart, font);

            exitButton = new Button("Exit",
                new Rectangle((screenWidth - bw) / 2, screenHeight / 2 + 130, bw, bh),
                GameOverAction.Exit, font);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            restartButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
        }

        public override GameOverAction HandleInput()
        {
            MouseState mouse = Mouse.GetState();
            if (restartButton.IsClicked(mouse)) return GameOverAction.Restart;
            if (exitButton.IsClicked(mouse)) return GameOverAction.Exit;
            return GameOverAction.None;
        }
    }
}
