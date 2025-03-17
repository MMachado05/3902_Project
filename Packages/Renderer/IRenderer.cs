using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.renderer
{
    public interface IRenderer
    {
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
