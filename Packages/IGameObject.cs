using Microsoft.Xna.Framework;

namespace Project
{
    public interface IGameObject
    {
        Rectangle Location { get; }
        void CollideWith(IGameObject collider);
    }
}
