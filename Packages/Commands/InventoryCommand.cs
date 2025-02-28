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
                    _itemManager.SelectItem(0);
                    break;
                case "2":
                    _itemManager.SelectItem(1);
                    break;
                case "3":
                    _itemManager.SelectItem(2);
                    break;
                case "Next":
                    _itemManager.NextItem();
                    break;
                case "Previous":
                    _itemManager.PreviousItem();
                    break;
            }
        }
    }
}
