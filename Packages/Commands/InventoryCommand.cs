using Project.Packages.Items;

namespace Project
{
    public class InventoryCommand : ICommand
    {
        private ItemManager _itemManager;
        private int _itemSlot;

        public InventoryCommand(ItemManager itemManager, int itemSlot)
        {
            _itemManager = itemManager;
            _itemSlot = itemSlot;
        }

        public void Execute()
        {
            _itemManager.currentItemIndex = _itemSlot;
        }
    }
}
