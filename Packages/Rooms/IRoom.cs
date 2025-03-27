using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.rooms
{
    public interface IRoom
    {
        public Rectangle PlayerLocation { get; }
        public Rectangle SavedPlayerLocation { set; }
        public bool IsOnScreen { get; set; }

        public void Draw(SpriteBatch sb);
        public void AssignPlayer(Player player);
    }
}
