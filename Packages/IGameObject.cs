using Microsoft.Xna.Framework;

namespace Project
{
    public interface IGameObject
    {
        Rectangle Location { get; }
        int PlayerHealthEffect { get; }

        void CollideWith(IGameObject collider);
    }
}
