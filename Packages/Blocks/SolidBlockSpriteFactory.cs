using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Destination = new Rectangle(0, 0, scale * 32, scale * 32);
        }


        public void LoadAllTextures(ContentManager content)
        {
            this.SolidBlockSheet = content.Load<Texture2D>("SolidBlocks32x32");
        }

        // Stopped Player sprites
        public SolidBlock grayBrick(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            return new SolidBlock(spriteBatch, this.SolidBlockSheet,
                new Rectangle(0, 0, 32, 32), Destination);
        }
        public SolidBlock grayGrassBrick(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            return new SolidBlock(spriteBatch, this.SolidBlockSheet,
                new Rectangle(32, 0, 32, 32), Destination);
        }
        public SolidBlock whiteBrick(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            return new SolidBlock(spriteBatch, this.SolidBlockSheet,
                new Rectangle(64, 0, 32, 32), Destination);
        }
        public SolidBlock orangeBrick(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            return new SolidBlock(spriteBatch, this.SolidBlockSheet,
                new Rectangle(96, 0, 32, 32), Destination);
        }

    }
}