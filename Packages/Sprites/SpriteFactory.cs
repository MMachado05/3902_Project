using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{


    public class SpriteFactory
    {
        // TODO: Might be good to have multiple factories for different sprite types (e.g.
        // enemies, characters, items, etc.)
        // TODO: Also I'm using way too many magic numbers, I should give these their own
        // private fields to make modifying things easier.

    private Texture2D playerSpriteSheet;

    private int scale;
    // TODO: In the future, we'll want all drawing to be delegated to some external
    // "renderer" object. Scaling, destRectangles... all that stuff should be done
    // somewhere else.

        private static SpriteFactory instance = new SpriteFactory();

        public static SpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

    private SpriteFactory()
    {
      this.scale = 2;
    }

    public void LoadAllTextures(ContentManager content)
    {
        this.playerSpriteSheet = content.Load<Texture2D>("HoodedCharacterTextureTemplate");
    }

    // Stopped Player sprites
    public ISprite NewUpStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(0, 0, 32, 32), scale, SpriteState.Stopped);
    }
    public ISprite NewRightStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(32, 0, 32, 32), scale, SpriteState.Stopped);
    }
    public ISprite NewDownStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(64, 0, 32, 32), scale, SpriteState.Stopped);
    }
    public ISprite NewLeftStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(96, 0, 32, 32), scale, SpriteState.Stopped);
    }

    // Walking Player sprites
    public ISprite NewUpWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(0, 32, 32, 32), scale, 4, SpriteState.Walking);
    }
    public ISprite NewRightWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(32, 32, 32, 32), scale, 4, SpriteState.Walking);
    }
    public ISprite NewDownWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(64, 32, 32, 32), scale, 4, SpriteState.Walking);
    }
    public ISprite NewLeftWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(96, 32, 32, 32), scale, 4, SpriteState.Walking);
    }

    // Attacking Player sprites - single use
    public ISprite NewUpAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(0, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 16);
    }
    public ISprite NewRightAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(64, 160, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 48);
    }
    public ISprite NewDownAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(32, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 48);
    }
    public ISprite NewLeftAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(64, 288, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 16);
    }

    // ----------------- DAMAGED --------------------
    // Stopped Player sprites
    public ISprite NewDamagedUpStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(128 + 0, 0, 32, 32), scale, SpriteState.Stopped);
    }
    public ISprite NewDamagedRightStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(128 + 32, 0, 32, 32), scale, SpriteState.Stopped);
    }
    public ISprite NewDamagedDownStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(128 + 64, 0, 32, 32), scale, SpriteState.Stopped);
    }
    public ISprite NewDamagedLeftStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(128 + 96, 0, 32, 32), scale, SpriteState.Stopped);
    }

    // Walking Player sprites
    public ISprite NewDamagedUpWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(128 + 0, 32, 32, 32), scale, 4, SpriteState.Walking);
    }
    public ISprite NewDamagedRightWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(128 + 32, 32, 32, 32), scale, 4, SpriteState.Walking);
    }
    public ISprite NewDamagedDownWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(128 + 64, 32, 32, 32), scale, 4, SpriteState.Walking);
    }
    public ISprite NewDamagedLeftWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(128 + 96, 32, 32, 32), scale, 4, SpriteState.Walking);
    }

    // Attacking Player sprites - single use
    public ISprite NewDamagedUpAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(128 + 0, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 16);
    }                                                                                                                   
    public ISprite NewDamagedRightAttackingPlayer()                                                                     
    {                                                                                                                   
      return new SingleAnimationSprite(this.playerSpriteSheet,                                                          
          new Rectangle(128 + 64, 160, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 48);
    }                                                                                                                   
    public ISprite NewDamagedDownAttackingPlayer()                                                                      
    {                                                                                                                   
      return new SingleAnimationSprite(this.playerSpriteSheet,                                                          
          new Rectangle(128 + 32, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 48);
    }                                                                                                                   
    public ISprite NewDamagedLeftAttackingPlayer()                                                                      
    {                                                                                                                   
      return new SingleAnimationSprite(this.playerSpriteSheet,                                                          
          new Rectangle(128 + 64, 288, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 16);
    }
}


}
