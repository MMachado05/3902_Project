using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies.EnemyStateClasses
{
    public class IdleState : IEnemyState
    {
        private float timer = 0;

        public void Update(IEnemy enemy)
        {
            timer += 0.75f;
            if (timer > 2)
            {
                enemy.SetState(new MovingState());
                timer = 0;
            }
        }
    }
}
