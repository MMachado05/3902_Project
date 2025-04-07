using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Rooms.Blocks;

namespace Project.Rooms
{
    public interface IRoom
    {
        public Rectangle PlayerLocation { get; }
        public Rectangle SavedPlayerLocation { set; }
        public bool IsOnScreen { get; set; }

        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch sb);
        public void AssignPlayer(Player player);
        public IBlock currentDoor();
    }
}
