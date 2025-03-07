
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Blocks
{
    public class SolidBlockSpriteFactory
    {
        private Texture2D SolidBlockSheet;

        private int scale;
        SpriteBatch spriteBatch;
        private static SolidBlockSpriteFactory instance = new SolidBlockSpriteFactory();
        Rectangle Destination;

        public static SolidBlockSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private SolidBlockSpriteFactory()
        {
            this.scale = 2;
            Destination = new Rectangle(400, 250, scale * 32, scale * 32);
        }


        public void LoadAllTextures(ContentManager content)
        {
            this.SolidBlockSheet = content.Load<Texture2D>("blocks");
        }

        // Stopped Player sprites
        public Rectangle boardersBrick()
        {
            return new Rectangle(320, 256, 32, 32);
        }
        public Rectangle doorBlock()
        {
            return new Rectangle(384, 256, 64, 64);
        }
        public Rectangle obstacle()
        {
            return new Rectangle(384, 384, 64, 64);
        }
        public Texture2D getSolidBlockSheet(){
            
            return this.SolidBlockSheet;
        }

    }
}