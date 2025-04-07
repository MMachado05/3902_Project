using Microsoft.Xna.Framework;

namespace Project
{
    public interface IGameObject
    {
        Rectangle Location { get; }
        int PlayerHealthEffect { get; }
        bool IsPassable { get; }

        void CollideWith(IGameObject collider);
    }
}
