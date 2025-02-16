using System;

namespace Project
{
    public class DamageCommand : ICommand
    {
        private Player _player;

        public DamageCommand(Player player)
        {
            _player = player;
        }

        public void Execute()
        {
            _player.isDamaged = !_player.isDamaged;
        }
    }
}
