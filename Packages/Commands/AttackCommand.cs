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
                        if (_player.isDamaged)
                            _player.ChangeSprite(SpriteFactory.Instance.NewDamagedUpAttackingPlayer());
                        else
                        _player.ChangeSprite(SpriteFactory.Instance.NewUpAttackingPlayer());
                        break;
                    case "Down":
                        if (_player.isDamaged)
                            _player.ChangeSprite(SpriteFactory.Instance.NewDamagedDownAttackingPlayer());
                        else
                            _player.ChangeSprite(SpriteFactory.Instance.NewDownAttackingPlayer());
                        break;
                    case "Left":
                        if (_player.isDamaged)
                            _player.ChangeSprite(SpriteFactory.Instance.NewDamagedLeftAttackingPlayer());
                        else
                            _player.ChangeSprite(SpriteFactory.Instance.NewLeftAttackingPlayer());
                        break;
                    case "Right":
                        if (_player.isDamaged)
                            _player.ChangeSprite(SpriteFactory.Instance.NewDamagedRightAttackingPlayer());
                        else
                            _player.ChangeSprite(SpriteFactory.Instance.NewRightAttackingPlayer());
                        break;
                }

            }

        }
    }
}
