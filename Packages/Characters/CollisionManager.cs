using Microsoft.Xna.Framework;

namespace Project.Packages.Characters
{
    // Needs to be public now for PlayerBlockCollisionCommand.cs
    public class CollisionManager
    {
        public void Collide(IGameObject a, IGameObject b)
        {
            if (b is ICustomCollidable multiBox)
            {
                foreach (var box in multiBox.GetCollisionBoxes())
                {
                    if (a.Location.Intersects(box))
                    {
                        Vector2 aCenter = new Vector2(a.Location.X + (a.Location.Width / 2),
                                                      a.Location.Y + (a.Location.Height / 2));
                        Vector2 bCenter = new Vector2(box.X + (box.Width / 2),
                                                      box.Y + (box.Height / 2));
                        Vector2 aFrom = Vector2.Subtract(aCenter, bCenter);
                        Vector2 bFrom = Vector2.Subtract(bCenter, aCenter);

                        a.CollideWith(b, aFrom);
                        b.CollideWith(a, bFrom);
                        return;
                    }
                }
            }
            else
            {
                if (a.Location.Intersects(b.Location))
                {
                    Vector2 aCenter = new Vector2(a.Location.X + (a.Location.Width / 2),
                                                  a.Location.Y + (a.Location.Height / 2));
                    Vector2 bCenter = new Vector2(b.Location.X + (b.Location.Width / 2),
                                                  b.Location.Y + (b.Location.Height / 2));
                    Vector2 aFrom = Vector2.Subtract(aCenter, bCenter);
                    Vector2 bFrom = Vector2.Subtract(bCenter, aCenter);

                    a.CollideWith(b, aFrom);
                    b.CollideWith(a, bFrom);
                }
            }
        }

    }
}
