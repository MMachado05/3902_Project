using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project;

public class AnimatedSprite : AbstractSprite
{
    private int maxFrames;
    private int currFrame;

    /// <summary>
    /// maxFrames is literal; if there are four frames of anmimation, pass in 4 as
    /// the constructor argument.
    /// </summary>
    public AnimatedSprite(Texture2D texture, Rectangle sourceInitial, int widthPixels, int heightPixels,
        int scaleFactor, int maxFrames) : base(texture, sourceInitial, widthPixels,
          heightPixels, scaleFactor)
    {
        this.maxFrames = maxFrames - 1;
        this.currFrame = 0;
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        Rectangle destRectangle = new Rectangle();
        destRectangle.X = (int)(position.X - (base.widthPixels / 2)) * base.scaleFactor;
        destRectangle.Y = (int)(position.Y - (base.heightPixels / 2)) * base.scaleFactor;
        destRectangle.Width = (int)(position.X + (base.widthPixels / 2)) * base.scaleFactor;
        destRectangle.Height = (int)(position.Y + (base.heightPixels / 2)) * base.scaleFactor;

        spriteBatch.Draw(base.texture, destRectangle, base.source, Color.White);
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
