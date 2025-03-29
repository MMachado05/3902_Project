using Microsoft.Xna.Framework.Graphics;

namespace Project.Items
{
    public interface IItem : IGameObject
    {
        float Speed { get; set; }
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
