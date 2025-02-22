using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project.Packages.Items
{
    public class ItemFactory
    {
        private Texture2D heartTexture;
        private Texture2D arrowTexture;
        private Texture2D bombTexture;

        private static ItemFactory instance = new ItemFactory();


        public static ItemFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.heartTexture = content.Load<Texture2D>("heart");
            this.arrowTexture = content.Load<Texture2D>("arrow");
            this.bombTexture = content.Load<Texture2D>("bomb");
        }
        public Arrow createArrow()
        {
            ISprite arrowSprite = new StationarySprite(this.arrowTexture, new Rectangle(0, 0, 32, 32), 3, new SpriteState());
            return new Arrow(arrowSprite);
        }
        public Heart createHeart()
        {
            ISprite heartSprite = new AnimatedLoopSprite(this.heartTexture, new Rectangle(0, 0, 13, 13), 3, 1, new SpriteState());
            return new Heart(heartSprite);
        }
        public Bomb createBomb()
        {
            ISprite bombSprite = new StationarySprite(this.bombTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            return new Bomb(bombSprite);
        }
    }
}
