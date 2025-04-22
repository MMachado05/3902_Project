using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Characters.Enums;
using Project.Items;
using Project.Sprites;
using Project.Sprites.ConcreteClasses;

namespace Project.Factories
{
    public class ItemFactory
    {
        private Texture2D heartTexture;
        private Texture2D UpArrowTexture;
        private Texture2D DownArrowTexture;
        private Texture2D LeftArrowTexture;
        private Texture2D RightArrowTexture;
        private Texture2D bombTexture;
        private Texture2D explodingBombTexture;
        private Texture2D swordTexture;
        private Texture2D bowTexture;
        private Texture2D coinTexture;
        private Texture2D keyTexture;
        private Texture2D fireball;
        private Texture2D Boomerang;
        private Texture2D slashTexture;
        private Texture2D explosionTexture;
        private Texture2D _basicAttackAtlas;

        private int _tileWidth;
        private int _tileHeight;

        private static readonly ItemFactory instance = new ItemFactory();

        public static ItemFactory Instance => instance;

        public void LoadAllTextures(ContentManager content, int tileWidth, int tileHeight)
        {
            heartTexture = content.Load<Texture2D>("heart");
            UpArrowTexture = content.Load<Texture2D>("UpArrow");
            DownArrowTexture = content.Load<Texture2D>("DownArrow");
            LeftArrowTexture = content.Load<Texture2D>("LeftArrow");
            RightArrowTexture = content.Load<Texture2D>("RightArrow");
            bombTexture = content.Load<Texture2D>("bomb");
            explodingBombTexture = content.Load<Texture2D>("ExplodingBomb");
            swordTexture = content.Load<Texture2D>("sword");
            bowTexture = content.Load<Texture2D>("bow");
            coinTexture = content.Load<Texture2D>("GluckCoin");
            keyTexture = content.Load<Texture2D>("key");
            fireball = content.Load<Texture2D>("fireball");
            Boomerang = content.Load<Texture2D>("Boomerang");
            slashTexture = content.Load<Texture2D>("swordSlash");
            explosionTexture = content.Load<Texture2D>("explosion");
            this._basicAttackAtlas = content.Load<Texture2D>("BasicAttack");

            this._tileWidth = tileWidth;
            this._tileHeight = tileHeight;
        }

        public ISprite CreateUpArrowSprite() => new StationarySprite(UpArrowTexture, new Rectangle(0, 0, 16, 16), new CharacterState());
        public ISprite CreateDownArrowSprite() => new StationarySprite(DownArrowTexture, new Rectangle(0, 0, 16, 16), new CharacterState());
        public ISprite CreateLeftArrowSprite() => new StationarySprite(LeftArrowTexture, new Rectangle(0, 0, 16, 16), new CharacterState());
        public ISprite CreateRightArrowSprite() => new StationarySprite(RightArrowTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public ISprite CreateHeartSprite() => new AnimatedLoopSprite(heartTexture, new Rectangle(0, 0, 13, 13), 1, new CharacterState());

        public ISprite CreateBombSprite() => new StationarySprite(bombTexture, new Rectangle(0, 0, 16, 16), new CharacterState());
        public ISprite CreateExplodingBombSprite() => new AnimatedLoopSprite(explodingBombTexture, new Rectangle(0, 0, 16, 16), 1, new CharacterState());

        public ISprite CreateSwordSprite() => new StationarySprite(swordTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public ISprite CreateBowSprite() => new StationarySprite(bowTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public ISprite CreateCoinSprite() => new StationarySprite(coinTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public ISprite CreateKeySprite() => new StationarySprite(keyTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public ISprite CreateFireballSprite() => new StationarySprite(fireball, new Rectangle(0, 0, 8, 8), CharacterState.Walking);

        public ISprite CreateBoomerangSprite() => new AnimatedLoopSprite(Boomerang, new Rectangle(0, 0, 8, 8), 4, CharacterState.Walking);

        public ISprite CreateSlashSprite() => new StationarySprite(slashTexture, new Rectangle(0, 0, 16, 16), new CharacterState());
        public ISprite CreateExplosionSprite() => new StationarySprite(explosionTexture, new Rectangle(0, 0, 16, 16), new CharacterState());

        public IItem CreateBasicAttack(Direction direction, int locationX, int locationY)
        {
            Rectangle location = new Rectangle(locationX, locationY, _tileWidth, _tileHeight);
            ISprite sprite = this.CreateBasicAttackSprite(direction);
            return new BasicAttack(sprite, location);
        }

        public int BasicAttackWidth { get => _tileWidth; }
        public int BasicAttackHeight { get => _tileHeight; }

        private ISprite CreateBasicAttackSprite(Direction direction)
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
