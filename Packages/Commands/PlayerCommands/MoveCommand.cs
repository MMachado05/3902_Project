using Project.Characters;
using Project.Characters.Enums;
using Project.Factories;

namespace Project.Commands.PlayerCommands
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
            // Early escape for mid-attack
            if (_player.Sprite.State == CharacterState.Attacking)
                return;

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

            if (_player.Sprite.State != CharacterState.Walking
                || (_player.Sprite.State == CharacterState.Walking
                  && _direction != _player.LastDirection))
                _player.ChangeSprite(PlayerSpriteFactory.Instance.NewWalkingPlayerSprite(_direction, _player.isDamaged));

            _player.SpriteType = _direction;
            _player.Sprite.State = CharacterState.Walking;

            _player.Move(dx, dy, _direction);
        }
    }
}
