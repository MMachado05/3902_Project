using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project
{
    public class EnemySpriteFactory
    {
        private Texture2D goblinSpriteSheet;
        private Texture2D skeletonSpriteSheet;
        private Texture2D dragonSpriteSheet;

        private int scale = 2;

        private static EnemySpriteFactory instance = new EnemySpriteFactory();

        public static EnemySpriteFactory Instance => instance;

        private EnemySpriteFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            goblinSpriteSheet = content.Load<Texture2D>("Character32x32TextureTemplateGreen");
            skeletonSpriteSheet = content.Load<Texture2D>("Character32x32TextureTemplateBlack");
            dragonSpriteSheet = content.Load<Texture2D>("Character32x32TextureTemplatePink");

            Console.WriteLine("------" + goblinSpriteSheet is null);
        }

        // ----------------- GOBLIN SPRITES -----------------
        public ISprite NewGoblinIdleUp() => new StationarySprite(goblinSpriteSheet, new Rectangle(0, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewGoblinIdleRight() => new StationarySprite(goblinSpriteSheet, new Rectangle(32, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewGoblinIdleDown() => new StationarySprite(goblinSpriteSheet, new Rectangle(64, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewGoblinIdleLeft() => new StationarySprite(goblinSpriteSheet, new Rectangle(96, 0, 32, 32), scale, SpriteState.Stopped);

        public ISprite NewGoblinWalkingUp() => new AnimatedLoopSprite(goblinSpriteSheet, new Rectangle(0, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewGoblinWalkingRight() => new AnimatedLoopSprite(goblinSpriteSheet, new Rectangle(32, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewGoblinWalkingDown() => new AnimatedLoopSprite(goblinSpriteSheet, new Rectangle(64, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewGoblinWalkingLeft() => new AnimatedLoopSprite(goblinSpriteSheet, new Rectangle(96, 32, 32, 32), scale, 4, SpriteState.Walking);

        public ISprite NewGoblinAttackingUp() => new SingleAnimationSprite(goblinSpriteSheet, new Rectangle(0, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 48);
        public ISprite NewGoblinAttackingRight() => new SingleAnimationSprite(goblinSpriteSheet, new Rectangle(64, 160, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 16);
        public ISprite NewGoblinAttackingDown() => new SingleAnimationSprite(goblinSpriteSheet, new Rectangle(32, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 16);
        public ISprite NewGoblinAttackingLeft() => new SingleAnimationSprite(goblinSpriteSheet, new Rectangle(64, 288, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 48);

        // ----------------- SKELETON SPRITES -----------------
        public ISprite NewSkeletonIdleUp() => new StationarySprite(skeletonSpriteSheet, new Rectangle(0, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewSkeletonIdleRight() => new StationarySprite(skeletonSpriteSheet, new Rectangle(32, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewSkeletonIdleDown() => new StationarySprite(skeletonSpriteSheet, new Rectangle(64, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewSkeletonIdleLeft() => new StationarySprite(skeletonSpriteSheet, new Rectangle(96, 0, 32, 32), scale, SpriteState.Stopped);

        public ISprite NewSkeletonWalkingUp() => new AnimatedLoopSprite(skeletonSpriteSheet, new Rectangle(0, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewSkeletonWalkingRight() => new AnimatedLoopSprite(skeletonSpriteSheet, new Rectangle(32, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewSkeletonWalkingDown() => new AnimatedLoopSprite(skeletonSpriteSheet, new Rectangle(64, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewSkeletonWalkingLeft() => new AnimatedLoopSprite(skeletonSpriteSheet, new Rectangle(96, 32, 32, 32), scale, 4, SpriteState.Walking);

        public ISprite NewSkeletonAttackingUp() => new SingleAnimationSprite(skeletonSpriteSheet, new Rectangle(0, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 48);
        public ISprite NewSkeletonAttackingRight() => new SingleAnimationSprite(skeletonSpriteSheet, new Rectangle(64, 160, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 16);
        public ISprite NewSkeletonAttackingDown() => new SingleAnimationSprite(skeletonSpriteSheet, new Rectangle(32, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 16);
        public ISprite NewSkeletonAttackingLeft() => new SingleAnimationSprite(skeletonSpriteSheet, new Rectangle(64, 288, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 48);

        // ----------------- DRAGON SPRITES -----------------
        public ISprite NewDragonIdleUp() => new StationarySprite(dragonSpriteSheet, new Rectangle(0, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewDragonIdleRight() => new StationarySprite(dragonSpriteSheet, new Rectangle(32, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewDragonIdleDown() => new StationarySprite(dragonSpriteSheet, new Rectangle(64, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewDragonIdleLeft() => new StationarySprite(dragonSpriteSheet, new Rectangle(96, 0, 32, 32), scale, SpriteState.Stopped);

        public ISprite NewDragonWalkingUp() => new AnimatedLoopSprite(dragonSpriteSheet, new Rectangle(0, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewDragonWalkingRight() => new AnimatedLoopSprite(dragonSpriteSheet, new Rectangle(32, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewDragonWalkingDown() => new AnimatedLoopSprite(dragonSpriteSheet, new Rectangle(64, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewDragonWalkingLeft() => new AnimatedLoopSprite(dragonSpriteSheet, new Rectangle(96, 32, 32, 32), scale, 4, SpriteState.Walking);

        public ISprite NewDragonAttackingUp() => new SingleAnimationSprite(dragonSpriteSheet, new Rectangle(0, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 48);
        public ISprite NewDragonAttackingRight() => new SingleAnimationSprite(dragonSpriteSheet, new Rectangle(64, 160, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 16);
        public ISprite NewDragonAttackingDown() => new SingleAnimationSprite(dragonSpriteSheet, new Rectangle(32, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 16);
        public ISprite NewDragonAttackingLeft() => new SingleAnimationSprite(dragonSpriteSheet, new Rectangle(64, 288, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 48);
    }
}
