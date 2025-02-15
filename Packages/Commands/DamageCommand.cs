using System;

namespace Project
{
    public class DamageCommand : ICommand
    {
        private Player _player;

        public DamageCommand(Player player)
        {
            _player = player;
        }

        public void Execute()
        {
            string direction = _player.LastDirection;

            if (_player.Sprite.State == SpriteState.Attacking)
            {
                switch (direction)
                {
                    case "Up":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedUpAttackingPlayer());
                        break;
                    case "Right":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedRightAttackingPlayer());
                        break;
                    case "Down":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedDownAttackingPlayer());
                        break;
                    case "Left":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedLeftAttackingPlayer());
                        break;
                }
            }
            else if (_player.Sprite.State == SpriteState.Walking)
            {
                switch (direction)
                {
                    case "Up":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedUpWalkingPlayer());
                        break;
                    case "Right":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedRightWalkingPlayer());
                        break;
                    case "Down":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedDownWalkingPlayer());
                        break;
                    case "Left":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedLeftWalkingPlayer());
                        break;
                }
            }
            else // If the player is idle, apply the damaged stopped sprite
            {
                switch (direction)
                {
                    case "Up":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedUpStoppedPlayer());
                        break;
                    case "Right":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedRightStoppedPlayer());
                        break;
                    case "Down":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedDownStoppedPlayer());
                        break;
                    case "Left":
                        _player.ChangeSprite(SpriteFactory.Instance.NewDamagedLeftStoppedPlayer());
                        break;
                }
            }
        }
    }
}
