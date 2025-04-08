using Microsoft.Xna.Framework;
using Project.Enemies;
using Microsoft.Xna.Framework.Graphics;
using Project.Packages.Characters;
using Project.Rooms;
using Project.Rooms.Blocks;
using Project.Items;
using Project.Characters;
using Project.Factories;
using Project.Rooms.Blocks.ConcreteClasses;
namespace Project.Packages
{
    public class BaseRoom : IRoom
    {
        // Global references
        private CollisionManager _collisionManager;
        private Player _player;
        public Rectangle PlayerLocation { get => this._player.Location; }

        private IBlock[,] internalMap;
        private IBlock Background;

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

        public BaseRoom(CollisionManager collisionManager, ItemManager itemManager, EnemyManager enemyManager, Rectangle defaultPlayerLocation,
            IBlock[,] internalMap, IBlock Background)
        {
            this._collisionManager = collisionManager;
            this._enemyManager = enemyManager;
            this._itemManager = itemManager;
            this._defaultPlayerLocation = defaultPlayerLocation;
            this.internalMap = internalMap;
            this.Background = Background;

            this._active = false;
        }

        public void AssignPlayer(Player player)
        {
            this._player = player;
        }

        public void Draw(SpriteBatch sb)
        {
            Background.Draw(sb);
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
                this._player.Location = this._defaultPlayerLocation;
            }

            this._player.Draw(sb);
            this._enemyManager.Draw(sb);
            foreach (IItem item in _itemManager.GetWorldItems())
            {
                item.Draw(sb);
            }
        }

        public void Update(GameTime gameTime)
        {
            this._enemyManager.Update(gameTime);

            //Player and Enemy Collision
            for (int i = 0; i < this._enemyManager.enemies.Count; i++)
            {
                this._enemyManager.SwitchToNextEnemy();
                this._collisionManager.Collide(this._player, this._enemyManager.ReturnEnemy());
            }
            //Player and Item Collison
            for (int i = 0; i < this._itemManager.GetWorldItems().Count; i++)
            {
                this._collisionManager.Collide(this._player, this._itemManager.GetWorldItems()[i]);
            }
            //Player and Block Collison
            for (int i = 0; i < this.internalMap.GetLength(0); i++)
            {
                for (int j = 0; j < this.internalMap.GetLength(1); j++)
                {
                    if (this.internalMap[i, j] != null)
                    {
                        this._collisionManager.Collide(this._player, this.internalMap[i, j]);
                    }
                }
            }
            // Enemy and Block Collision
            for (int i = 0; i < this._enemyManager.enemies.Count; i++)
            {
                var enemy = this._enemyManager.enemies[i];
                for (int x = 0; x < this.internalMap.GetLength(0); x++)
                {
                    for (int y = 0; y < this.internalMap.GetLength(1); y++)
                    {
                        if (this.internalMap[x, y] != null)
                        {
                            this._collisionManager.Collide(enemy, this.internalMap[x, y]);
                        }
                    }
                }

            }
        }
    }
}