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
        private Texture2D swordTexture;
        private Texture2D bowTexture;
        private Texture2D coinTexture;
        private Texture2D keyTexture;
        private Texture2D slashTexture;
        private Texture2D explosionTexture;

        private static readonly ItemFactory instance = new ItemFactory();

        public static ItemFactory Instance => instance;

        public void LoadContent(ContentManager content)
        {
            heartTexture = content.Load<Texture2D>("heart");
            arrowTexture = content.Load<Texture2D>("arrow");
            bombTexture = content.Load<Texture2D>("bomb");
            swordTexture = content.Load<Texture2D>("sword");
            bowTexture = content.Load<Texture2D>("bow");
            coinTexture = content.Load<Texture2D>("GluckCoin");
            keyTexture = content.Load<Texture2D>("key");
            slashTexture = content.Load<Texture2D>("swordSlash");
            explosionTexture = content.Load<Texture2D>("explosion");
        }

        public IItem CreateArrow(Vector2 position)
        {
            ISprite arrowSprite = new StationarySprite(arrowTexture, new Rectangle(0, 0, 32, 32), 3, new SpriteState());
            return new ProjectileItem(position, new Vector2(1, 0), arrowSprite, 5, 500);
        }

        public IItem CreateHeart(Vector2 position)
        {
            ISprite heartSprite = new AnimatedLoopSprite(heartTexture, new Rectangle(0, 0, 13, 13), 3, 1, new SpriteState());
            return new StationaryItem(position, 0, heartSprite);
        }

        public IItem CreateBomb(Vector2 position)
        {
            ISprite bombSprite = new StationarySprite(bombTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            return new StationaryItem(position, 0, bombSprite);
        }

        public IItem CreateSword(Vector2 position)
        {
            ISprite swordSprite = new StationarySprite(swordTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            return new StationaryItem(position, 0, swordSprite);
        }

        public IItem CreateBow(Vector2 position)
        {
            ISprite bowSprite = new StationarySprite(bowTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            return new StationaryItem(position, 0, bowSprite);
        }

        public IItem CreateCoin(Vector2 position)
        {
            ISprite coinSprite = new StationarySprite(coinTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            return new StationaryItem(position, 0, coinSprite);
        }

        public IItem CreateKey(Vector2 position)
        {
            ISprite keySprite = new StationarySprite(keyTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            return new StationaryItem(position, 0, keySprite);
        }

        public IItem CreateSlash(Vector2 position)
        {
            ISprite slashSprite = new StationarySprite(slashTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            return new ProjectileItem(position, new Vector2(1, 0), slashSprite, 0, 500);
        }

        public IItem CreateExplosion(Vector2 position)
        {
            ISprite explosionSprite = new StationarySprite(explosionTexture, new Rectangle(0, 0, 16, 16), 3, new SpriteState());
            return new ProjectileItem(position, new Vector2(1, 0), explosionSprite, 0, 500);
        }
    }
}
