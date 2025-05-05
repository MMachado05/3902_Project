using Project.Items;

namespace Project.Enemies.EnemyStateClasses
{
    public interface IEnemyState
    {
        void Execute(IEnemy enemy, float deltaTime, ItemManager itemManager = null);
        bool IsDone { get; }
        StateId Id { get; }
    }
}
