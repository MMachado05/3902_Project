using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public class SingleAnimationSprite : AbstractSprite
    {
        private int maxFrames;
        private int currFrame;
        private int topLeftXInitial;
        private int topLeftYInitial;

        /// <summary>
        /// maxFrames is literal; if there are four frames of anmimation, pass in 4 as
        /// the constructor argument.
        /// </summary>
        public SingleAnimationSprite(Texture2D texture, Rectangle sourceInitial, 
            int scaleFactor, int maxFrames) : base(texture, sourceInitial, scaleFactor)
        {
            this.maxFrames = maxFrames - 1;
            this.currFrame = 0;
            this.topLeftXInitial = sourceInitial.X;
            this.topLeftYInitial = sourceInitial.Y;
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
            Console.WriteLine("ATTcurrFrame: " + this.currFrame);
            if (this.currFrame < this.maxFrames)
            {
                this.currFrame++;
                base.source.Y = this.topLeftYInitial + (base.heightPixels * this.currFrame);
            }
        }
    }
}
