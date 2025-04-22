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

        public void Update(IEnemy enemy)
        {
            current.Execute(enemy);

            if (current.IsDone)
                current = ai.DecideNextState(enemy, current);
        }
    }

}
