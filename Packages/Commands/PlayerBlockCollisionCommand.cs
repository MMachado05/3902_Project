using Project.Blocks;
using Project;

public class PlayerBlockCollisionCommand : ICommand
{
    private Player _player;
    private SolidBlock _block;

    public PlayerBlockCollisionCommand(Player player, SolidBlock block)
    {
        _player = player;
        _block = block;
    }

    public void Execute()
    {
        _player.HandleBlockCollision(_block);
    }
}
