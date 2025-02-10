using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project;

public class SpriteFactory
{
    // TODO: Might be good to have multiple factories for different sprite types (e.g.
    // enemies, characters, items, etc.)
    // TODO: Also I'm using way too many magic numbers, I should give these their own
    // private fields to make modifying things easier.

    private Texture2D playerSpriteSheet;

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
    }

    public void LoadAllTextures(ContentManager content)
    {
        this.playerSpriteSheet = content.Load<Texture2D>("Character32x32TextureTemplate"); // TODO: Replace with actual
    }

    // Stopped Player sprites
    public ISprite NewUpStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(0, 0, 32, 32), 2);
    }
    public ISprite NewRightStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(32, 0, 32, 32), 2);
    }
    public ISprite NewDownStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(64, 0, 32, 32), 2);
    }
    public ISprite NewLeftStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(96, 0, 32, 32), 2);
    }

    // Walking Player sprites
    public ISprite NewUpWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(0, 32, 32, 32), 2, 4);
    }
    public ISprite NewRightWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(32, 32, 32, 32), 2, 4);
    }
    public ISprite NewDownWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(64, 32, 32, 32), 2, 4);
    }
    public ISprite NewLeftWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(96, 32, 32, 32), 2, 4);
    }

    // Attacking Player sprites - single use
    public ISprite NewUpAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(0, 160, 32, 64), 2, 4);
    }
    public ISprite NewRightAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(64, 160, 64, 32), 2, 4);
    }
    public ISprite NewDownAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(32, 160, 32, 64), 2, 4);
    }
    public ISprite NewLeftAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(64, 288, 64, 32), 2, 4);
    }

    // ----------------- DAMAGED --------------------
    // Stopped Player sprites
    public ISprite NewDamagedUpStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(128 + 0, 0, 32, 32), 2);
    }
    public ISprite NewDamagedRightStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(128 + 32, 0, 32, 32), 2);
    }
    public ISprite NewDamagedDownStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(128 + 64, 0, 32, 32), 2);
    }
    public ISprite NewDamagedLeftStoppedPlayer()
    {
      return new StationarySprite(this.playerSpriteSheet,
          new Rectangle(128 + 96, 0, 32, 32), 2);
    }

    // Walking Player sprites
    public ISprite NewDamagedUpWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(128 + 0, 32, 32, 32), 2, 4);
    }
    public ISprite NewDamagedRightWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(128 + 32, 32, 32, 32), 2, 4);
    }
    public ISprite NewDamagedDownWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(128 + 64, 32, 32, 32), 2, 4);
    }
    public ISprite NewDamagedLeftWalkingPlayer()
    {
      return new AnimatedLoopSprite(this.playerSpriteSheet,
          new Rectangle(128 + 96, 32, 32, 32), 2, 4);
    }

    // Attacking Player sprites - single use
    public ISprite NewDamagedUpAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(128 + 0, 160, 32, 64), 2, 4);
    }
    public ISprite NewDamagedRightAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(128 + 64, 160, 64, 32), 2, 4);
    }
    public ISprite NewDamagedDownAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(128 + 32, 160, 32, 64), 2, 4);
    }
    public ISprite NewDamagedLeftAttackingPlayer()
    {
      return new SingleAnimationSprite(this.playerSpriteSheet,
          new Rectangle(128 + 64, 288, 64, 32), 2, 4);
    }
}
