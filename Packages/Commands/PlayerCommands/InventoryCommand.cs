﻿using Project.Characters;
using Project.Items;

namespace Project.Commands.PlayerCommands
{
    public class InventoryCommand : ICommand
    {
        private Player _player;
        private int _itemIndex;

        public InventoryCommand(Player player, int itemIndex)
        {
            _player = player;
            _itemIndex = itemIndex;
        }

        public void Execute()
        {
            _player._inventory.setIndex(_itemIndex);
            _player._inventory.GetCurrentItem().Item1.Location = _player.Location;
        }
    }
}
