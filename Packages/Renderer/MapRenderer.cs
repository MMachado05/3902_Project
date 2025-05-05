using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Renderer
{
    public class MapRenderer
    {
        private Texture2D _spriteSheet;
        private Rectangle _sourceRect;
        private Vector2 _position;
        private float _scale;

        public MapRenderer(Texture2D spriteSheet, Vector2 position, float scale)
        {
            _spriteSheet = spriteSheet;
            _position = position;
            _scale = scale;
        }

        public void SetRoomIndex(int index)
        {
            _sourceRect = new Rectangle(index * 32, 0, 32, 32);
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public void SetScale(float scale)
        {
            _scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteSheet, _position, _sourceRect, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }
    }
}
