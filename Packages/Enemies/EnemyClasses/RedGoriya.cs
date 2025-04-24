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
        private List<ProjectileItem> projectiles = new List<ProjectileItem>();
        private bool hasThrownBoomerang = false;

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

        public override void Attack()
        {
            if (hasThrownBoomerang) return;

            hasThrownBoomerang = true;
            Vector2 direction = GetAttackDirection();
            Rectangle boomerangLocation = new Rectangle(Location.X, Location.Y, Location.Width / 2, Location.Height / 2);
            projectiles.Add(new ProjectileItem(boomerangLocation, direction, ItemFactory.Instance.CreateBoomerangSprite(), 5.0f, 10.0f));
            SoundEffectManager.Instance.playBoomerang();
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
                projectiles[i].Update(gameTime);
                if (projectiles[i].HasReturned())
                {
                    projectiles.RemoveAt(i);
                    hasThrownBoomerang = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            projectiles.ForEach(p => p.Draw(spriteBatch));
        }

        public override List<ProjectileItem> GetProjectiles()
        {
            return projectiles;
        }
    }
}
