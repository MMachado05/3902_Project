using System;

namespace Project
{
    public class AttackCommand : ICommand
    {
        private Game1 _game;
        private string _direction;

        public AttackCommand(Game1 game, string direction)
        {
            _game = game;
            _direction = direction;
        }

        public void Execute()
        {
            Console.WriteLine("spriteState: " + this._game.spriteState);
            switch (_direction)
            {
                case "Up":
                if (_game.spriteState != SpriteState.Attacking){
                    _game.spriteState = SpriteState.Attacking;
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewUpAttackingPlayer());
                }
                    break;
                case "Down":
                if (_game.spriteState != SpriteState.Attacking){
                    _game.spriteState = SpriteState.Attacking;
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewDownAttackingPlayer());
                }
                    break;
                case "Left":
                if (_game.spriteState != SpriteState.Attacking){
                    _game.spriteState = SpriteState.Attacking;
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewLeftAttackingPlayer());
                }
                    break;
                case "Right":
                if (_game.spriteState != SpriteState.Attacking){
                    _game.spriteState = SpriteState.Attacking;
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewRightAttackingPlayer());
                }
                    break;
            }
            
        }
    }
}
