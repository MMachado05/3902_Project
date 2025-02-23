using Project.Packages.Items;

namespace Project
{
    public class InventoryCommand : ICommand
    {
        private ItemManager _itemManager;
        private string _command;

        public InventoryCommand(ItemManager itemManager, string command)
        {
            _itemManager = itemManager;
            _command = command;
        }

        public void Execute()
        {
            switch (_command)
            {
                case "1":
                    _itemManager.currentItemIndex = 2;
                    break;
                case "2":
                    _itemManager.currentItemIndex = 3;
                    break;
                case "3":
                    _itemManager.currentItemIndex = 4;
                    break;
                //these might be removed if we don't need u and i for switching items
                case "Next":
                    _itemManager.nextItem();
                    break;
                case "Previous":
                    _itemManager.previousItem();
                    break;
            }
            
        }
    }
}
