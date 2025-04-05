using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public GameRenderer(int screenWidth, int screenHeight, int tileWidth, int tileHeight, GameStateMachine gameState, SpriteFont tempPauseFont)
        {
            this._screenWidth = screenWidth;
            this._screenHeight = screenHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this._fieldsSatisfied = false;
            this._gameState = gameState;
            this._tempPauseFont = tempPauseFont;
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
                string pauseMsg = "Game Paused (Press P to Unpause)";
                Vector2 textSize = this._tempPauseFont.MeasureString(pauseMsg);
                spriteBatch.DrawString(_tempPauseFont,
                    pauseMsg,
                    new Vector2(
                      (this._screenWidth - textSize.X) / 2,
                      (this._screenHeight - textSize.Y) / 2
                      ),
                    Color.White);
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
    }
}
