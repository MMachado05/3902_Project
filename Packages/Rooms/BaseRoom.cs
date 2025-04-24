using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Enemies;
using Project.Items;
using Project.Packages.Characters;
using Project.Packages.Rooms;
using Project.Rooms;
using Project.Rooms.Blocks;
using System.Collections.Generic;

namespace Project.Packages
{
    public class BaseRoom : IRoom
    {
        // Global references
        private CollisionManager _collisionManager;
        private Player _player;
        public Rectangle PlayerLocation
        {
            get => this._player.Location;
        }

        private IBlock[,] internalMap;
        private IBlock Background;

        // Per-room entity managers
        private EnemyManager _enemyManager;
        private ItemManager _itemManager;

        // Internal logic
        private Rectangle _defaultPlayerLocation;
        public Rectangle SavedPlayerLocation { set; get; } = Rectangle.Empty;
        string RoomName;

        private bool _active;
        IBlock MiniMap;

        public bool IsOnScreen
        {
            get => this._active;
            set => this._active = value;
        }

        // Logistic fields
        public BaseRoom(RoomData roomData)
        {
            this._collisionManager = roomData.CollisionManager;
            this._enemyManager = roomData.EnemyManager;
            this._itemManager = roomData.ItemManager;
            this._defaultPlayerLocation = roomData.PlayerStartLocation;
            this.internalMap = roomData.BlockMap;
            this.Background = roomData.Background;
            SavedPlayerLocation = this._defaultPlayerLocation;
            this.MiniMap = roomData.MiniMap;

            this._active = false;
        }

        public void TriggerPlayerAttack()
        {
            _player.Attack(_itemManager);
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
            MiniMap.Draw(sb);

            // Draw player in default location if this room was just activated
            if (!this._active)
            {
                this._active = true;

                this._player.Location =
                    SavedPlayerLocation != Rectangle.Empty
                        ? SavedPlayerLocation
                        : _defaultPlayerLocation;
            }

            this._player.Draw(sb);
            this._enemyManager.Draw(sb);
            foreach (IItem item in _itemManager.GetWorldItems())
            {
                item.Draw(sb);
            }
        }
        public string GetRoomName()
        {
            return RoomName;
        }
        public List<IEnemy> GetAllCurrentEnimeies()
        {
            return this._enemyManager.GetAllEnimies();
        }

        public void Update(GameTime gameTime)
        {
            this._enemyManager.Update(gameTime);
            this._itemManager.Update(gameTime);

            //Player and Enemy/Projecilt Collision
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
            for (int i = 0; i < this._itemManager.GetProjectiles.Count; i++)
            {
                this._collisionManager.Collide(this._player, this._itemManager.GetProjectiles[i]);
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

            // Enemy and Projectile Collision
            for (int i = 0; i < this._enemyManager.enemies.Count; i++)
            {
                var enemy = this._enemyManager.enemies[i];
                if (
                    _player._inventory.GetCurrentItem() is Bomb
                    && ((Bomb)_player._inventory.GetCurrentItem()).ExplodingBomb is Explosion
                )
                {
                    this._collisionManager.Collide(
                        enemy,
                        ((Bomb)_player._inventory.GetCurrentItem()).ExplodingBomb
                    );
                }
                if (_player._inventory.GetCurrentItem() is Boomerang)
                {
                    foreach (
                        ThrownBoomerang boomerang in (
                            (Boomerang)_player._inventory.GetCurrentItem()
                        ).projectiles
                    )
                        this._collisionManager.Collide(enemy, boomerang);
                }
            }
            // Enemy and BaseAttack Collisions
            if (_player.IsAttacking())
            {
                var attack = _player.GetBasicAttack;
                if (attack != null)
                {
                    foreach (IEnemy enemy in _enemyManager.enemies)
                    {
                        if (attack.Location.Intersects(enemy.Location))
                        {
                            enemy.TakeDamage(1);
                        }
                    }
                }
            }
        }
    }
}
