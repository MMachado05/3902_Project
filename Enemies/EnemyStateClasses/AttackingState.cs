using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies.EnemyStateClasses
{
    public class AttackingState : IEnemyState
    {
        private float attackDuration = 1.0f;
        private float elapsedTime = 0;

        public void Update(IEnemy enemy)
        {
            elapsedTime += 1f;
            if (elapsedTime >= attackDuration)
            {
                enemy.SetState(new IdleState());
                elapsedTime = 0;
            }
        }
    }
}
