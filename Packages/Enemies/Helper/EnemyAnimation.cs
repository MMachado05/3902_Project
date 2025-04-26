using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Sprites;
using Project.Characters;

namespace Project.Enemies.Helper
{
    public class EnemyAnimation
    {
        private readonly ISprite idleUp, idleDown, idleLeft, idleRight;
        private readonly ISprite walkUp, walkDown, walkLeft, walkRight;
        private readonly ISprite attackUp, attackDown, attackLeft, attackRight;

        private ISprite currentAnimation;
        private float elapsedTime;

        public EnemyAnimation(
            ISprite idleUp, ISprite idleDown, ISprite idleLeft, ISprite idleRight,
            ISprite walkUp, ISprite walkDown, ISprite walkLeft, ISprite walkRight,
            ISprite attackUp, ISprite attackDown, ISprite attackLeft, ISprite attackRight)
        {
            this.idleUp = idleUp;
            this.idleDown = idleDown;
            this.idleLeft = idleLeft;
            this.idleRight = idleRight;
            this.walkUp = walkUp;
            this.walkDown = walkDown;
            this.walkLeft = walkLeft;
            this.walkRight = walkRight;
            this.attackUp = attackUp;
            this.attackDown = attackDown;
            this.attackLeft = attackLeft;
            this.attackRight = attackRight;

            currentAnimation = idleDown;
        }

        public void Update(float deltaTime, bool isMoving, Direction facing)
        {
            elapsedTime += deltaTime;

            if (isMoving)
                SetWalkAnimation(facing);
            else
                SetIdleAnimation(facing);

            if (elapsedTime >= 0.25f)
            {
                currentAnimation.Update(null);
                elapsedTime = 0f;
            }
        }

        public void SetIdleAnimation(Direction direction)
        {
            currentAnimation = direction switch
            {
                Direction.Up => idleUp,
                Direction.Down => idleDown,
                Direction.Left => idleLeft,
                Direction.Right => idleRight,
                _ => idleDown
            };
        }

        public void SetWalkAnimation(Direction direction)
        {
            currentAnimation = direction switch
            {
                Direction.Up => walkUp,
                Direction.Down => walkDown,
                Direction.Left => walkLeft,
                Direction.Right => walkRight,
                _ => walkDown
            };
        }

        public void SetAttackAnimation(Direction direction)
        {
            currentAnimation = direction switch
            {
                Direction.Up => attackUp,
                Direction.Down => attackDown,
                Direction.Left => attackLeft,
                Direction.Right => attackRight,
                _ => attackDown
            };
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle location)
        {
            currentAnimation.Draw(spriteBatch, location);
        }
    }
}
