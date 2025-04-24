using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Enemies.EnemyClasses;

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
        public List<Enemy> GetAllCurrentEnimeies();
    }
}
