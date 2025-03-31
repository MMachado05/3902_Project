using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Rooms.Blocks.ConcreteClasses
{
    public class BackgroundBlock : IBlock
    {
        public bool IsPassable { get => false; }

        public Rectangle Location => throw new System.NotImplementedException();

        public void CollideWith(IGameObject collider) { /* Intentionally empty */ }

        public void Draw(SpriteBatch sb)
        {
            throw new System.NotImplementedException();
        }
    }
}
