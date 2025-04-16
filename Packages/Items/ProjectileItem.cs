
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Sprites;
using Project.Items;

public class ProjectileItem : Item
{
    public override Rectangle Location { get; set; }
    public override float Speed { get; set; }
    public Vector2 Direction { get; }
    private readonly Rectangle initialPosition;
    private readonly float maxDistance;
    private bool returning;
    public readonly Vector2? returnTarget;

    public ProjectileItem(Rectangle position, Vector2 direction, ISprite sprite, float speed, float maxDistance)
        : base(sprite)
    {
        Location = position;
        Direction = direction;
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
            : Direction;

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
