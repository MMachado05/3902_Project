using Microsoft.Xna.Framework.Graphics;

namespace Project.rooms
{
    public interface IRoom
    {
        public void Draw(SpriteBatch sb);
        public int getPlayerIndex();
    }
}
