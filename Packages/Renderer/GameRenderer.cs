using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.rooms;

namespace Project.renderer
{
    public class GameRenderer : IRenderer
    {
        private Player _playerCharacter;
        private RoomManager _roomManager;

        public GameRenderer() { }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>The renderer shouldn't need to update anything.</summary>
        public void Update(GameTime gameTime) { }
    }
}
