using Project.Characters;
using Project.Characters.Enums;
using Project.Factories;

namespace Project.Commands.PlayerCommands
{
    public class UpdateVelocityCommand : ICommand
    {
        private Player _player;
        private Direction _direction;
        private int _dx;
        private int _dy;
        private bool _useX;
        private bool _useY;

        public UpdateVelocityCommand(Player player, Direction direction,
            int dx, int dy, bool useX, bool useY)
        {
            _direction = direction;
            _player = player;
            this._dx = dx;
            this._dy = dy;
            this._useX = useX;
            this._useY = useY;
        }

        public void Execute()
        {
            // Early escape for mid-attack
            if (_player.Sprite.State == CharacterState.Attacking)
                return;

            if (_player.Sprite.State != CharacterState.Walking
                || (_player.Sprite.State == CharacterState.Walking
                  && _direction != _player.LastDirection))
                _player.ChangeSprite(PlayerSpriteFactory.Instance.NewWalkingPlayerSprite(_direction, _player.isDamaged));

            _player.SpriteType = _direction;
            _player.Sprite.State = CharacterState.Walking;

            _player.UpdateVelocity(_dx, _dy, _useX, _useY, _direction);
        }
    }
}
