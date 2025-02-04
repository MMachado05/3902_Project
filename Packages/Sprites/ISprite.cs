using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project; // TODO: Give proper namespace when project matures

public interface ISprite
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
