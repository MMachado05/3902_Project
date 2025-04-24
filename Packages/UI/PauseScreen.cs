using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Inventory;

namespace Project.UI
{
    public class PauseScreen : BasicScreen
    {
        private Button resumeButton;
        private Button toggleMusicButton;

        private MouseState previousMouseState;

        private IInventory inventory;

        public PauseScreen(SpriteFont font, int screenWidth, int screenHeight, IInventory inventory)
            : base(font, screenWidth, screenHeight, "Game is Paused")
        {
            this.inventory = inventory;

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

            if (inventory != null && inventory.Items != null)
            {
                int itemSize = 48;
                int padding = 10;
                int startX = (screenWidth - (itemSize + padding) * inventory.Items.Count) / 2;
                int startY = screenHeight - 100;

                int index = 0;
                foreach (var kvp in inventory.Items)
                {
                    var item = kvp.Key;
                    var quantity = kvp.Value;

                    // Positioning
                    var drawRect = new Rectangle(startX + index * (itemSize + padding), startY, itemSize, itemSize);

                    item.Location = drawRect;
                    item.Draw(spriteBatch);

                    // Quantity display
                    spriteBatch.DrawString(font, $"x{quantity}", new Vector2(drawRect.X, drawRect.Y + itemSize + 2), Color.White);

                    // Highlight active slot
                    if (index == inventory.ActiveSlot)
                    {
                        spriteBatch.DrawString(font, "x", new Vector2(drawRect.X + itemSize / 2 - 8, drawRect.Y - 20), Color.Yellow);
                    }

                    index++;
                }
            }

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
