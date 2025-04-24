using Project.Characters.Enums;
using Project.Factories;
using Project.Packages.Sounds;
using Project.Rooms;

namespace Project.Commands.PlayerCommands
{
    public class AttackCommand : ICommand
    {
        private RoomManager _roomManager;

        public AttackCommand(RoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        public void Execute()
        {
            if (_roomManager.player.Sprite.State != CharacterState.Attacking)
                _roomManager.player.ChangeSprite(PlayerSpriteFactory.Instance.NewAttackingPlayerSprite(_roomManager.player.LastActiveDirection, _roomManager.player.invincibleTime > 0));
            _roomManager.GetCurrentRoom().TriggerPlayerAttack();
            SoundEffectManager.Instance.playSword();
        }
    }
}
