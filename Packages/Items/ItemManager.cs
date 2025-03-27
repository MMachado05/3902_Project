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

            // NOTE: From Boggus: Separate these into three different classes.
            // Recommendation: items in the world and *maybe* the ones in the inventory
            // can be IItems. Projectiles should be IProjectiles, due to the collision
            // logic differing (inventory items don't collide, so we could just reuse
            // some other type). Always ensure we have sprites to actually draw them.
            inventory = new List<IItem>
            {
                /*new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateSwordSprite()),*/
                /*new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateBombSprite()),*/
                /*new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateBowSprite()),*/
                /*new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateHeartSprite()),*/
                /*new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateCoinSprite()),*/
                /*new StationaryItem(Vector2.Zero, 0, ItemFactory.Instance.CreateKeySprite())*/
            };

            projectiles = new List<IItem>
            {
                /*new ProjectileItem(Vector2.Zero, Vector2.Zero, ItemFactory.Instance.CreateSlashSprite(), 0, 500),*/
                /*new ProjectileItem(Vector2.Zero, Vector2.Zero, ItemFactory.Instance.CreateExplosionSprite(), 0, 500),*/
                /*new ProjectileItem(Vector2.Zero, Vector2.Zero, ItemFactory.Instance.CreateArrowSprite(), 5, 500)*/
            };

            worldItems = new List<IItem>
            {
                //temporary random positions for collectible Items
                /*new StationaryItem(new Vector2(100,200), 0, ItemFactory.Instance.CreateHeartSprite()),*/
                /*new StationaryItem(new Vector2(500,100), 0, ItemFactory.Instance.CreateCoinSprite()),*/
                /*new StationaryItem(new Vector2(300,300), 0, ItemFactory.Instance.CreateKeySprite())*/
            };
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
            /*GetCurrentItem().Position = GetPlacementPosition();*/
        }

        public void PlaceProjectile(int index)
        {
            if (index < 0 || index >= projectiles.Count) return;

            Vector2 position = GetPlacementPosition();
            Vector2 direction = GetItemDirection();

            IItem itemToPlace = projectiles[index];

            /*if (itemToPlace is ProjectileItem proj)*/
            /*{*/
            /*    worldItems.Add(new ProjectileItem(position, direction, proj.Sprite, proj.Speed, 100));*/
            /*}*/
        }

        private Vector2 GetPlacementPosition()
        {
            /*Vector2 playerPos = _game.player.PositionVector;*/
            /*return _game.lastDirection switch*/
            /*{*/
            /*    "Up" => playerPos + new Vector2(0, -50),*/
            /*    "Down" => playerPos + new Vector2(0, 50),*/
            /*    "Left" => playerPos + new Vector2(-30, 0),*/
            /*    "Right" => playerPos + new Vector2(30, 0),*/
            /*    _ => playerPos*/
            /*};*/
            // TODO: Fix this whole ass class
            return new Vector2();
        }

        private Vector2 GetItemDirection()
        {
            // TODO: Needa refactor

            /*return _game.lastDirection switch*/
            /*{*/
            /*    "Up" => new Vector2(0, -1),*/
            /*    "Down" => new Vector2(0, 1),*/
            /*    "Left" => new Vector2(-1, 0),*/
            /*    "Right" => new Vector2(1, 0),*/
            /*    _ => new Vector2(1, 0) // Default: Right*/
            /*};*/
            return new Vector2();
        }

        public void removeItem(Item item)
        {
            worldItems.Remove(item);
        }
    }
}
