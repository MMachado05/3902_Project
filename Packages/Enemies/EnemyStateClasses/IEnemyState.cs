namespace Project.Enemies.EnemyStateClasses
{
    public interface IEnemyState
    {
        void Execute(IEnemy enemy);
        bool IsDone { get; }
        StateId Id { get; }
    }

}
