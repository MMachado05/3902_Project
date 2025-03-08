using Project.Blocks;
using Project;
using Project.Enemies.EnemyClasses;
using Project.Enemies;

public class EnemyBlockCollisionCommand : ICommand
{
    private Enemy _enemy;
    private SolidBlock _block;
    private EnemyCollisionManager _collisionManager;

    public EnemyBlockCollisionCommand(Enemy enemy, SolidBlock block, EnemyCollisionManager collisionManager)
    {
        _enemy = enemy;
        _block = block;
        _collisionManager = collisionManager;
    }

    public void Execute()
    {
        _collisionManager.HandleBlockCollision(_enemy, _block);
    }
}
