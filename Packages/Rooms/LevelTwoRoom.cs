using Project.Blocks;
using Project.Enemies;

namespace Project.Packages
{
    public class LevelTwoRoom : BaseRoom
    {
        public LevelTwoRoom(SolidBlockManager manager,EnemyManager enemyManager,Game1 game) : base(manager,enemyManager,game)
        {
            room = parser.loadRoom("../../../Data/room2.csv");
            // NOTE: From Boggus: consider making a single room class that takes in various
            // construction parameters to create different "kinds" of rooms
        }
    }
}
