using Project.Packages.Items;

namespace Project.Content
{
    public class PlayerItemCollisionHandler
    {
        private ItemManager itemManager;
        private Player player;
        public PlayerItemCollisionHandler(ItemManager _itemManager, Player _player)
        {
            this.itemManager = _itemManager;
            this.player = _player;
        }
        public void HandlePlayerItemCollision()
        {
            // TODO: Implement using Player rectangle field

            /*Rectangle playerRectangle = new Rectangle((int)player.PositionVector.X, (int)player.PositionVector.Y, 30, 30);*/
            /*Item removedItem = null;*/
            /*foreach (Item item in itemManager.GetWorldItems())*/
            /*{*/
            /*    Rectangle itemRectangle = new Rectangle((int)item.Position.X, (int)item.Position.Y, 16, 16);*/
            /*    if (playerRectangle.IntersectsWith(itemRectangle))*/
            /*    {*/
            /*        removedItem = item;*/
            /*    }*/
            /*}*/
            /*//removes it only after the loop is done so it doesn't mess it up*/
            /*if (removedItem != null)*/
            /*{*/
            /*    itemManager.removeItem(removedItem);*/
            /*}*/
        }
    }
}
