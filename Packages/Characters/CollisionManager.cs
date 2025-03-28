namespace Project.Packages.Characters
{
    // Needs to be public now for PlayerBlockCollisionCommand.cs
    public class CollisionManager
    {
        public void Collide(IGameObject a, IGameObject b)
        {
            if (a.Location.Intersects(b.Location))
            {
                a.CollideWith(b);
                b.CollideWith(a);
            }
        }
    }
}
