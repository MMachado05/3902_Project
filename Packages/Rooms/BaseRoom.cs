using Microsoft.Xna.Framework;
using Project.Enemies;
using Project.rooms;
using Project.Packages.Items;
using Microsoft.Xna.Framework.Graphics;
namespace Project.Packages
{
    public class BaseRoom : IRoom
    {
        private IBlock[,] internalMap;

        // Per-room entity managers
        private EnemyManager _enemyManager;
        private ItemManager _itemManager;

        // Internal logic
        private Rectangle _defaultPlayerLocation;
        private bool _active;

        public bool IsOnScreen { get => this._active; set => this._active = value; }

        // Logistic fields
        int playerIndex;

        public BaseRoom(EnemyManager enemyManager, Rectangle defaultPlayerLocation,
            IBlock[,] internalMap)
        {
            _enemyManager = enemyManager;
            this._defaultPlayerLocation = defaultPlayerLocation;
            this.internalMap = internalMap;

            this._active = false;
        }

        public void Draw(SpriteBatch sb)
        {
            for (int x = 0; x < this.internalMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.internalMap.GetLength(1); y++)
                {
                    if (this.internalMap[x, y] != null)
                        this.internalMap[x, y].Draw(sb);
                }
            }
        }

        public int getPlayerIndex()
        {
            return playerIndex;
        }
    }
}
