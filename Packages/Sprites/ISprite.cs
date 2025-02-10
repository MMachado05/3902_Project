using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{


    public interface ISprite
    {
        void Update();
        void Draw(SpriteBatch spriteBatch, Vector2 position);
    }
}
