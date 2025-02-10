using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{

    public class StationarySprite : AbstractSprite
    {
        public StationarySprite(Texture2D texture, Rectangle source, int scaleFactor)
          : base(texture, source, scaleFactor)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // Kev changes:
            
            // // Calculate destination rectangle
            // Rectangle destRectangle = new Rectangle();

            // int scaledHalfWidth = base.widthPixels / 2 * base.scaleFactor;
            // int scaledHalfHeight = base.heightPixels / 2 * base.scaleFactor;

            // destRectangle.X = (int)(position.X - scaledHalfWidth);
            // destRectangle.Y = (int)(position.Y - scaledHalfHeight);
            // destRectangle.Width = base.widthPixels * base.scaleFactor;
            // destRectangle.Height = base.heightPixels * base.scaleFactor;

            // Calculate destination rectangle
            Rectangle destRectangle = new Rectangle();
            destRectangle.X = (int)(position.X - (base.widthPixels / 2)) * base.scaleFactor;
            destRectangle.Y = (int)(position.Y - (base.heightPixels / 2)) * base.scaleFactor;
            destRectangle.Width = (int)(position.X + (base.widthPixels / 2)) * base.scaleFactor;
            destRectangle.Height = (int)(position.Y + (base.heightPixels / 2)) * base.scaleFactor;

            // Temp error handling
            if (texture == null)
            {
                throw new System.Exception("Texture is null! Ensure LoadAllTextures() from Sprite Factory is called before creating sprites.");
            }

            spriteBatch.Draw(base.texture, destRectangle, base.source, Color.White);
        }

        public override void Update()
        {
            // Empty method, stationary sprite
        }
    }
}
