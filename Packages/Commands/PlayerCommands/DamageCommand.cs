using Project.Characters;

namespace Project.Commands.PlayerCommands
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
            _player.health -= 1;
            _player.invincibleTime = 1;
        }
    }
}
