namespace Project
{
    public class StopPlayerCommand : ICommand
    {
        private Player _player;

        public StopPlayerCommand(Player player)
        {
            this._player = player;
        }

        public void Execute()
        {
            if (this._player.Sprite.State != CharacterState.Attacking)
                this._player.SetStaticSprite();
        }
    }
}
