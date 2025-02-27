using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Packages.Items;

namespace Project.Content
{
    internal class PlayerItemCollisionHandler
    {
        private Game1 game;
        private ItemManager itemManager;
        private Player player;
        public PlayerItemCollisionHandler(Game1 game, ItemManager _itemManager, Player _player)
        {
            this.game = game;
            this.itemManager = _itemManager;
            this.player = _player;
        }
        public void HandlePlayerItemCollision()
        {
            Rectangle playerRectangle = new Rectangle((int)player.PositionVector.X, (int)player.PositionVector.Y, 30, 30);
            foreach (Item item in itemManager.)
            {
                Rectangle itemRectangle = new Rectangle();
                if (playerRectangle.Intersect()
                {
                    item.PickUp(game.player);
                    game.itemManager.RemoveItem(item);
                }
            }
        }
    }
}
