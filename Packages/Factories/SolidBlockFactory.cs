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
        private Texture2D _bossRoomTexture;
        private Texture2D _room1Texture;
        private Texture2D _room2Texture;

        private Texture2D _roo3Texture;

        private Texture2D _room4Texture;

        private Texture2D _room5Texture;

        private Texture2D _room6Texture;

        private Texture2D _room7Texture;
        private Texture2D _room8Texture;





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
            this._bossRoomTexture = content.Load<Texture2D>("bossBackground");

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
                new Rectangle(256, 64, 64, 64), new Rectangle(0, 0, 960, 704));
        }
        public IBlock boosBackground()
        {
            return new BackgroundBlock(_bossRoomTexture,
                new Rectangle(0, 0, 580, 425), new Rectangle(64, 64, 832, 576));
        }
           public IBlock room1Background()
        {
            return new BackgroundBlock(_bossRoomTexture,
                new Rectangle(0, 0, 580, 425), new Rectangle(64, 64, 832, 576));
        }
           public IBlock room2Background()
        {
            return new BackgroundBlock(_bossRoomTexture,
                new Rectangle(0, 0, 580, 425), new Rectangle(64, 64, 832, 576));
        }
           public IBlock room3Background()
        {
            return new BackgroundBlock(_bossRoomTexture,
                new Rectangle(0, 0, 580, 425), new Rectangle(64, 64, 832, 576));
        }
           public IBlock room4Background()
        {
            return new BackgroundBlock(_bossRoomTexture,
                new Rectangle(0, 0, 580, 425), new Rectangle(64, 64, 832, 576));
        }
           public IBlock room5Background()
        {
            return new BackgroundBlock(_bossRoomTexture,
                new Rectangle(0, 0, 580, 425), new Rectangle(64, 64, 832, 576));
        }
           public IBlock room6Background()
        {
            return new BackgroundBlock(_bossRoomTexture,
                new Rectangle(0, 0, 580, 425), new Rectangle(64, 64, 832, 576));
        }
           public IBlock room7Background()
        {
            return new BackgroundBlock(_bossRoomTexture,
                new Rectangle(0, 0, 580, 425), new Rectangle(64, 64, 832, 576));
        }
           public IBlock room8Background()
        {
            return new BackgroundBlock(_bossRoomTexture,
                new Rectangle(0, 0, 580, 425), new Rectangle(64, 64, 832, 576));
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
