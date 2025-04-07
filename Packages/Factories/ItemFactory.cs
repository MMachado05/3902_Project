using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Characters.Enums;
using Project.Sprites;
using Project.Sprites.ConcreteClasses;

namespace Project.Factories
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
        private Texture2D _basicAttackAtlas;

        private static readonly ItemFactory instance = new ItemFactory();

        public static ItemFactory Instance => instance;

        public void LoadAllTextures(ContentManager content)
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
            this._basicAttackAtlas = content.Load<Texture2D>("BasicAttack");
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

        public ISprite CreateBasicAttackSprite(Direction direction)
        {
            int sourceX = 0, sourceY = 0;
            switch (direction)
            {
                case Direction.Down:
                    sourceX = 32;
                    break;
                case Direction.Right:
                    sourceX = 64;
                    break;
                case Direction.Left:
                    sourceX = 96;
                    break;
                case Direction.Up:
                default:
                    break;
            }

            return new SingleAnimationSprite(this._basicAttackAtlas,
                new Rectangle(sourceX, sourceY, 32, 32), 4, CharacterState.Attacking,
                CharacterState.FinishedAttack);
        }
    }
}
