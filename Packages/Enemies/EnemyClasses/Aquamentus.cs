using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Factories;
using Project.Items;

namespace Project.Enemies.EnemyClasses
{
    public class Aquamentus : Enemy
    {
        private List<ProjectileItem> projectiles = new List<ProjectileItem>();
        public bool hasShot = false;

        public Aquamentus(Rectangle initialPosition) : base(initialPosition) { }

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

        public override void Attack()
        {
            if (hasShot) return;
            hasShot = true;

            foreach (var direction in GetAttackDirections())
            {
                /*projectiles.Add(new ProjectileItem(Position, direction, ItemFactory.Instance.CreateFireballSprite(), 30.0f, 600f));*/
            }
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
            /*projectiles.ForEach(p => p.Draw(spriteBatch));*/
        }

        public override float GetAttackDuration() => 2f;
    }
}
