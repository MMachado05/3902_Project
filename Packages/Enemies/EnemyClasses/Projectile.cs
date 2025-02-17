using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Enemies.EnemyClasses
{
    public class Projectile
    {
        public Vector2 Position;
        public Vector2 Direction;
        public float Speed = 20.0f;
        private ISprite sprite;

        public Projectile(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
            sprite = EnemySpriteFactory.Instance.NewFireball();
        }

        public void Update()
        {
            Position += Direction * Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
    }
}