using Project.Characters;
using Project.Characters.Enums;
using Project.Factories;

namespace Project.Commands.PlayerCommands
{
    public class AttackCommand : ICommand
    {
        private Player _player;

        public AttackCommand(Player player)
        {
            _player = player;
        }

        public void Execute()
        {
            System.Console.WriteLine("Attacking...");
            if (_player.Sprite.State != CharacterState.Attacking)
                _player.ChangeSprite(PlayerSpriteFactory.Instance.NewAttackingPlayerSprite(_player.LastDirection, _player.isDamaged));
        }
    }
}
