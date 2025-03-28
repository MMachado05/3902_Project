using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public interface IItem : IGameObject
    {
        Rectangle PositionRect { get; set; }
        float Speed { get; set; }
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
