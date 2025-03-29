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

        private int tileWidth;
        private int tileHeight;

        public int TileWidth { get => tileWidth; }
        public int TileHeight { get => tileHeight; }

        public GameRenderer(int tileWidth, int tileHeight)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this._fieldsSatisfied = false;
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
