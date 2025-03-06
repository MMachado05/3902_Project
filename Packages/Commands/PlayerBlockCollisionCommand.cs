using Project.Blocks;
using Project;
using Project.Packages.Characters;

public class PlayerBlockCollisionCommand : ICommand
{
    private Player _player;
    private SolidBlock _block;
    private CollisionManager _collisionManager;

    public PlayerBlockCollisionCommand(Player player, SolidBlock block, CollisionManager collisionManager)
    {
        _player = player;
        _block = block;
        _collisionManager = collisionManager;
    }

    public void Execute()
    {
        _collisionManager.HandleBlockCollision(_player, _block);
    }
}
