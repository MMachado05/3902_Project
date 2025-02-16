using Microsoft.Xna.Framework;

namespace Project.Enemies.EnemyClasses
{
    public class RedGoriya : Enemy
    {
        public RedGoriya(Vector2 startPosition) : base(startPosition) { }

        protected override void LoadAnimations()
        {
            idleUp = EnemySpriteFactory.Instance.NewRedGoriyaIdleUp();
            idleDown = EnemySpriteFactory.Instance.NewRedGoriyaIdleDown();
            idleLeft = EnemySpriteFactory.Instance.NewRedGoriyaIdleLeft();
            idleRight = EnemySpriteFactory.Instance.NewRedGoriyaIdleRight();

            walkUp = EnemySpriteFactory.Instance.NewRedGoriyaWalkingUp();
            walkDown = EnemySpriteFactory.Instance.NewRedGoriyaWalkingDown();
            walkLeft = EnemySpriteFactory.Instance.NewRedGoriyaWalkingLeft();
            walkRight = EnemySpriteFactory.Instance.NewRedGoriyaWalkingRight();

            attackUp = EnemySpriteFactory.Instance.NewRedGoriyaAttackingUp();
            attackDown = EnemySpriteFactory.Instance.NewRedGoriyaAttackingDown();
            attackLeft = EnemySpriteFactory.Instance.NewRedGoriyaAttackingLeft();
            attackRight = EnemySpriteFactory.Instance.NewRedGoriyaAttackingRight();
        }
    }
}
