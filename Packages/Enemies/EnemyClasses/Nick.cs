using Microsoft.Xna.Framework;
using Project.Factories;

namespace Project.Enemies.EnemyClasses
{
    public class Nick : Enemy
    {
        public Nick(Rectangle initialPosition) : base(initialPosition)
        {
            Health = 15;
        }

        protected override void LoadAnimations()
        {
            var move = EnemySpriteFactory.Instance.NewNickMoving();

            idleUp = idleDown = idleLeft = idleRight = move;
            walkUp = walkDown = walkLeft = walkRight = move;
            attackUp = attackDown = attackLeft = attackRight = move;
        }
    }
}