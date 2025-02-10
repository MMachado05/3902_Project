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

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        Rectangle destRectangle = new Rectangle();

        int scaledHalfWidth = this.widthPixels / 2 * this.scaleFactor;
        int scaledHalfHeight = this.heightPixels / 2 * this.scaleFactor;

        destRectangle.X = (int)(position.X - scaledHalfWidth);
        destRectangle.Y = (int)(position.Y - scaledHalfHeight);
        destRectangle.Width = this.widthPixels * this.scaleFactor;
        destRectangle.Height = this.heightPixels * this.scaleFactor;

        spriteBatch.Draw(this.texture, destRectangle, this.source, Color.White);
    }
    public abstract void Update();
}
