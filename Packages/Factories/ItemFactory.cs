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
        private Texture2D fireball;
        private Texture2D Boomerang;
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
            fireball = content.Load<Texture2D>("fireball");
            Boomerang = content.Load<Texture2D>("Boomerang");
            slashTexture = content.Load<Texture2D>("swordSlash");
            explosionTexture = content.Load<Texture2D>("explosion");
        }

        public ISprite CreateArrowSprite() => new StationarySprite(arrowTexture, new Rectangle(0, 0, 32, 32), new CharacterState());

        public ISprite CreateHeartSprite() => new AnimatedLoopSprite(heartTexture, new Rectangle(0, 0, 13, 13), 1, new CharacterState());

        public ISprite CreateBombSprite() => new StationarySprite(bombTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public ISprite CreateSwordSprite() => new StationarySprite(swordTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public ISprite CreateBowSprite() => new StationarySprite(bowTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public ISprite CreateCoinSprite() => new StationarySprite(coinTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public ISprite CreateKeySprite() => new StationarySprite(keyTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public ISprite CreateFireballSprite() => new StationarySprite(fireball, new Rectangle(0, 0, 8, 8), CharacterState.Stopped);

        public ISprite CreateBoomerangSprite() => new AnimatedLoopSprite(Boomerang, new Rectangle(0, 0, 8, 8), 4, CharacterState.Walking);

        public ISprite CreateSlashSprite() => new StationarySprite(slashTexture, new Rectangle(0, 0, 16, 16), new CharacterState());
        public ISprite CreateExplosionSprite() => new StationarySprite(explosionTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

    }
}
