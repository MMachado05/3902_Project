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
            switch (_direction)
            {
                case "Up":
                    _game.MovePlayer(0, -5, "Up");
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewUpWalkingPlayer());
                    break;
                case "Down":
                    _game.MovePlayer(0, 5, "Down");
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewDownWalkingPlayer());
                    break;
                case "Left":
                    _game.MovePlayer(-5, 0, "Left");
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewLeftWalkingPlayer());
                    break;
                case "Right":
                    _game.MovePlayer(5, 0, "Right");
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewRightWalkingPlayer());
                    break;
            }
        }
    }
}
