using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Project.Characters;
using Project.Rooms;

namespace Project.Renderer
{
    public class GameRenderer : IRenderer
    {
        private Player _playerCharacter;
        public Player PlayerCharacter { set => this._playerCharacter = value; }

        private RoomManager _roomManager;
        public RoomManager RoomManager { set => this._roomManager = value; }

        private bool _fieldsSatisfied;
        private GameStateMachine _gameState;
        private SpriteFont _tempPauseFont;

        private int _screenWidth; // Since we can't see game1
        private int _screenHeight; // Since we can't see game1
        private int tileWidth;
        private int tileHeight;

        public int TileWidth { get => tileWidth; }
        public int TileHeight { get => tileHeight; }

        // Myra UI components for the pause menu
        private Desktop _pauseDesktop;
        private Panel _pausePanel;
        private VerticalStackPanel stackPanel;
        private Label _pauseLabel;
        private Button _musicToggleButton;
        private VerticalStackPanel _inventoryPanel;

        public GameRenderer(int screenWidth, int screenHeight, int tileWidth, int tileHeight, GameStateMachine gameState, SpriteFont tempPauseFont)
        {
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this._fieldsSatisfied = false;
            this._gameState = gameState;
            this._tempPauseFont = tempPauseFont;

            InitializePauseMenuUI();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Ensure dependencies have been injected
            if (!this._fieldsSatisfied)
                this.SatisfyFields();

            this._roomManager.DrawCurrentRoom(spriteBatch);
            this._playerCharacter.Draw(spriteBatch);

            // TODO: Catch collisions during the drawing stage and call relevant commands
            //  to colliding objects as needed

            if (this._gameState.State == GameState.Paused)
            {
                // Osama: im gonna try some super janky workaround I saw on one of the forums:
                spriteBatch.End();

                _pauseDesktop.Render(); // Myra test

                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            }
        }

        /// <summary>The renderer shouldn't need to update anything.</summary>
        public void Update(GameTime gameTime) { }

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
            // Create the Desktop – Myra’s root container
            _pauseDesktop = new Desktop();

            // Create a Panel with a semi-transparent black background
            _pausePanel = new Panel
            {
                Background = new SolidBrush(new Color(0, 0, 0, 150)),
                Width = 300,
                Height = 250,
                Padding = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Main stack panel to hold everything vertically
            var mainStack = new VerticalStackPanel
            {
                Spacing = 8,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // 1. Centered "GAME PAUSED" label
            var pauseLabel = new Label
            {
                Text = "GAME PAUSED",
                TextColor = Color.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            mainStack.Widgets.Add(pauseLabel);

            // 2. Sub-panel for everything else (left-aligned with a margin)
            var subPanel = new VerticalStackPanel
            {
                Spacing = 8,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 20, 0, 0) // Adds 20 pixels of space above this panel
            };

            // "Music: On/Off" button
            var musicToggleButton = new Button
            {
                Content = new Label { Text = "Music: On/Off", TextColor = Color.White },
                HorizontalAlignment = HorizontalAlignment.Left
            };
            subPanel.Widgets.Add(musicToggleButton);

            // "Inventory" label
            var inventoryHeader = new Label
            {
                Text = "Inventory",
                TextColor = Color.White,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            subPanel.Widgets.Add(inventoryHeader);

            // Inventory items
            subPanel.Widgets.Add(new Label { Text = "   Sword", TextColor = Color.White });
            subPanel.Widgets.Add(new Label { Text = "   Boomerang", TextColor = Color.White });
            subPanel.Widgets.Add(new Label { Text = "   Bomb", TextColor = Color.White });

            // Add the sub-panel to the main stack
            mainStack.Widgets.Add(subPanel);

            // Add the main stack panel to the pause panel
            _pausePanel.Widgets.Add(mainStack);

            // Set the panel as the root widget of the Desktop
            _pauseDesktop.Root = _pausePanel;
        }



    }

}
