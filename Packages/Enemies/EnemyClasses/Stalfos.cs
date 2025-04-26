using Microsoft.Xna.Framework;
using Project.Enemies.Helper;
using Project.Enemies.EnemyStateClasses;

namespace Project.Enemies.EnemyClasses
{
    public class Stalfos : Enemy
    {
        public Stalfos(Rectangle initialPosition) : base(initialPosition)
        {
            Health = 1;
            Speed = 100.0f;
        }

        protected override EnemyMovement CreateMovement(Rectangle spawnArea)
        {
            return new EnemyMovement(spawnArea, 100.0f);
        }

        protected override EnemyAnimation CreateAnimation()
        {
            return EnemyAnimationFactory.CreateStalfosAnimation();
        }

        protected override EnemyStateMachine CreateStateMachine()
        {
            return new EnemyStateMachine(new AggressiveMoverAI(), new MovingState(this));
        }
    }
}
