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

        public void Update(IEnemy enemy, float deltaTime, ItemManager itemManager = null)
        {
            current.Execute(enemy, deltaTime, itemManager);

            if (current.IsDone)
                current = ai.DecideNextState(enemy, current, deltaTime);
        }

        public void OverrideState(IEnemyState newState)
        {
            current = newState;
        }
    }
}
