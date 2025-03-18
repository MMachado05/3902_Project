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
            this._player.SetStaticSprite();
        }
    }
}
