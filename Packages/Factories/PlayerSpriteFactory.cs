using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Characters.Enums;
using Project.Sprites;
using Project.Sprites.ConcreteClasses;

namespace Project.Factories
{

    public class PlayerSpriteFactory
    {

        private Texture2D playerSpriteSheet;

        private int widthPixels;
        private int heightPixels;

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
                  this.heightPixels), 4, CharacterState.Walking);
        }

        public ISprite NewAttackingPlayerSprite(Direction direction, bool damaged)
        {
            int xOrigin;
            int yOrigin = 0;
            int damagedOffset = damaged ? 128 : 0;

            // NOTE: From previous method of creating attack animations, where we included
            // the sprite for the attack in the sprite proper.
            //
            /*int width = this.widthPixels;*/
            /*int height = this.heightPixels;*/
            /**/
            /*if (direction == Direction.Left) yOrigin = 288;*/
            /*else yOrigin = 160;*/
            /**/
            /*if (direction == Direction.Left || direction == Direction.Right) width *= 2;*/
            /*else height *= 2;*/
            /**/
            /*switch (direction)*/
            /*{*/
            /*    case Direction.Up:*/
            /*        xOrigin = 0;*/
            /*        break;*/
            /*    case Direction.Right:*/
            /*        xOrigin = 64;*/
            /*        break;*/
            /*    case Direction.Down:*/
            /*        xOrigin = 32;*/
            /*        break;*/
            /*    case Direction.Left:*/
            /*    default:*/
            /*        xOrigin = 64;*/
            /*        break;*/
            /*}*/

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
                default:
                    xOrigin = 96;
                    break;
            }

            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(xOrigin + damagedOffset, yOrigin, this.widthPixels, this.heightPixels),
                4, CharacterState.Attacking, CharacterState.FinishedAttack);
        }
    }
}
