using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Packages.Items;

namespace Project.Enemies.EnemyClasses
{
    public class RedGoriya : Enemy
    {
        private List<ProjectileItem> projectiles = new List<ProjectileItem>();
        private bool hasThrownBoomerang = false;

        public RedGoriya(Rectangle initialPosition) : base(initialPosition) { }

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

        public override void Attack()
        {
            if (hasThrownBoomerang) return;

            hasThrownBoomerang = true;
            Vector2 direction = GetAttackDirection();
            projectiles.Add(new ProjectileItem(Position, direction, ItemFactory.Instance.CreateBoomerangSprite(), 30.0f, 150.0f));
        }

        public override void ResetAttackState()
        {
            hasThrownBoomerang = false;
        }

        public override void UpdateAnimation(GameTime gameTime)
        {
            base.UpdateAnimation(gameTime);
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();
                /*if (projectiles[i].HasReturned())*/
                /*{*/
                /*    projectiles.RemoveAt(i);*/
                /*    hasThrownBoomerang = false;*/
                /*}*/
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            projectiles.ForEach(p => p.Draw(spriteBatch, p.PositionRect));
        }

    }
}
