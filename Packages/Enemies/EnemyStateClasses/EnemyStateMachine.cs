using Project.Items;

namespace Project.Enemies.EnemyStateClasses
{
    public class EnemyStateMachine
    {
        private IEnemyState current;
        private readonly IEnemyAI ai;

        public EnemyStateMachine(IEnemyAI controller, IEnemyState initialState)
        {
            ai = controller;
            current = initialState;
        }

        public void Update(IEnemy enemy, ItemManager itemManager)
        {
            current.Execute(enemy, itemManager);

            if (current.IsDone)
                current = ai.DecideNextState(enemy, current);
        }
    }

}
