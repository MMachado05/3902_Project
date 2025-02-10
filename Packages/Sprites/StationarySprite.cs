using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project;

public class StationarySprite : AbstractSprite
{
    public StationarySprite(Texture2D texture, Rectangle source, int scaleFactor)
      : base(texture, source, scaleFactor)
    {
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        // Calculate destination rectangle
        Rectangle destRectangle = new Rectangle();

        int scaledHalfWidth = base.widthPixels / 2 * base.scaleFactor;
        int scaledHalfHeight = base.heightPixels / 2 * base.scaleFactor;

        destRectangle.X = (int)(position.X - scaledHalfWidth);
        destRectangle.Y = (int)(position.Y - scaledHalfHeight);
        destRectangle.Width = base.widthPixels * base.scaleFactor;
        destRectangle.Height = base.heightPixels * base.scaleFactor;

        spriteBatch.Draw(base.texture, destRectangle, base.source, Color.White);
    }

    public override void Update()
    {
        // Empty method, stationary sprite
    }
}
