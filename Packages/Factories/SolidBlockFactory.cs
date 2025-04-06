using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Rooms.Blocks;
using Project.Rooms.Blocks.ConcreteClasses;

namespace Project.Factories
{
    public class SolidBlockFactory
    {
        private Texture2D _textureAtlas;
        private Texture2D _backgroundTextureAtlas;


        private static SolidBlockFactory instance = new SolidBlockFactory();

        public static SolidBlockFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private SolidBlockFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            this._textureAtlas = content.Load<Texture2D>("blocks");
            this._backgroundTextureAtlas = content.Load<Texture2D>("bossBackground");

        }

        /// <summary>
        /// Creates a set of blocks with a brick texture. Select the number of individual
        /// blocks using the "horizontals" and "verticals" parameters.
        /// </summary>
        public IBlock CreateBricks(int horizontals, int verticals, Rectangle dest)
        {
            return new SolidBlock(_textureAtlas,
                new Rectangle(320, 256, 64, 64),
                horizontals, verticals, dest);
        }
          public IBlock GreenBg()
        {
            return new BackgroundBlock(_textureAtlas,
                new Rectangle(256, 64, 64, 64), new Rectangle(0,0,960,704));
        }
          public IBlock boosBackground()
        {
            return new BackgroundBlock(_backgroundTextureAtlas,
                new Rectangle(0, 0, 580, 425), new Rectangle(64,64,832,576));
        }

        public IBlock CreateDoor(Rectangle dest)
        {
            return new SolidBlock(_textureAtlas,
                new Rectangle(384, 256, 64, 64),
                1, 1, dest);
        }

        public IBlock CreateWoodPlanks(int horizontals, int verticals, Rectangle dest)
        {
            return new SolidBlock(_textureAtlas,
                new Rectangle(384, 384, 64, 64),
                horizontals, verticals, dest);
        }
    }
}
