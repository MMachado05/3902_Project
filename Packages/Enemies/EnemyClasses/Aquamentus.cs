using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Project.Factories;
using Project.Items;
using Project.Packages.Sounds;
using Project.Enemies.Helper;
using Project.Characters;

namespace Project.Enemies.EnemyClasses
{
    public class Aquamentus : Enemy
    {
        private bool hasShot = false;

        private readonly ItemManager itemManager;

        public Aquamentus(Rectangle initialPosition, ItemManager itemManager) : base(initialPosition)
        {
            this.itemManager = itemManager;
            Health = 2;
            Speed = 100.0f;
            Shooter = new ProjectileShooter(itemManager);
        }


        protected override EnemyMovement CreateMovement(Rectangle spawnArea)
        {
            return new EnemyMovement(spawnArea, 100.0f);
        }

        protected override EnemyAnimation CreateAnimation()
        {
            return EnemyAnimationFactory.CreateAquamentusAnimation();
        }

        private IEnumerable<Vector2> GetAttackDirections()
        {
            return Movement.LastDirection switch
            {
                Direction.Left => new[] { new Vector2(-1, 0), new Vector2(-0.7f, -0.7f), new Vector2(-0.7f, 0.7f) },
                Direction.Right => new[] { new Vector2(1, 0), new Vector2(0.7f, -0.7f), new Vector2(0.7f, 0.7f) },
                _ => new[] { new Vector2(0, -1) }
            };
        }

        public override void Attack(ItemManager itemManager)
        {
            if (hasShot) return;
            hasShot = true;

            Shooter.Shoot(Location, ItemFactory.Instance.CreateFireballSprite(), GetAttackDirections());

            SoundEffectManager.Instance.playFireball();
        }

        public override void ResetAttackState() => hasShot = false;
        public override float GetAttackDuration() => 2f;
        public override List<Direction> PossibleMovementDirections()
        {
            return new List<Direction> { Direction.Left, Direction.Right };
        }
    }
}
