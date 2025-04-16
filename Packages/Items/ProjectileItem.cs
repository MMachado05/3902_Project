using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Sprites;

namespace Project.Items
{
    public class ProjectileItem : Item
    {
        public override Rectangle Location { get; set; }
        public override Direction Direction { get; set; }
        private readonly Rectangle initialPosition;
        private readonly float maxDistance;
        private bool returning;
        public readonly Vector2? returnTarget;

        public ProjectileItem(Rectangle position, Direction direction, ISprite sprite, float maxDistance)
            : base(sprite)
        {
            Location = position;
            Direction = direction;
            this.maxDistance = maxDistance;
            /*this.returnTarget = returnTarget;*/
            // TODO: Reimplement return targets with rectangles
            initialPosition = position;
            returning = false;
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Use rectangles here
            /*if (!returning && Vector2.Distance(initialPosition, Position) >= maxDistance)*/
            /*{*/
            /*    returning = returnTarget.HasValue;*/
            /*}*/

            /*Vector2 moveDirection = returning && returnTarget.HasValue*/
            /*    ? Vector2.Normalize(returnTarget.Value - Position)*/
            /*    : Direction;*/
            /**/
            /*Position += moveDirection * Speed;*/
            /*Sprite.Update();*/
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        // TODO: Use rectangles
        /*public bool HasReturned() => returning && returnTarget.HasValue && Vector2.Distance(Position, returnTarget.Value) < 5.0f;*/
    }
}
