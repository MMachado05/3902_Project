using Project.Packages.Items;
using Microsoft.Xna.Framework;

namespace Project
{
    public class InventoryCommand : ICommand
    {
        private ItemManager _itemManager;
        private string _command;

        private Game1 _game;

        public InventoryCommand(ItemManager itemManager, string command, Game1 game)
        {
            _itemManager = itemManager;
            _command = command;
            _game = game;
        }

        public void Execute()
        {
            Vector2 placePosition = _game.player.PositionVector + new Vector2(30, 0);

            switch (_command)
            {
                case "1":
                    _itemManager.PlaceItem(0, placePosition);
                    break;
                case "2":
                    _itemManager.PlaceItem(1, placePosition);
                    break;
                case "3":
                    _itemManager.PlaceItem(2, placePosition);
                    break;
                case "4":
                    _itemManager.PlaceItem(3, placePosition);
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
