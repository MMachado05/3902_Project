using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{

    public class PlayerSpriteFactory
    {

        // Osama: Put these in the Jira Board!!!

        // TODO: Might be good to have multiple factories for different sprite types (e.g.
        // enemies, characters, items, etc.)
        // TODO: Also I'm using way too many magic numbers, I should give these their own
        // private fields to make modifying things easier.

        private Texture2D playerSpriteSheet;

        private int scale;
        private int widthPixels;
        private int heightPixels;
        // TODO: In the future, we'll want all drawing to be delegated to some external
        // "renderer" object. Scaling, destRectangles... all that stuff should be done
        // somewhere else.

        private static PlayerSpriteFactory instance = new PlayerSpriteFactory();

        public static PlayerSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private PlayerSpriteFactory()
        {
            this.scale = 2;
            this.widthPixels = 32;
            this.heightPixels = 32;
        }

        public void LoadAllTextures(ContentManager content)
        {
            this.playerSpriteSheet = content.Load<Texture2D>("HoodedCharacterTextureTemplate");
        }

        public ISprite NewStoppedPlayerSprite(Direction direction, bool damaged)
        {
            int xOrigin;
            int yOrigin = 0;
            int damagedOffset = damaged ? 128 : 0;

            switch (direction)
            {
                case Direction.Up:
                    xOrigin = 0;
                    break;
                case Direction.Right:
                    xOrigin = 32;
                    break;
                case Direction.Down:
                    xOrigin = 64;
                    break;
                case Direction.Left:
                default: // Just use the left one
                    xOrigin = 96;
                    break;
            }

            return new StationarySprite(this.playerSpriteSheet,
                new Rectangle(xOrigin + damagedOffset, yOrigin, this.widthPixels,
                  this.heightPixels), CharacterState.Stopped);
        }

        public ISprite NewWalkingPlayerSprite(Direction direction, bool damaged)
        {
            int xOrigin;
            int yOrigin = 32;
            int damagedOffset = damaged ? 128 : 0;

            switch (direction)
            {
                case Direction.Up:
                    xOrigin = 0;
                    break;
                case Direction.Right:
                    xOrigin = 32;
                    break;
                case Direction.Down:
                    xOrigin = 64;
                    break;
                case Direction.Left:
                default: // Just use the left one
                    xOrigin = 96;
                    break;
            }

            return new AnimatedLoopSprite(this.playerSpriteSheet,
                new Rectangle(xOrigin + damagedOffset, yOrigin, this.widthPixels,
                  this.heightPixels), 4, CharacterState.Stopped);
        }

        public ISprite NewAttackingPlayerSprite(Direction direction, bool damaged)
        {
            int xOrigin;
            int yOrigin;
            int damagedOffset = damaged ? 128 : 0;
            int width = this.widthPixels;
            int height = this.heightPixels;

            if (direction == Direction.Left) yOrigin = 288;
            else yOrigin = 160;

            if (direction == Direction.Left || direction == Direction.Right) width *= 2;
            else height *= 2;

            switch (direction)
            {
                case Direction.Up:
                    xOrigin = 0;
                    break;
                case Direction.Right:
                    xOrigin = 64;
                    break;
                case Direction.Down:
                    xOrigin = 32;
                    break;
                case Direction.Left:
                default:
                    xOrigin = 64;
                    break;
            }

            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(xOrigin + damagedOffset, yOrigin, width, height),
                4, CharacterState.Attacking, CharacterState.FinishedAttack);
        }
    }
}
