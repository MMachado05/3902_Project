using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Factories;
using Project.Items;

namespace Project.Enemies.EnemyClasses
{
    public class Stalfos : Enemy
    {
        public Stalfos(Rectangle initialPosition) : base(initialPosition) {
            Health = 1;
        }

        protected override void LoadAnimations()
        {
            idleUp = EnemySpriteFactory.Instance.NewStalfosIdleUp();
            idleDown = EnemySpriteFactory.Instance.NewStalfosIdleDown();
            idleLeft = EnemySpriteFactory.Instance.NewStalfosIdleLeft();
            idleRight = EnemySpriteFactory.Instance.NewStalfosIdleRight();

            walkUp = EnemySpriteFactory.Instance.NewStalfosWalkingUp();
            walkDown = EnemySpriteFactory.Instance.NewStalfosWalkingDown();
            walkLeft = EnemySpriteFactory.Instance.NewStalfosWalkingLeft();
            walkRight = EnemySpriteFactory.Instance.NewStalfosWalkingRight();

            attackUp = EnemySpriteFactory.Instance.NewStalfosAttackingUp();
            attackDown = EnemySpriteFactory.Instance.NewStalfosAttackingDown();
            attackLeft = EnemySpriteFactory.Instance.NewStalfosAttackingLeft();
            attackRight = EnemySpriteFactory.Instance.NewStalfosAttackingRight();
        }
        public override List<ProjectileItem> GetProjectiles()
        {
            return new List<ProjectileItem>();
        }
    }
}
