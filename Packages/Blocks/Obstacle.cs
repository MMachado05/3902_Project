using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project;

namespace Project.Blocks
{
    public class Obstacle:IBlock
    {
        private Rectangle Source;
        private Rectangle Destination;
        Texture2D texture;
        SpriteBatch spriteBatch;
        

        public Obstacle(SpriteBatch spriteBatch,Texture2D texture,Rectangle src, Rectangle dst)
        {
            this.texture= texture;
            this.spriteBatch = spriteBatch;
            Source=src;
            Destination=dst;
        }
         public void draw()
        {
         spriteBatch.Draw(texture,Source,Destination,Color.White);
         
            
         
        }

    }
}