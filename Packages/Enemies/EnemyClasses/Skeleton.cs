using Microsoft.Xna.Framework;

namespace Project.Enemies.EnemyClasses
{
    public class Skeleton : Enemy
    {
        public Skeleton(Vector2 startPosition) : base(startPosition) { }

        protected override void LoadAnimations()
        {
            idleUp = EnemySpriteFactory.Instance.NewSkeletonIdleUp();
            idleDown = EnemySpriteFactory.Instance.NewSkeletonIdleDown();
            idleLeft = EnemySpriteFactory.Instance.NewSkeletonIdleLeft();
            idleRight = EnemySpriteFactory.Instance.NewSkeletonIdleRight();

            walkUp = EnemySpriteFactory.Instance.NewSkeletonWalkingUp();
            walkDown = EnemySpriteFactory.Instance.NewSkeletonWalkingDown();
            walkLeft = EnemySpriteFactory.Instance.NewSkeletonWalkingLeft();
            walkRight = EnemySpriteFactory.Instance.NewSkeletonWalkingRight();

            attackUp = EnemySpriteFactory.Instance.NewSkeletonAttackingUp();
            attackDown = EnemySpriteFactory.Instance.NewSkeletonAttackingDown();
            attackLeft = EnemySpriteFactory.Instance.NewSkeletonAttackingLeft();
            attackRight = EnemySpriteFactory.Instance.NewSkeletonAttackingRight();
        }
    }
}
