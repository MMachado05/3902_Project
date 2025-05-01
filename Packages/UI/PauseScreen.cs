using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Inventory;
using Project.Renderer;

namespace Project.UI
{
    public class PauseScreen : BasicScreen
    {
        private Button resumeButton;
        private Button toggleMusicButton;
        private MouseState previousMouseState;
        private IInventory inventory;

        private readonly MapRenderer _mapRenderer;

        public PauseScreen(SpriteFont font, int screenWidth, int screenHeight, IInventory inventory, MapRenderer mapRenderer)
            : base(font, screenWidth, screenHeight, "Game is Paused")
        {
            this.inventory = inventory;
            this._mapRenderer = mapRenderer;

            int bw = 200, bh = 50;
            int centerX = (screenWidth - bw) / 2;

            // Adjusted Y positions
            int titleY = 80;
            int resumeY = titleY + 80;
            int toggleY = resumeY + 70;
            int inventoryY = toggleY + 90;

            // Map position at bottom
            int mapSize = 32 * 10;
            int mapY = screenHeight - mapSize - 40;
            _mapRenderer.SetScale(10f);
            _mapRenderer.SetPosition(new Vector2((screenWidth - mapSize) / 2 + 60, mapY));

            resumeButton = new Button("Resume",
                new Rectangle(centerX, resumeY, bw, bh),
                GameStateAction.TogglePause, font);

            toggleMusicButton = new Button("Toggle Music",
                new Rectangle(centerX, toggleY, bw, bh),
                GameStateAction.ToggleMusic, font);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Title
            Vector2 titleSize = font.MeasureString(title);
            spriteBatch.DrawString(font, title, new Vector2((screenWidth - titleSize.X) / 2, 80), Color.White);

            // Buttons
            resumeButton.Draw(spriteBatch);
            toggleMusicButton.Draw(spriteBatch);

            // Inventory
            if (inventory?.Items != null)
            {
                int itemSize = 48;
                int padding = 10;
                int startY = toggleMusicButton.Bounds.Bottom + 40;
                int startX = (screenWidth - (itemSize + padding) * inventory.Items.Count) / 2;

                int index = 0;
                foreach (var kvp in inventory.Items)
                {
                    var item = kvp.Key;
                    int quantity = kvp.Value;

                    var drawRect = new Rectangle(startX + index * (itemSize + padding), startY, itemSize, itemSize);
                    item.Location = drawRect;
                    item.Draw(spriteBatch);

                    spriteBatch.DrawString(font, $"x{quantity}", new Vector2(drawRect.X, drawRect.Y + itemSize + 2), Color.White);

                    if (index == inventory.ActiveSlot)
                    {
                        spriteBatch.DrawString(font, "x", new Vector2(drawRect.X + itemSize / 2 - 8, drawRect.Y - 20), Color.Yellow);
                    }

                    index++;
                }
            }

            // Map last to stay at the bottom
            _mapRenderer?.Draw(spriteBatch);
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

        public void SetMapRoomIndex(int index)
        {
            _mapRenderer?.SetRoomIndex(index);
        }
    }
}
