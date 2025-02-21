namespace Project
{
    public class MoveCommand : ICommand
    {
        private Player _player;
        private string _direction;

        public MoveCommand(Player player, string direction)
        {
            _direction = direction;
            _player = player;
        }

        public void Execute()
        {
            // Maybe avoid hardcoding speed
            int dx = 0, dy = 0;

            switch (_direction)
            {
                case "Up":
                    dy = -2;
                    break;
                case "Down":
                    dy = 2;
                    break;
                case "Left":
                    dx = -2;
                    break;
                case "Right":
                    dx = 2;
                    break;
            }

            if (_player.Sprite.State != SpriteState.Attacking)
            {
                _player.Move(dx, dy, _direction);

                switch (_direction)
                {
                    case "Up":
                        if (_player.SpriteType != Direction.Up
                            || _player.Sprite.State == SpriteState.FinishedAttack)
                        {
                            _player.SpriteType = Direction.Up;
                            if (_player.isDamaged)
                                _player.ChangeSprite(SpriteFactory.Instance.NewDamagedUpWalkingPlayer());
                            else
                            _player.ChangeSprite(SpriteFactory.Instance.NewUpWalkingPlayer());
                        }
                        break;
                    case "Down":
                        if (_player.SpriteType != Direction.Down
                            || _player.Sprite.State == SpriteState.FinishedAttack)
                        {
                            _player.SpriteType = Direction.Down;
                            if (_player.isDamaged)
                                _player.ChangeSprite(SpriteFactory.Instance.NewDamagedDownWalkingPlayer());
                            else
                            _player.ChangeSprite(SpriteFactory.Instance.NewDownWalkingPlayer());
                        }
                        break;
                    case "Left":
                        if (_player.SpriteType != Direction.Left
                            || _player.Sprite.State == SpriteState.FinishedAttack)
                        {
                            _player.SpriteType = Direction.Left;
                            if (_player.isDamaged)
                                _player.ChangeSprite(SpriteFactory.Instance.NewDamagedLeftWalkingPlayer());
                            else
                                _player.ChangeSprite(SpriteFactory.Instance.NewLeftWalkingPlayer());
                        }
                        break;
                    case "Right":
                        if (_player.SpriteType != Direction.Right
                            || _player.Sprite.State == SpriteState.FinishedAttack)
                        {
                            _player.SpriteType = Direction.Right;
                            if (_player.isDamaged)
                                _player.ChangeSprite(SpriteFactory.Instance.NewDamagedRightWalkingPlayer());
                            else
                                _player.ChangeSprite(SpriteFactory.Instance.NewRightWalkingPlayer());
                        }
                        break;
                }
            }
        }
    }
}
