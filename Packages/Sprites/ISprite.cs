using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project; // TODO: Give proper namespace when project matures

public interface ISprite
{
    SpriteType Type { get; }
    void Update();
    void Draw(SpriteBatch spriteBatch, Vector2 position);
}
