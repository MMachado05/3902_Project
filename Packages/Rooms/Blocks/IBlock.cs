using Microsoft.Xna.Framework.Graphics;

namespace Project.Rooms.Blocks
{
    public interface IBlock : IGameObject
    {
        public void Draw(SpriteBatch sb);
    }
}
