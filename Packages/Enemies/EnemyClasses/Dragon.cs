using Microsoft.Xna.Framework;

namespace Project.Enemies.EnemyClasses
{
    public class Dragon : Enemy
    {
        public Dragon(Vector2 startPosition) : base(startPosition) { }

        protected override void LoadAnimations()
        {
            idleUp = EnemySpriteFactory.Instance.NewDragonIdleUp();
            idleDown = EnemySpriteFactory.Instance.NewDragonIdleDown();
            idleLeft = EnemySpriteFactory.Instance.NewDragonIdleLeft();
            idleRight = EnemySpriteFactory.Instance.NewDragonIdleRight();

            walkUp = EnemySpriteFactory.Instance.NewDragonWalkingUp();
            walkDown = EnemySpriteFactory.Instance.NewDragonWalkingDown();
            walkLeft = EnemySpriteFactory.Instance.NewDragonWalkingLeft();
            walkRight = EnemySpriteFactory.Instance.NewDragonWalkingRight();

            attackUp = EnemySpriteFactory.Instance.NewDragonAttackingUp();
            attackDown = EnemySpriteFactory.Instance.NewDragonAttackingDown();
            attackLeft = EnemySpriteFactory.Instance.NewDragonAttackingLeft();
            attackRight = EnemySpriteFactory.Instance.NewDragonAttackingRight();
        }
    }
}
