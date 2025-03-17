namespace Project
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

            if (_player.Sprite.State != SpriteState.Attacking)
              _player.ChangeSprite(PlayerSpriteFactory.Instance.NewAttackingPlayerSprite(_player.LastDirection, _player.isDamaged));

        }
    }
}
