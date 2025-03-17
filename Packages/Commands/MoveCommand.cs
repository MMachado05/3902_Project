using System.Collections.Generic;

namespace Project
{
    public class MoveCommand : ICommand
    {
        private Player _player;
        private string _direction;
        private Dictionary<string, Direction> stringToEnum;

        public MoveCommand(Player player, string direction)
        {
            _direction = direction;
            _player = player;
            stringToEnum = new Dictionary<string, Direction>();
            stringToEnum.Add("Up", Direction.Up);
            stringToEnum.Add("Down", Direction.Down);
            stringToEnum.Add("Right", Direction.Right);
            stringToEnum.Add("Left", Direction.Left);
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

                if (_player.SpriteType != stringToEnum[_direction]
                    || _player.Sprite.State == SpriteState.FinishedAttack)
                {
                  _player.SpriteType = stringToEnum[_direction];
                  _player.ChangeSprite(PlayerSpriteFactory.Instance.NewWalkingPlayerSprite(stringToEnum[_direction], _player.isDamaged));
                }
            }
        }
    }
}
