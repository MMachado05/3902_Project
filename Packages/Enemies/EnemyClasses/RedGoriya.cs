using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Factories;
using Project.Items;
using Project.Packages.Sounds;

namespace Project.Enemies.EnemyClasses
{
    public class RedGoriya : Enemy
    {
        private bool hasThrownBoomerang = false;
        private ProjectileItem _activeBoomerang;

        public RedGoriya(Rectangle initialPosition) : base(initialPosition)
        {
            Health = 1;
        }

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

        private Vector2 GetAttackDirection()
        {
            return lastDirection switch
            {
                Direction.Up => new Vector2(0, -1),
                Direction.Down => new Vector2(0, 1),
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                _ => new Vector2(0, -1)
            };
        }

        public override void Attack(ItemManager itemManager)
        {
            if (hasThrownBoomerang) return;

            hasThrownBoomerang = true;

            Rectangle boomerangLocation = new Rectangle(
                Location.X,
                Location.Y,
                Location.Width / 2,
                Location.Height / 2
                );

            _activeBoomerang = new ProjectileItem(
                  boomerangLocation,
                  GetAttackDirection(),
                  ItemFactory.Instance.CreateBoomerangSprite(),
                  5.0f,
                  150.0f
                  );


            itemManager.AddProjectile(_activeBoomerang);

            SoundEffectManager.Instance.playBoomerang();
        }

        public override void ResetAttackState()
        {
            hasThrownBoomerang = false;
        }

        public override void UpdateAnimation(GameTime gameTime)
        {
            base.UpdateAnimation(gameTime);
            if (_activeBoomerang.HasReturned())
                hasThrownBoomerang = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
