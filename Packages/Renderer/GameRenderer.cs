using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using Project.Characters;
using Project.Commands;
using Project.Commands.PlayerCommands;
using Project.Factories;
using Project.Packages.Sounds;
using Project.Rooms;

namespace Project.Renderer
{
    public class GameRenderer : IRenderer
    {
        private Player _playerCharacter;
        public Player PlayerCharacter
        {
            set => this._playerCharacter = value;
        }

        private RoomManager _roomManager;
        public RoomManager RoomManager
        {
            set => this._roomManager = value;
        }

        private bool _fieldsSatisfied;
        private GameStateMachine _gameState;

        // Osama: exposing public property of map sprite sheet to most simply render them here.

        private int _screenWidth; // Since we can't see game1
        private int _screenHeight; // Since we can't see game1
        private int tileWidth;
        private int tileHeight;

        public int TileWidth
        {
            get => tileWidth;
        }
        public int TileHeight
        {
            get => tileHeight;
        }

        // Myra UI components for the pause menu
        private Desktop _pauseDesktop;
        private VerticalStackPanel mainStack;
        private Panel _pausePanel;
        private VerticalStackPanel stackPanel;
        private Label _pauseLabel;
        private Label _inventoryPanel;
        private Button _musicToggleButton;
        private ICommand _toggleMusicCommand;
        private MapRenderer _mapRenderer;


        public GameRenderer(
            int screenWidth,
            int screenHeight,
            int tileWidth,
            int tileHeight,
            GameStateMachine gameState
        )
        {
            //TODO: adjust naming convention for some of these
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this._fieldsSatisfied = false;
            this._gameState = gameState;
            InitializePauseMenuUI();
        }

        public void SetMapRenderer(MapRenderer mapRenderer)
        {
            _mapRenderer = mapRenderer;
        }

        public void SetMapRoomIndex(int index)
        {
            _mapRenderer?.SetRoomIndex(index);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_fieldsSatisfied)
                SatisfyFields();

            // Draw room & player with vertical offset
            spriteBatch.End();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(0, 96, 0));
            _roomManager.DrawCurrentRoom(spriteBatch);
            _playerCharacter.Draw(spriteBatch);
            spriteBatch.End();

            // Draw HUD at top of screen
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            var healthBar = HealthBarSpriteFactory.Instance.HealthBarSprite(_playerCharacter.health);
            healthBar.Draw(spriteBatch, new Rectangle(64, 8, 256, 64));

            var inventory = _playerCharacter._inventory;

            if (inventory?.Items != null && inventory.Items.Count > 0)
            {
                int itemSize = 48;
                int startX = 520;
                int startY = 16;

                int index = 0;
                foreach (var kvp in inventory.Items)
                {
                    if (index == inventory.ActiveSlot)
                    {
                        var item = kvp.Key;
                        var drawRect = new Rectangle(startX, startY, itemSize, itemSize);

                        // Draw border (use 1x1 white pixel texture scaled)
                        Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                        pixel.SetData(new[] { Color.White });
                        Color borderColor = Color.CornflowerBlue;
                        int thickness = 2;

                        // Top
                        spriteBatch.Draw(pixel, new Rectangle(drawRect.X, drawRect.Y, drawRect.Width, thickness), borderColor);
                        // Bottom
                        spriteBatch.Draw(pixel, new Rectangle(drawRect.X, drawRect.Y + drawRect.Height - thickness, drawRect.Width, thickness), borderColor);
                        // Left
                        spriteBatch.Draw(pixel, new Rectangle(drawRect.X, drawRect.Y, thickness, drawRect.Height), borderColor);
                        // Right
                        spriteBatch.Draw(pixel, new Rectangle(drawRect.X + drawRect.Width - thickness, drawRect.Y, thickness, drawRect.Height), borderColor);

                        // Draw item itself
                        var originalLocation = item.Location;
                        item.Location = drawRect;
                        item.Draw(spriteBatch);
                        item.Location = originalLocation;

                        break;
                    }
                    index++;
                }
            }

            if (_mapRenderer != null)
            {
                _mapRenderer.Draw(spriteBatch);
            }


            if (_gameState.State == GameState.Paused)
                _pauseDesktop.Render();
        }

        /// <summary>The renderer shouldn't need to update anything.</summary>
        public void Update(GameTime gameTime)
        {
            _mapRenderer.SetRoomIndex(_roomManager.GetCurrentRoomIndex());
        }

        private void SatisfyFields()
        {
            if (this._playerCharacter == null)
                throw new System.NullReferenceException("Player property must be set.");
            if (this._roomManager == null)
                throw new System.NullReferenceException("RoomManager property must be set.");
            this._fieldsSatisfied = true;
        }

        /// <summary>
        /// Initializes the Myra UI for the pause menu (auto-configured from library).
        /// </summary>
        private void InitializePauseMenuUI()
        {
            // Create Myra root container as Desktop (rename to "root"?)
            _pauseDesktop = new Desktop();

            // Create a Panel with a semi-transparent black background
            _pausePanel = new Panel
            {
                Background = new SolidBrush(new Color(0, 0, 0, 150)),
                Width = 300,
                Height = 250,
                Padding = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            // Main stack panel to hold everything vertically; UI cluster
            mainStack = new VerticalStackPanel
            {
                Spacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            // 1. Centered "GAME PAUSED" label
            _pauseLabel = new Label
            {
                Text = "GAME PAUSED",
                TextColor = Color.White,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            mainStack.Widgets.Add(_pauseLabel);

            // 2. Sub-panel for everything else (left-aligned with a margin)
            stackPanel = new VerticalStackPanel
            {
                Spacing = 8,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 20, 0, 0), // Adds 20 pixels of space above this panel
            };

            // TODO: Add toggle off functionality after branch is merged
            _musicToggleButton = new Button
            {
                Content = new Label { Text = "Music: On/Off", TextColor = Color.White },
                HorizontalAlignment = HorizontalAlignment.Left,
            };

            _toggleMusicCommand = new ToggleMusicCommand(SoundEffectManager.Instance);
            _musicToggleButton.Click += (sender, e) => _toggleMusicCommand.Execute();

            stackPanel.Widgets.Add(_musicToggleButton);

            // Inventory header to align text
            _inventoryPanel = new Label
            {
                Text = "Inventory",
                TextColor = Color.White,
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            stackPanel.Widgets.Add(_inventoryPanel);

            // Inventory items; TODO: Work with Baqer to make functional
            stackPanel.Widgets.Add(new Label { Text = "   Sword", TextColor = Color.White });
            stackPanel.Widgets.Add(new Label { Text = "   Boomerang", TextColor = Color.White });
            stackPanel.Widgets.Add(new Label { Text = "   Bomb", TextColor = Color.White });

            // Add "child" stackPanel to the main stack
            mainStack.Widgets.Add(stackPanel);

            // Add the mainStack panel to the root pause panel
            _pausePanel.Widgets.Add(mainStack);

            // Set the panel as root widget of the Desktop
            _pauseDesktop.Root = _pausePanel;
        }
    }
}
