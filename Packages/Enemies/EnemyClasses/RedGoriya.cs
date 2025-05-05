using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Project.Factories;
using Project.Items;
using Project.Packages.Sounds;
using Project.Enemies.Helper;
using Project.Characters;

namespace Project.Enemies.EnemyClasses
{
    public class RedGoriya : Enemy
    {
        private bool hasThrownBoomerang = false;

        private readonly ItemManager itemManager;

        public RedGoriya(Rectangle initialPosition, ItemManager itemManager) : base(initialPosition)
        {
            this.itemManager = itemManager;
            Health = 1;
            Speed = 100.0f;
            Shooter = new ProjectileShooter(itemManager, 5f, 150f);
        }


        protected override EnemyMovement CreateMovement(Rectangle spawnArea)
        {
            return new EnemyMovement(spawnArea, 100.0f);
        }

        protected override EnemyAnimation CreateAnimation()
        {
            return EnemyAnimationFactory.CreateRedGoriyaAnimation();
        }

        private IEnumerable<Vector2> GetAttackDirection()
        {
            return new[] { Movement.LastDirection switch
            {
                Direction.Up => new Vector2(0, -1),
                Direction.Down => new Vector2(0, 1),
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                _ => new Vector2(0, -1)
            }};
        }

        public override void Attack(ItemManager itemManager)
        {
            if (hasThrownBoomerang) return;
            hasThrownBoomerang = true;

            Shooter.Shoot(Location, ItemFactory.Instance.CreateBoomerangSprite(), GetAttackDirection());

            SoundEffectManager.Instance.playBoomerang();
        }


        public override void ResetAttackState() => hasThrownBoomerang = false;
        public override float GetAttackDuration() => 1.5f;
    }
}
