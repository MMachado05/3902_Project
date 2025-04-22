namespace Project.Enemies.EnemyStateClasses
{
    public class EnemyStateMachine
    {
        private IEnemyState current;
        private readonly IEnemyAI ai;

        public EnemyStateMachine(IEnemyAI controller)
        {
            ai = controller;
            current = new IdleState();
        }

        public void Update(IEnemy enemy)
        {
            current.Execute(enemy);

            if (current.IsDone)
                current = ai.DecideNextState(enemy, current);
        }
    }
}
