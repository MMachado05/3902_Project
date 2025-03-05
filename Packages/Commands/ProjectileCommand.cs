using Project.Packages.Items;

namespace Project
{
    public class ProjectileCommand : ICommand
    {
        private ItemManager _itemManager;
        private int _itemIndex;

        public ProjectileCommand(ItemManager itemManager)
        {
            _itemManager = itemManager;
        }

        public void Execute()
        {
            _itemManager.PlaceProjectile(_itemManager.GetCurrentIndex());
        }
    }
}
