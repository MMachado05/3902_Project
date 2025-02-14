using System;

namespace Project
{
    public class AttackCommand : ICommand
    {
        private Game1 _game;

        public AttackCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            string _direction = _game.lastDirection; // Needed to be in update() method, since in constructor != what direction user is facing at runtime

            Console.WriteLine("spriteState: " + this._game.playerSprite.State);
            switch (_direction)
            {
                case "Up":
                if (_game.playerSprite.State != SpriteState.Attacking){
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewUpAttackingPlayer());
                }
                    break;
                case "Down":
                if (_game.playerSprite.State != SpriteState.Attacking){
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewDownAttackingPlayer());
                }
                    break;
                case "Left":
                if (_game.playerSprite.State != SpriteState.Attacking){
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewLeftAttackingPlayer());
                }
                    break;
                case "Right":
                if (_game.playerSprite.State != SpriteState.Attacking){
                    _game.ChangePlayerSprite(SpriteFactory.Instance.NewRightAttackingPlayer());
                }
                    break;
            }
            
        }
    }
}
