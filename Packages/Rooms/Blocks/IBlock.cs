using Microsoft.Xna.Framework.Graphics;

namespace Project.Rooms.Blocks
{
    public interface IBlock : IGameObject
    {
        bool SwitchRoom { get; set; }
        public void Draw(SpriteBatch sb);
    }
}
