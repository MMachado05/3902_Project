using System;
using Project.Rooms.Blocks.ConcreteClasses;

namespace Project.Packages.Characters
{
    // Needs to be public now for PlayerBlockCollisionCommand.cs
    public class CollisionManager
    {
        public void Collide(IGameObject a, IGameObject b)
        {
            if (a.Location.Intersects(b.Location))
            {
                if (b is DoorBlock door)
                {
                    Console.WriteLine("Player collided with DoorBlock at " + door.Location); // ✅
                    door.SwitchRoom = true;
                }
                a.CollideWith(b);
                b.CollideWith(a);
            }
        }
    }
}
