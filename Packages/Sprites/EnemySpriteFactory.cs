using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    public class EnemySpriteFactory
    {
        private Texture2D redGoriyaSpriteSheet;
        private Texture2D stalfosSpriteSheet;
        private Texture2D aquamentusSpriteSheet;
        private Texture2D fireball;

        private Texture2D boomerang;

        private int scale = 2;

        private static EnemySpriteFactory instance = new EnemySpriteFactory();

        public static EnemySpriteFactory Instance => instance;

        private EnemySpriteFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            redGoriyaSpriteSheet = content.Load<Texture2D>("RedGoriya");
            stalfosSpriteSheet = content.Load<Texture2D>("Stalfos");
            aquamentusSpriteSheet = content.Load<Texture2D>("Aquamentus");
            fireball = content.Load<Texture2D>("fireball");
            boomerang = content.Load<Texture2D>("boomerang");
        }

        // ----------------- RED GORIYA SPRITES -----------------
        public ISprite NewRedGoriyaIdleUp() => new StationarySprite(redGoriyaSpriteSheet, new Rectangle(0, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewRedGoriyaIdleRight() => new StationarySprite(redGoriyaSpriteSheet, new Rectangle(32, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewRedGoriyaIdleDown() => new StationarySprite(redGoriyaSpriteSheet, new Rectangle(64, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewRedGoriyaIdleLeft() => new StationarySprite(redGoriyaSpriteSheet, new Rectangle(96, 0, 32, 32), scale, SpriteState.Stopped);

        public ISprite NewRedGoriyaWalkingUp() => new AnimatedLoopSprite(redGoriyaSpriteSheet, new Rectangle(0, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewRedGoriyaWalkingRight() => new AnimatedLoopSprite(redGoriyaSpriteSheet, new Rectangle(32, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewRedGoriyaWalkingDown() => new AnimatedLoopSprite(redGoriyaSpriteSheet, new Rectangle(64, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewRedGoriyaWalkingLeft() => new AnimatedLoopSprite(redGoriyaSpriteSheet, new Rectangle(96, 32, 32, 32), scale, 4, SpriteState.Walking);

        public ISprite NewRedGoriyaAttackingUp() => new SingleAnimationSprite(redGoriyaSpriteSheet, new Rectangle(0, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 48);
        public ISprite NewRedGoriyaAttackingRight() => new SingleAnimationSprite(redGoriyaSpriteSheet, new Rectangle(64, 160, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 16);
        public ISprite NewRedGoriyaAttackingDown() => new SingleAnimationSprite(redGoriyaSpriteSheet, new Rectangle(32, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 16);
        public ISprite NewRedGoriyaAttackingLeft() => new SingleAnimationSprite(redGoriyaSpriteSheet, new Rectangle(64, 288, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 48);

        // ----------------- STALFOS SPRITES -----------------
        public ISprite NewStalfosIdleUp() => new StationarySprite(stalfosSpriteSheet, new Rectangle(0, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewStalfosIdleRight() => new StationarySprite(stalfosSpriteSheet, new Rectangle(32, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewStalfosIdleDown() => new StationarySprite(stalfosSpriteSheet, new Rectangle(64, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewStalfosIdleLeft() => new StationarySprite(stalfosSpriteSheet, new Rectangle(96, 0, 32, 32), scale, SpriteState.Stopped);

        public ISprite NewStalfosWalkingUp() => new AnimatedLoopSprite(stalfosSpriteSheet, new Rectangle(0, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewStalfosWalkingRight() => new AnimatedLoopSprite(stalfosSpriteSheet, new Rectangle(32, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewStalfosWalkingDown() => new AnimatedLoopSprite(stalfosSpriteSheet, new Rectangle(64, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewStalfosWalkingLeft() => new AnimatedLoopSprite(stalfosSpriteSheet, new Rectangle(96, 32, 32, 32), scale, 4, SpriteState.Walking);

        public ISprite NewStalfosAttackingUp() => new SingleAnimationSprite(stalfosSpriteSheet, new Rectangle(0, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 48);
        public ISprite NewStalfosAttackingRight() => new SingleAnimationSprite(stalfosSpriteSheet, new Rectangle(64, 160, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 16);
        public ISprite NewStalfosAttackingDown() => new SingleAnimationSprite(stalfosSpriteSheet, new Rectangle(32, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 16);
        public ISprite NewStalfosAttackingLeft() => new SingleAnimationSprite(stalfosSpriteSheet, new Rectangle(64, 288, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 48);

        // ----------------- AQUAMENTUS SPRITES -----------------
        public ISprite NewAquamentusIdleUp() => new StationarySprite(aquamentusSpriteSheet, new Rectangle(0, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewAquamentusIdleRight() => new StationarySprite(aquamentusSpriteSheet, new Rectangle(32, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewAquamentusIdleDown() => new StationarySprite(aquamentusSpriteSheet, new Rectangle(64, 0, 32, 32), scale, SpriteState.Stopped);
        public ISprite NewAquamentusIdleLeft() => new StationarySprite(aquamentusSpriteSheet, new Rectangle(96, 0, 32, 32), scale, SpriteState.Stopped);

        public ISprite NewAquamentusWalkingUp() => new AnimatedLoopSprite(aquamentusSpriteSheet, new Rectangle(0, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewAquamentusWalkingRight() => new AnimatedLoopSprite(aquamentusSpriteSheet, new Rectangle(32, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewAquamentusWalkingDown() => new AnimatedLoopSprite(aquamentusSpriteSheet, new Rectangle(64, 32, 32, 32), scale, 4, SpriteState.Walking);
        public ISprite NewAquamentusWalkingLeft() => new AnimatedLoopSprite(aquamentusSpriteSheet, new Rectangle(96, 32, 32, 32), scale, 4, SpriteState.Walking);

        public ISprite NewAquamentusAttackingUp() => new SingleAnimationSprite(aquamentusSpriteSheet, new Rectangle(0, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 48);
        public ISprite NewAquamentusAttackingRight() => new SingleAnimationSprite(aquamentusSpriteSheet, new Rectangle(64, 160, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 16);
        public ISprite NewAquamentusAttackingDown() => new SingleAnimationSprite(aquamentusSpriteSheet, new Rectangle(32, 160, 32, 64), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originY: 16);
        public ISprite NewAquamentusAttackingLeft() => new SingleAnimationSprite(aquamentusSpriteSheet, new Rectangle(64, 288, 64, 32), scale, 4, SpriteState.Attacking, SpriteState.FinishedAttack, originX: 48);

        // ----------------- PROJECTILE SPRITES -----------------
        public ISprite NewFireball() => new StationarySprite(fireball, new Rectangle(0, 0, 8, 8), scale, SpriteState.Stopped);
        public ISprite NewBoomerang() => new AnimatedLoopSprite(boomerang, new Rectangle(0, 0, 8, 8), scale, 4, SpriteState.Walking);
    }
}
