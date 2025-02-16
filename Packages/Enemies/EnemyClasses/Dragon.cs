using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project.Enemies.EnemyClasses
{
    public class Dragon : Enemy
    {
        public Dragon(Vector2 startPosition) : base(startPosition) { }

        protected override void LoadAnimations()
        {
            idleUp = EnemySpriteFactory.Instance.NewDragonIdleUp();
            idleDown = EnemySpriteFactory.Instance.NewDragonIdleDown();
            idleLeft = EnemySpriteFactory.Instance.NewDragonIdleLeft();
            idleRight = EnemySpriteFactory.Instance.NewDragonIdleRight();

            walkUp = EnemySpriteFactory.Instance.NewDragonWalkingUp();
            walkDown = EnemySpriteFactory.Instance.NewDragonWalkingDown();
            walkLeft = EnemySpriteFactory.Instance.NewDragonWalkingLeft();
            walkRight = EnemySpriteFactory.Instance.NewDragonWalkingRight();

            attackUp = EnemySpriteFactory.Instance.NewDragonAttackingUp();
            attackDown = EnemySpriteFactory.Instance.NewDragonAttackingDown();
            attackLeft = EnemySpriteFactory.Instance.NewDragonAttackingLeft();
            attackRight = EnemySpriteFactory.Instance.NewDragonAttackingRight();
        }

        private List<Projectile> projectiles = new List<Projectile>();

        public override void SetAttackAnimation()
        {
            base.SetAttackAnimation();
        }

        public bool hasShot = false;
        public void ShootProjectiles()
        {
            if (hasShot) return;

            hasShot = true;
            Vector2[] directions = GetAttackDirections();
            foreach (var direction in directions)
            {
                projectiles.Add(new Projectile(Position, direction));
            }
        }

        private Vector2[] GetAttackDirections()
        {
            return lastDirection switch
            {
                "Up" => [new Vector2(0, -1), new Vector2(-0.7f, -0.7f), new Vector2(0.7f, -0.7f)],
                "Down" => [new Vector2(0, 1), new Vector2(-0.7f, 0.7f), new Vector2(0.7f, 0.7f)],
                "Left" => [new Vector2(-1, 0), new Vector2(-0.7f, -0.7f), new Vector2(-0.7f, 0.7f)],
                "Right" => [new Vector2(1, 0), new Vector2(0.7f, -0.7f), new Vector2(0.7f, 0.7f)],
                _ => [new Vector2(0, -1)]
            };
        }

        public override void UpdateAnimation(GameTime gameTime)
        {
            base.UpdateAnimation(gameTime);
            foreach (var projectile in projectiles)
            {
                projectile.Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
    }
}