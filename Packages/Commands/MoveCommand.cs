using Microsoft.Xna.Framework;

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

            switch (_direction){
                case "Up": dy = -2; 
            break;
                case "Down": dy = 2;
            break;
                case "Left": dx = -2;
            break;
                case "Right": dx = 2;
            break;
            }

            if (_player.Sprite.State != SpriteState.Attacking){
              _player.Move(dx, dy, _direction);

              switch (_direction)
              {
                case "Up":
                  if (_player.SpriteType != SpriteType.WalkingUp)
                  {
                    _player.SpriteType = SpriteType.WalkingUp;
                    _player.ChangeSprite(SpriteFactory.Instance.NewUpWalkingPlayer());
                  }
                  break;
                case "Down":
                  if (_player.SpriteType != SpriteType.WalkingDown)
                  {
                    _player.SpriteType = SpriteType.WalkingDown;
                    _player.ChangeSprite(SpriteFactory.Instance.NewDownWalkingPlayer());
                  }
                  break;
                case "Left":
                  if (_player.SpriteType != SpriteType.WalkingLeft)
                  {
                    _player.SpriteType = SpriteType.WalkingLeft;
                    _player.ChangeSprite(SpriteFactory.Instance.NewLeftWalkingPlayer());
                  }
                  break;
                case "Right":
                  if (_player.SpriteType != SpriteType.WalkingRight)
                  {
                    _player.SpriteType = SpriteType.WalkingRight;
                    _player.ChangeSprite(SpriteFactory.Instance.NewRightWalkingPlayer());
                  }
                  break;
              }
            }
        }
    }
}
