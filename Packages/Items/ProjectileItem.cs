
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Sprites;
using Project.Items;

namespace Project.Items
{
    public class ProjectileItem : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }
        public virtual int PlayerHealthEffect { get => -1; }
        public Vector2 VectorDirection { get; set; }
        private readonly Rectangle initialPosition;
        private readonly float maxDistance;
        private bool returning;
        public readonly Vector2? returnTarget;
        public float Speed;

        public ProjectileItem(Rectangle position, Vector2 vectorDirection, ISprite sprite, float speed, float maxDistance)
            : base(sprite)
        {
            Location = position;
            VectorDirection = vectorDirection;
            Speed = speed;
            this.maxDistance = maxDistance;
            initialPosition = position;
            returning = false;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 currentPosition = new Vector2(Location.X, Location.Y);

            if (!returning && Vector2.Distance(new Vector2(initialPosition.X, initialPosition.Y), currentPosition) >= maxDistance)
            {
                returning = returnTarget.HasValue;
            }

            Vector2 moveDirection = returning && returnTarget.HasValue
                ? Vector2.Normalize(returnTarget.Value - currentPosition)
                : VectorDirection;

            Location = new Rectangle(
                (int)(currentPosition.X + moveDirection.X * Speed),
                (int)(currentPosition.Y + moveDirection.Y * Speed),
                Location.Width,
                Location.Height
            );

            Sprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public bool HasReturned() =>
            returning && returnTarget.HasValue && Vector2.Distance(new Vector2(Location.X, Location.Y), returnTarget.Value) < 5.0f;
    }
}
