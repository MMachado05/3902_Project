using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Packages.Items;

namespace Project.Enemies.EnemyClasses
{
    public class Aquamentus : Enemy
    {
        private List<ProjectileItem> projectiles = new List<ProjectileItem>();
        public bool hasShot = false;

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

        public override void Attack()
        {
            if (hasShot) return;
            hasShot = true;

            foreach (var direction in GetAttackDirections())
            {
                projectiles.Add(new ProjectileItem(Position, direction, ItemFactory.Instance.CreateFireballSprite(), 30.0f, 600f));
            }
        }

        public override void ResetAttackState()
        {
            hasShot = false;
        }

        public override void UpdateAnimation(GameTime gameTime)
        {
            base.UpdateAnimation(gameTime);
            projectiles.ForEach(p => p.Update());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            projectiles.ForEach(p => p.Draw(spriteBatch));
        }

        public override float GetAttackDuration() => 2f;
    }
}
