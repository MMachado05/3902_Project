using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{


public abstract class AbstractSprite : ISprite
{
    protected Texture2D texture;
    protected Rectangle source;
    protected SpriteState state;
    protected int widthPixels;
    protected int heightPixels;
    protected int scaleFactor;
    protected int originX;
    protected int originY;

    public AbstractSprite(Texture2D texture, Rectangle source, int scaleFactor,
        SpriteState state, int originX=-1, int originY=-1)
    {
        this.texture = texture;
        this.source = source;
        this.widthPixels = source.Width;
        this.heightPixels = source.Height;
        this.scaleFactor = scaleFactor;
        this.state = state;
        this.originX = originX;
        this.originY = originY;
    }

    public SpriteState State
    {
        get
        {
            return this.state;
        }
        set
        {
            this.state = value;
        }
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

        int xOffset = this.widthPixels / 2 * this.scaleFactor;
        int yOffset = this.heightPixels / 2 * this.scaleFactor;

        if (originX >= 0) xOffset += xOffset - originX;
        if (originY >= 0) yOffset += yOffset - originY;

        destRectangle.X = (int)(position.X - xOffset);
        destRectangle.Y = (int)(position.Y - yOffset);
        destRectangle.Width = this.widthPixels * this.scaleFactor;
        destRectangle.Height = this.heightPixels * this.scaleFactor;

        spriteBatch.Draw(this.texture, destRectangle, this.source, Color.White);
    }
    public abstract void Update();
}

}
