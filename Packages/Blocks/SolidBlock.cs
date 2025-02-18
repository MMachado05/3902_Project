
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Blocks
{
    public class SolidBlock : IBlock
    {
        private Rectangle Source;
        private Rectangle Destination;
        Texture2D texture;
        SpriteBatch spriteBatch;


        public SolidBlock(SpriteBatch spriteBatch, Texture2D texture, Rectangle src, Rectangle dst)
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            Source = src;
            Destination = dst;
        }
        public void Draw()
        {
            spriteBatch.Draw(texture, Destination, Source, Color.White);



        }

    }
}