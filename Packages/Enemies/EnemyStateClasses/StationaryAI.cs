using Project.Enemies.EnemyClasses;
using Project.Enemies.EnemyStateClasses;

namespace Project.Enemies.EnemyStateClasses
{
    public class StationaryAI : IEnemyAI
    {
        public IEnemyState DecideNextState(IEnemy enemy, IEnemyState currentState)
        {
            return new IdleState();
        }
    }
}
