using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project.Enemies.EnemyClasses
{
    public class Aquamentus : Enemy
    {
        public Aquamentus(Vector2 startPosition) : base(startPosition) { }

        protected override void LoadAnimations()
        {
            idleUp = EnemySpriteFactory.Instance.NewAquamentusIdleUp();
            idleDown = EnemySpriteFactory.Instance.NewAquamentusIdleDown();
            idleLeft = EnemySpriteFactory.Instance.NewAquamentusIdleLeft();
            idleRight = EnemySpriteFactory.Instance.NewAquamentusIdleRight();

            walkUp = EnemySpriteFactory.Instance.NewAquamentusWalkingUp();
            walkDown = EnemySpriteFactory.Instance.NewAquamentusWalkingDown();
            walkLeft = EnemySpriteFactory.Instance.NewAquamentusWalkingLeft();
            walkRight = EnemySpriteFactory.Instance.NewAquamentusWalkingRight();

            attackUp = EnemySpriteFactory.Instance.NewAquamentusAttackingUp();
            attackDown = EnemySpriteFactory.Instance.NewAquamentusAttackingDown();
            attackLeft = EnemySpriteFactory.Instance.NewAquamentusAttackingLeft();
            attackRight = EnemySpriteFactory.Instance.NewAquamentusAttackingRight();
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