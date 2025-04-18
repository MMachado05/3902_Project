using Microsoft.Xna.Framework.Graphics;

namespace Project.UI
{
    public interface IScreen
    {
        void Draw(SpriteBatch spriteBatch);
        void Update();
    }
}
