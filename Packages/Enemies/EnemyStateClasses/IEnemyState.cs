using Project.Items;

namespace Project.Enemies.EnemyStateClasses
{
    public interface IEnemyState
    {
        void Execute(IEnemy enemy, ItemManager itemManager);
        bool IsDone { get; }
        StateId Id { get; }
    }
}
