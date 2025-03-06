using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public class ProjectileItem : Item
    {
        public override Vector2 Position { get; set; }
        public override float Speed { get; set; }
        public Vector2 Direction { get; }
        private readonly Vector2 initialPosition;
        private readonly float maxDistance;
        private bool returning;
        public readonly Vector2? returnTarget;

        public ProjectileItem(Vector2 position, Vector2 direction, ISprite sprite, float speed, float maxDistance, Vector2? returnTarget = null)
            : base(sprite)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
            this.maxDistance = maxDistance;
            this.returnTarget = returnTarget;
            initialPosition = position;
            returning = false;
        }

        public override void Update()
        {
            if (!returning && Vector2.Distance(initialPosition, Position) >= maxDistance)
            {
                returning = returnTarget.HasValue;
            }

            Vector2 moveDirection = returning && returnTarget.HasValue
                ? Vector2.Normalize(returnTarget.Value - Position)
                : Direction;

            Position += moveDirection * Speed;
            Sprite.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public bool HasReturned() => returning && returnTarget.HasValue && Vector2.Distance(Position, returnTarget.Value) < 5.0f;
    }
}
