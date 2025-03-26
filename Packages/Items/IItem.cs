using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public interface IItem : IGameObject
    {
        float Speed { get; set; }
        void Update();
        void Draw(SpriteBatch spriteBatch, Rectangle dest);
    }
}
