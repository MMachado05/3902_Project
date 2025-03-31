using Microsoft.Xna.Framework.Graphics;

namespace Project.Rooms.Blocks
{
    public interface IBlock : IGameObject
    {
        public bool IsPassable { get; }
        public void Draw(SpriteBatch sb);
    }
}
