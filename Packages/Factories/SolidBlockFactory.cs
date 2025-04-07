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

        public IBlock CreateDoor(Rectangle dest)
        {
            return new DoorBlock(_textureAtlas,
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
