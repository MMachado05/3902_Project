using Project.Packages.Items;

namespace Project
{
    public class InventoryCommand : ICommand
    {
        private ItemManager _itemManager;
        private int _itemIndex;

        public InventoryCommand(ItemManager itemManager, int itemIndex)
        {
            _itemManager = itemManager;
            _itemIndex = itemIndex;
        }

        public void Execute()
        {
            _itemManager.PlaceItem(_itemIndex);
        }
    }
}
