using Project.Packages.Items;

namespace Project
{
    public class ProjectileCommand : ICommand
    {
        private ItemManager _itemManager;
        private int _itemIndex;

        public ProjectileCommand(ItemManager itemManager, int itemIndex)
        {
            _itemManager = itemManager;
            _itemIndex = itemIndex;
        }

        public void Execute()
        {
            _itemManager.PlaceProjectile(_itemIndex);
        }
    }
}
