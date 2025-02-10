using Microsoft.Xna.Framework;

namespace Project
{
    public class MoveCommand : ICommand
    {
        private Game1 _game;
        private string _direction;

        public MoveCommand(Game1 game, string direction)
        {
            _game = game;
            _direction = direction;
        }

        public void Execute()
        {
            // Maybe avoid hardcoding speed
            switch (_direction)
            {
                case "Up":
                    _game.MovePlayer(0, -2, "Up");
                    if (_game.spriteType != SpriteType.WalkingUp)
                    {
                        _game.spriteType = SpriteType.WalkingUp;
                        _game.ChangePlayerSprite(SpriteFactory.Instance.NewUpWalkingPlayer());
                    }
                    break;
                case "Down":
                    _game.MovePlayer(0, 2, "Down");
                    if (_game.spriteType != SpriteType.WalkingDown)
                    {
                        _game.spriteType = SpriteType.WalkingDown;
                        _game.ChangePlayerSprite(SpriteFactory.Instance.NewDownWalkingPlayer());
                    }
                    break;
                case "Left":
                    _game.MovePlayer(-2, 0, "Left");
                    if (_game.spriteType != SpriteType.WalkingLeft)
                    {
                        _game.spriteType = SpriteType.WalkingLeft;
                        _game.ChangePlayerSprite(SpriteFactory.Instance.NewLeftWalkingPlayer());
                    }
                    break;
                case "Right":
                    _game.MovePlayer(2, 0, "Right");
                    if (_game.spriteType != SpriteType.WalkingRight)
                    {
                        _game.spriteType = SpriteType.WalkingRight;
                        _game.ChangePlayerSprite(SpriteFactory.Instance.NewRightWalkingPlayer());
                    }
                    break;
            }
        }
    }
}
