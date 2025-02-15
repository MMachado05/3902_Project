using Microsoft.Xna.Framework;

namespace Project.Enemies.EnemyClasses
{
    public class Goblin : Enemy
    {
        public Goblin(Vector2 startPosition) : base(startPosition) { }

        protected override void LoadAnimations()
        {
            idleUp = EnemySpriteFactory.Instance.NewGoblinIdleUp();
            idleDown = EnemySpriteFactory.Instance.NewGoblinIdleDown();
            idleLeft = EnemySpriteFactory.Instance.NewGoblinIdleLeft();
            idleRight = EnemySpriteFactory.Instance.NewGoblinIdleRight();

            walkUp = EnemySpriteFactory.Instance.NewGoblinWalkingUp();
            walkDown = EnemySpriteFactory.Instance.NewGoblinWalkingDown();
            walkLeft = EnemySpriteFactory.Instance.NewGoblinWalkingLeft();
            walkRight = EnemySpriteFactory.Instance.NewGoblinWalkingRight();

            attackUp = EnemySpriteFactory.Instance.NewGoblinAttackingUp();
            attackDown = EnemySpriteFactory.Instance.NewGoblinAttackingDown();
            attackLeft = EnemySpriteFactory.Instance.NewGoblinAttackingLeft();
            attackRight = EnemySpriteFactory.Instance.NewGoblinAttackingRight();
        }
    }
}
