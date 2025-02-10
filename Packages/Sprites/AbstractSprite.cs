using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project;

public abstract class AbstractSprite : ISprite
{
    protected Texture2D texture;
    protected Rectangle source;
    protected int widthPixels;
    protected int heightPixels;
    protected int scaleFactor;

    public AbstractSprite(Texture2D texture, Rectangle source, int scaleFactor)
    {
      this.texture = texture;
      this.source = source;
      this.widthPixels = source.Width;
      this.heightPixels = source.Height;
      this.scaleFactor = scaleFactor;
    }

    public Rectangle Source
    {
      get
      {
        return this.source;
      }
      set
      {
        this.source = value;
      }
    }

    public abstract void Draw(SpriteBatch spriteBatch, Vector2 position);
    public abstract void Update();
}
