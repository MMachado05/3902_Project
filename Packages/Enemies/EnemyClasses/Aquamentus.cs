using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Factories;
using Project.Items;
using Project.Packages.Sounds;

namespace Project.Enemies.EnemyClasses
{
    public class Aquamentus : Enemy
    {
        private List<ProjectileItem> projectiles = new List<ProjectileItem>();
        public bool hasShot = false;

        public Aquamentus(Rectangle initialPosition) : base(initialPosition)
        {
            Health = 2;
        }

        protected override void LoadAnimations()
        {
            idleLeft = EnemySpriteFactory.Instance.NewAquamentusIdleLeft();
            idleRight = EnemySpriteFactory.Instance.NewAquamentusIdleRight();

            walkLeft = EnemySpriteFactory.Instance.NewAquamentusWalkingLeft();
            walkRight = EnemySpriteFactory.Instance.NewAquamentusWalkingRight();

            attackLeft = EnemySpriteFactory.Instance.NewAquamentusAttackingLeft();
            attackRight = EnemySpriteFactory.Instance.NewAquamentusAttackingRight();
        }

        private Vector2[] GetAttackDirections()
        {
            return lastDirection switch
            {
                Direction.Left => [new Vector2(-1, 0), new Vector2(-0.7f, -0.7f), new Vector2(-0.7f, 0.7f)],
                Direction.Right => [new Vector2(1, 0), new Vector2(0.7f, -0.7f), new Vector2(0.7f, 0.7f)],
                _ => [new Vector2(0, -1)]
            };
        }

        public override void Attack(ItemManager itemManager)
        {
            if (hasShot) return;
            hasShot = true;

            foreach (Vector2 direction in GetAttackDirections())
            {
                Rectangle fireballLocation = new Rectangle(Location.X, Location.Y, Location.Width / 2, Location.Height / 2);
                itemManager.AddProjectile(new ProjectileItem(fireballLocation, direction, ItemFactory.Instance.CreateFireballSprite(), 5.0f, 600f));
            }
            SoundEffectManager.Instance.playFireball();
        }

        public override void ResetAttackState()
        {
            hasShot = false;
        }

        public override void UpdateAnimation(GameTime gameTime)
        {
            base.UpdateAnimation(gameTime);
            projectiles.ForEach(p => p.Update(gameTime));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            projectiles.ForEach(p => p.Draw(spriteBatch));
        }

        public override float GetAttackDuration() => 2f;

        public override List<Direction> PossibleMovementDirections()
        {
            return new List<Direction> { Direction.Left, Direction.Right };
        }
    }
}
