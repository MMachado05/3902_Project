using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project.Packages.Items
{
    public class ItemManager
    {
        private readonly List<IItem> inventory;
        private readonly List<IItem> worldItems;
        private readonly List<IItem> projectiles;
        private int currentItemIndex = 0;
        private Game1 _game;

        public ItemManager(Game1 game)
        {
            _game = game;

            inventory = new List<IItem>
            {
                new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateSwordSprite()),
                new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateBombSprite()),
                new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateBowSprite()),
                new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateHeartSprite()),
                new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateCoinSprite()),
                new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateKeySprite())
            };

            projectiles = new List<IItem>
            {
                new ProjectileItem(Vector2.Zero, Vector2.Zero, ItemFactory.Instance.CreateSlashSprite(), 0, 500),
                new ProjectileItem(Vector2.Zero, Vector2.Zero, ItemFactory.Instance.CreateExplosionSprite(), 0, 500),
                new ProjectileItem(Vector2.Zero, Vector2.Zero, ItemFactory.Instance.CreateArrowSprite(), 5, 500)
            };

            worldItems = new List<IItem>(); // Initially empty
        }

        public void SetCurrentIndex(int index)
        {
            if (index >= 0 && index < inventory.Count)
                currentItemIndex = index;
        }

        public int GetCurrentIndex() => currentItemIndex;

        public IItem GetCurrentItem() => inventory[currentItemIndex];

        public List<IItem> GetWorldItems() => worldItems;

        public void PlaceInventoryItem()
        {
            GetCurrentItem().Position = GetPlacementPosition();
        }

        public void PlaceProjectile(int index)
        {
            if (index < 0 || index >= projectiles.Count) return;

            Vector2 position = GetPlacementPosition();
            Vector2 direction = GetItemDirection();

            IItem itemToPlace = projectiles[index];

            if (itemToPlace is ProjectileItem proj)
            {
                worldItems.Add(new ProjectileItem(position, direction, proj.Sprite, proj.Speed, 100));
            }
        }

        private Vector2 GetPlacementPosition()
        {
            Vector2 playerPos = _game.player.PositionVector;
            return _game.lastDirection switch
            {
                "Up" => playerPos + new Vector2(0, -30),
                "Down" => playerPos + new Vector2(0, 30),
                "Left" => playerPos + new Vector2(-30, 0),
                "Right" => playerPos + new Vector2(30, 0),
                _ => playerPos
            };
        }

        private Vector2 GetItemDirection()
        {
            return _game.lastDirection switch
            {
                "Up" => new Vector2(0, -1),
                "Down" => new Vector2(0, 1),
                "Left" => new Vector2(-1, 0),
                "Right" => new Vector2(1, 0),
                _ => new Vector2(1, 0) // Default: Right
            };
        }
    }
}
