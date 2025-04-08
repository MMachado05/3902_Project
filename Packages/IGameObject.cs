using Microsoft.Xna.Framework;

namespace Project
{
    public interface IGameObject
    {
        Rectangle Location { get; set; }
        int PlayerHealthEffect { get; }
        bool IsPassable { get; }
        bool SwitchRoom{get;set;}

        void CollideWith(IGameObject collider);
    }
}
