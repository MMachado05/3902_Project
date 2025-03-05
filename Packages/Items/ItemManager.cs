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
                ItemFactory.Instance.CreateSword(new Vector2(100, 100)),
                ItemFactory.Instance.CreateBomb(new Vector2(100, 100)),
                ItemFactory.Instance.CreateBow(new Vector2(100, 100))
            };
            //replace with other projectiles
            projectiles = new List<IItem>
            {
                ItemFactory.Instance.CreateArrow(new Vector2(100, 100)),
                ItemFactory.Instance.CreateArrow(new Vector2(100, 100)),
                ItemFactory.Instance.CreateArrow(new Vector2(100, 100))
            };

            worldItems = new List<IItem>
            {
                ItemFactory.Instance.CreateHeart(new Vector2(200, 300)),
                ItemFactory.Instance.CreateCoin(new Vector2(450, 200)),
                ItemFactory.Instance.CreateKey(new Vector2(700, 250))
            };
        }

        public void SetCurrentItem(int index)
        {
            currentItemIndex = index;
        }

        public IItem GetCurrentItem()
        {
            return inventory[currentItemIndex];
        }

        public List<IItem> GetWorldItems()
        {
            return worldItems;
        }

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
