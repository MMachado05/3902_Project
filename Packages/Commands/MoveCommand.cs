namespace Project
{
    public class MoveCommand : ICommand
    {
        private Player _player;
        private Direction _direction;

        public MoveCommand(Player player, Direction direction)
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
                case Direction.Up:
                    dy = -2;
                    break;
                case Direction.Down:
                    dy = 2;
                    break;
                case Direction.Left:
                    dx = -2;
                    break;
                case Direction.Right:
                    dx = 2;
                    break;
            }

            if (_player.Sprite.State != SpriteState.Attacking)
            {
                _player.Move(dx, dy, _direction);

                if (_player.SpriteType != _direction
                    || _player.Sprite.State == SpriteState.FinishedAttack)
                {
                  _player.SpriteType = _direction;
                  _player.ChangeSprite(PlayerSpriteFactory.Instance.NewWalkingPlayerSprite(_direction, _player.isDamaged));
                }
            }
        }
    }
}
