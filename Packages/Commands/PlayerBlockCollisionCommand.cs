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
    // NOTE: From Boggus: Any collision manager that has to consider *sides* of a collision,
    // should (per Boggus' personal opinion) take care of that itself, and commands should
    // just be simple and call one thing.
    // We don't *need* four different commands for four sides; that really depends on how
    // we implement our collision-handling methods in the handler class(es).
    // We can also consider passing in parameters to our Execute() method, but this would
    // require a new interface. Maybe just call some method on Player, Block, etc... Keeps
    // the command simple and single-interfaced.
    // Heck, you don't even need a command for this if you *really* don't want one, since
    // there's already some necessary coupling here that the Command design pattern doesn't
    // necessarily fix.
}
