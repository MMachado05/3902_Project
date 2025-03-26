using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public interface IBlock : IGameObject
    {
        public void Draw(SpriteBatch sb);
    }
}
