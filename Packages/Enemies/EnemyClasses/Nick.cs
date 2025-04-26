using Microsoft.Xna.Framework;
using Project.Enemies.Helper;
using Project.Enemies.EnemyStateClasses;

namespace Project.Enemies.EnemyClasses
{
    public class Nick : Enemy
    {
        public Nick(Rectangle initialPosition) : base(initialPosition)
        {
            Health = 6;
            Speed = 50.0f;
        }

        protected override EnemyMovement CreateMovement(Rectangle spawnArea)
        {
            return new EnemyMovement(spawnArea, 50.0f);
        }

        protected override EnemyAnimation CreateAnimation()
        {
            return EnemyAnimationFactory.CreateNickAnimation();
        }

        protected override EnemyStateMachine CreateStateMachine()
        {
            return new EnemyStateMachine(new AggressiveMoverAI(), new MovingState(this));
        }
    }
}
