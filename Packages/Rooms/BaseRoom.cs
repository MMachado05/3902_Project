using Microsoft.Xna.Framework;
using Project.Enemies;
using Project.rooms;
using Project.Packages.Items;
using Microsoft.Xna.Framework.Graphics;
namespace Project.Packages
{
    public class BaseRoom : IRoom
    {
        // Player reference
        private Player _player;
        public Rectangle PlayerLocation { get => this._player.PositionRect; }

        private IBlock[,] internalMap;

        // Per-room entity managers
        private EnemyManager _enemyManager;
        private ItemManager _itemManager;

        // Internal logic
        private Rectangle _defaultPlayerLocation;
        public Rectangle SavedPlayerLocation { set => this._defaultPlayerLocation = value; }

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

        public void AssignPlayer(Player player)
        {
            this._player = player;
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

            // Draw player in default location if this room was just activated
            if (!this._active)
            {
                this._active = true;
                this._player.PositionRect = this._defaultPlayerLocation;
            }

            System.Console.WriteLine("X: " + this._player.PositionRect.X);
            System.Console.WriteLine("Y: " + this._player.PositionRect.Y);
            System.Console.WriteLine("Width: " + this._player.PositionRect.Width);
            System.Console.WriteLine("Height: " + this._player.PositionRect.Height);

            this._player.Draw(sb);
        }
    }
}
