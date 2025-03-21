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
                new Rectangle(xOrigin + damagedOffset, yOrigin, this.widthPixels, this.heightPixels),
                scale, SpriteState.Stopped);
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
                new Rectangle(xOrigin + damagedOffset, yOrigin, this.widthPixels, this.heightPixels),
                scale, 4, SpriteState.Stopped);
        }

        public ISprite NewAttackingPlayerSprite(Direction direction, bool damaged)
        {
            int xOrigin;
            int yOrigin;
            int damagedOffset = damaged ? 128 : 0;
            int xSpriteOrigin = -1;
            int ySpriteOrigin = -1;
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
                    ySpriteOrigin = 16;
                    break;
                case Direction.Right:
                    xOrigin = 64;
                    xSpriteOrigin = 48;
                    break;
                case Direction.Down:
                    xOrigin = 32;
                    ySpriteOrigin = 48;
                    break;
                case Direction.Left:
                default:
                    xOrigin = 64;
                    xSpriteOrigin = 16;
                    break;
            }

            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(xOrigin + damagedOffset, yOrigin, width, height), scale, 4, SpriteState.Attacking,
                SpriteState.FinishedAttack, originX: xSpriteOrigin, originY: ySpriteOrigin);
        }

        // Stopped Player sprites
        [System.Obsolete("Deprecated; call NewStoppedPlayerSprite(Direction.Up, false)")]
        public ISprite NewUpStoppedPlayer()
        {
            return new StationarySprite(this.playerSpriteSheet,
                new Rectangle(0, 0, 32, 32), scale, SpriteState.Stopped);
        }
        [System.Obsolete("Deprecated; call NewStoppedPlayerSprite(Direction.Right, false)")]
        public ISprite NewRightStoppedPlayer()
        {
            return new StationarySprite(this.playerSpriteSheet,
                new Rectangle(32, 0, 32, 32), scale, SpriteState.Stopped);
        }
        [System.Obsolete("Deprecated; call NewStoppedPlayerSprite(Direction.Down, false)")]
        public ISprite NewDownStoppedPlayer()
        {
            return new StationarySprite(this.playerSpriteSheet,
                new Rectangle(64, 0, 32, 32), scale, SpriteState.Stopped);
        }
        [System.Obsolete("Deprecated; call NewStoppedPlayerSprite(Direction.Left, false)")]
        public ISprite NewLeftStoppedPlayer()
        {
            return new StationarySprite(this.playerSpriteSheet,
                new Rectangle(96, 0, 32, 32), scale, SpriteState.Stopped);
        }

        // Walking Player sprites
        [System.Obsolete("Deprecated; call NewWalkingPlayerSprite(Direction.Up, false)")]
        public ISprite NewUpWalkingPlayer()
        {
            return new AnimatedLoopSprite(this.playerSpriteSheet,
                new Rectangle(0, 32, 32, 32), scale, 4, SpriteState.Walking);
        }
        [System.Obsolete("Deprecated; call NewWalkingPlayerSprite(Direction.Right, false)")]
        public ISprite NewRightWalkingPlayer()
        {
            return new AnimatedLoopSprite(this.playerSpriteSheet,
                new Rectangle(32, 32, 32, 32), scale, 4, SpriteState.Walking);
        }
        [System.Obsolete("Deprecated; call NewWalkingPlayerSprite(Direction.Down, false)")]
        public ISprite NewDownWalkingPlayer()
        {
            return new AnimatedLoopSprite(this.playerSpriteSheet,
                new Rectangle(64, 32, 32, 32), scale, 4, SpriteState.Walking);
        }
        [System.Obsolete("Deprecated; call NewWalkingPlayerSprite(Direction.Left, false)")]
        public ISprite NewLeftWalkingPlayer()
        {
            return new AnimatedLoopSprite(this.playerSpriteSheet,
                new Rectangle(96, 32, 32, 32), scale, 4, SpriteState.Walking);
        }

        // Attacking Player sprites - single use
        [System.Obsolete("Deprecated; call NewAttackingPlayerSprite(Direction.Up, false)")]
        public ISprite NewUpAttackingPlayer()
        {
            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(0, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 16);
        }
        [System.Obsolete("Deprecated; call NewAttackingPlayerSprite(Direction.Right, false)")]
        public ISprite NewRightAttackingPlayer()
        {
            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(64, 160, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 48);
        }
        [System.Obsolete("Deprecated; call NewAttackingPlayerSprite(Direction.Down, false)")]
        public ISprite NewDownAttackingPlayer()
        {
            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(32, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 48);
        }
        [System.Obsolete("Deprecated; call NewAttackingPlayerSprite(Direction.Left, false)")]
        public ISprite NewLeftAttackingPlayer()
        {
            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(64, 288, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 16);
        }

        // ----------------- DAMAGED --------------------
        // Stopped Player sprites
        [System.Obsolete("Deprecated; call NewStoppedPlayerSprite(Direction.Up, true)")]
        public ISprite NewDamagedUpStoppedPlayer()
        {
            return new StationarySprite(this.playerSpriteSheet,
                new Rectangle(128 + 0, 0, 32, 32), scale, SpriteState.Stopped);
        }
        [System.Obsolete("Deprecated; call NewStoppedPlayerSprite(Direction.Right, true)")]
        public ISprite NewDamagedRightStoppedPlayer()
        {
            return new StationarySprite(this.playerSpriteSheet,
                new Rectangle(128 + 32, 0, 32, 32), scale, SpriteState.Stopped);
        }
        [System.Obsolete("Deprecated; call NewStoppedPlayerSprite(Direction.Down, true)")]
        public ISprite NewDamagedDownStoppedPlayer()
        {
            return new StationarySprite(this.playerSpriteSheet,
                new Rectangle(128 + 64, 0, 32, 32), scale, SpriteState.Stopped);
        }
        [System.Obsolete("Deprecated; call NewStoppedPlayerSprite(Direction.Left, true)")]
        public ISprite NewDamagedLeftStoppedPlayer()
        {
            return new StationarySprite(this.playerSpriteSheet,
                new Rectangle(128 + 96, 0, 32, 32), scale, SpriteState.Stopped);
        }

        // Walking Player sprites
        [System.Obsolete("Deprecated; call NewWalkingPlayerSprite(Direction.Up, true)")]
        public ISprite NewDamagedUpWalkingPlayer()
        {
            return new AnimatedLoopSprite(this.playerSpriteSheet,
                new Rectangle(128 + 0, 32, 32, 32), scale, 4, SpriteState.Walking);
        }
        [System.Obsolete("Deprecated; call NewWalkingPlayerSprite(Direction.Right, true)")]
        public ISprite NewDamagedRightWalkingPlayer()
        {
            return new AnimatedLoopSprite(this.playerSpriteSheet,
                new Rectangle(128 + 32, 32, 32, 32), scale, 4, SpriteState.Walking);
        }
        [System.Obsolete("Deprecated; call NewWalkingPlayerSprite(Direction.Down, true)")]
        public ISprite NewDamagedDownWalkingPlayer()
        {
            return new AnimatedLoopSprite(this.playerSpriteSheet,
                new Rectangle(128 + 64, 32, 32, 32), scale, 4, SpriteState.Walking);
        }
        [System.Obsolete("Deprecated; call NewWalkingPlayerSprite(Direction.Left, true)")]
        public ISprite NewDamagedLeftWalkingPlayer()
        {
            return new AnimatedLoopSprite(this.playerSpriteSheet,
                new Rectangle(128 + 96, 32, 32, 32), scale, 4, SpriteState.Walking);
        }

        // Attacking Player sprites - single use
        [System.Obsolete("Deprecated; call NewAttackingPlayerSprite(Direction.Up, true)")]
        public ISprite NewDamagedUpAttackingPlayer()
        {
            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(128 + 0, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 16);
        }
        [System.Obsolete("Deprecated; call NewAttackingPlayerSprite(Direction.Right, true)")]
        public ISprite NewDamagedRightAttackingPlayer()
        {
            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(128 + 64, 160, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 48);
        }
        [System.Obsolete("Deprecated; call NewAttackingPlayerSprite(Direction.Down, true)")]
        public ISprite NewDamagedDownAttackingPlayer()
        {
            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(128 + 32, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 48);
        }
        [System.Obsolete("Deprecated; call NewAttackingPlayerSprite(Direction.Left, true)")]
        public ISprite NewDamagedLeftAttackingPlayer()
        {
            return new SingleAnimationSprite(this.playerSpriteSheet,
                new Rectangle(128 + 64, 288, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 16);
        }
    }


}
