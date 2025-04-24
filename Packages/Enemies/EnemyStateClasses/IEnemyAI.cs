namespace Project.Enemies.EnemyStateClasses
{
    public interface IEnemyAI
    {
        IEnemyState DecideNextState(IEnemy enemy, IEnemyState current);
    }
}
