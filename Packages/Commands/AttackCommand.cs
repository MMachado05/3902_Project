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

            string direction = _player.LastDirection; // Must be in Excecute for lastest direction, NOT constructor

            if (_player.Sprite.State != SpriteState.Attacking)
            {
                switch (direction)
                {
                    case "Up":
                        _player.ChangeSprite(SpriteFactory.Instance.NewUpAttackingPlayer());
                        break;
                    case "Down":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDownAttackingPlayer());
                        break;
                    case "Left":
                        _player.ChangeSprite(SpriteFactory.Instance.NewLeftAttackingPlayer());
                        break;
                    case "Right":
                        _player.ChangeSprite(SpriteFactory.Instance.NewRightAttackingPlayer());
                        break;
                }

            }

        }
    }
}
