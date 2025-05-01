using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Enemies;
using Project.Rooms.Blocks;

namespace Project.Rooms
{
    public interface IRoom
    {
        public Rectangle PlayerLocation { get; }
        public Rectangle SavedPlayerLocation { set; get; }
        public bool IsOnScreen { get; set; }

        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch sb);
        public void AssignPlayer(Player player);
        public string GetRoomName();
        public void TriggerPlayerAttack();
        public List<IEnemy> GetAllCurrentEnimeies();

    }
}
