using Project.Characters;
using Project.Characters.Enums;
using Project.Factories;

namespace Project.Commands.PlayerCommands
{
    public class UpdateVelocityCommand : ICommand
    {
        private Player _player;
        private Direction _direction;
        private bool _deregister;
        private int _dx;
        private int _dy;
        private bool _useX;
        private bool _useY;

        public UpdateVelocityCommand(Player player, Direction direction, bool deregister,
            int dx, int dy, bool useX, bool useY)
        {
            _direction = direction;
            _player = player;
            this._deregister = deregister;
            this._dx = dx;
            this._dy = dy;
            this._useX = useX;
            this._useY = useY;
        }

        public void Execute()
        {
            // Register direction for associated velocity change
            if (!this._deregister)
                this._player.RegisterDirection(this._direction, this._dx, this._dy);
            else
                this._player.DeregisterDirection(this._direction);

            _player.ChangeSprite(PlayerSpriteFactory.Instance.NewWalkingPlayerSprite(this._player.LastActiveDirection, _player.isDamaged));

            _player.SpriteType = _direction;
            _player.Sprite.State = CharacterState.Walking;
        }
    }
}
