using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project.Packages.Items
{
    public class ItemManager
    {
        private readonly List<IItem> inventory;
        private readonly List<IItem> worldItems;
        private Game1 _game;

        public ItemManager(Game1 game)
        {
            _game = game;

            inventory = new List<IItem>
            {
                new ProjectileItem(new Vector2(100, 100), new Vector2(0, 1), ItemFactory.Instance.CreateArrowSprite(), 5, 500),
                new StationaryItem(new Vector2(100, 100), 0, ItemFactory.Instance.CreateSwordSprite()),
                new StationaryItem(new Vector2(100, 100), 0, ItemFactory.Instance.CreateBombSprite()),
                new StationaryItem(new Vector2(100, 100), 0, ItemFactory.Instance.CreateBowSprite()),
                new ProjectileItem(new Vector2(100, 100), new Vector2(0, 1), ItemFactory.Instance.CreateArrowSprite(), 5, 500)
            };

            worldItems = new List<IItem>
            {
                new StationaryItem(new Vector2(200, 300), 0, ItemFactory.Instance.CreateHeartSprite()),
                new StationaryItem(new Vector2(450, 200), 0, ItemFactory.Instance.CreateCoinSprite()),
                new StationaryItem(new Vector2(700, 250), 0, ItemFactory.Instance.CreateKeySprite())
            };
        }

        public List<IItem> GetWorldItems()
        {
            return worldItems;
        }

        public void PlaceItem(int index)
        {
            if (index < 0 || index >= inventory.Count) return;

            Vector2 position = GetPlacementPosition();
            Vector2 direction = GetItemDirection();
            IItem itemToPlace = inventory[index];

            if (itemToPlace is ProjectileItem proj)
            {
                worldItems.Add(new ProjectileItem(position, direction, proj.Sprite, proj.Speed, 100));
            }
            else if (itemToPlace is StationaryItem stat)
            {
                worldItems.Add(new StationaryItem(position, stat.Speed, stat.Sprite));
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
