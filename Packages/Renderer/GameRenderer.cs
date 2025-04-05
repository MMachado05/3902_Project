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

        private int _screenWidth;
        private int _screenHeight;
        private int tileWidth;
        private int tileHeight;

        public int TileWidth { get => tileWidth; }
        public int TileHeight { get => tileHeight; }

        // Myra UI components for the pause menu
        private Desktop _pauseDesktop;
        private Panel _pausePanel;
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
                
                _pauseDesktop.Render(); // Myra test

                //string pauseMsg = "Game Paused (Press P to Unpause)";
                //Vector2 textSize = this._tempPauseFont.MeasureString(pauseMsg);
                //spriteBatch.DrawString(_tempPauseFont,
                //    pauseMsg,
                //    new Vector2(
                //      (this._screenWidth - textSize.X) / 2,
                //      (this._screenHeight - textSize.Y) / 2
                //      ),
                //    Color.White);
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
        /// Initializes the Myra UI for the pause menu.
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
                Height = 200,
                Padding = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Create a label indicating the game is paused
            _pauseLabel = new Label
            {
                Text = "Game Paused",
                TextColor = Color.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Create a placeholder music toggle button
            _musicToggleButton = new Button
            {
                Content = new Label { Text = "Music: On/Off", TextColor = Color.White },
                HorizontalAlignment = HorizontalAlignment.Center
            };

            _musicToggleButton.Click += (sender, args) =>
            {
                // TODO: Implement music toggle functionality
            };

            // Create a vertical stack panel for inventory items
            _inventoryPanel = new VerticalStackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Spacing = 8
            };

            // Add sample inventory items
            _inventoryPanel.Widgets.Add(new Label { Text = "Sword", TextColor = Color.White });
            _inventoryPanel.Widgets.Add(new Label { Text = "Boomerang", TextColor = Color.White });
            _inventoryPanel.Widgets.Add(new Label { Text = "Bomb", TextColor = Color.White });

            // Add the UI elements to the panel
            _pausePanel.Widgets.Add(_pauseLabel);
            _pausePanel.Widgets.Add(_musicToggleButton);
            _pausePanel.Widgets.Add(_inventoryPanel);

            // Set the panel as the root widget of the Desktop
            _pauseDesktop.Root = _pausePanel;
        }
    }

}
