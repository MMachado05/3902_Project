using Project.Factories;
using Project.Sprites;

namespace Project.Enemies.Helper
{
    public static class EnemyAnimationFactory
    {
        public static EnemyAnimation CreateAquamentusAnimation()
        {
            var factory = EnemySpriteFactory.Instance;
            return new EnemyAnimation(
                factory.NewAquamentusIdleLeft(),
                factory.NewAquamentusIdleRight(),
                factory.NewAquamentusIdleLeft(),
                factory.NewAquamentusIdleRight(),
                factory.NewAquamentusWalkingLeft(),
                factory.NewAquamentusWalkingRight(),
                factory.NewAquamentusWalkingLeft(),
                factory.NewAquamentusWalkingRight(),
                factory.NewAquamentusAttackingLeft(),
                factory.NewAquamentusAttackingRight(),
                factory.NewAquamentusAttackingLeft(),
                factory.NewAquamentusAttackingRight()
            );
        }

        public static EnemyAnimation CreateRedGoriyaAnimation()
        {
            var factory = EnemySpriteFactory.Instance;
            return new EnemyAnimation(
                factory.NewRedGoriyaIdleUp(),
                factory.NewRedGoriyaIdleDown(),
                factory.NewRedGoriyaIdleLeft(),
                factory.NewRedGoriyaIdleRight(),
                factory.NewRedGoriyaWalkingUp(),
                factory.NewRedGoriyaWalkingDown(),
                factory.NewRedGoriyaWalkingLeft(),
                factory.NewRedGoriyaWalkingRight(),
                factory.NewRedGoriyaAttackingUp(),
                factory.NewRedGoriyaAttackingDown(),
                factory.NewRedGoriyaAttackingLeft(),
                factory.NewRedGoriyaAttackingRight()
            );
        }

        public static EnemyAnimation CreateStalfosAnimation()
        {
            var factory = EnemySpriteFactory.Instance;
            return new EnemyAnimation(
                factory.NewStalfosIdleUp(),
                factory.NewStalfosIdleDown(),
                factory.NewStalfosIdleLeft(),
                factory.NewStalfosIdleRight(),
                factory.NewStalfosWalkingUp(),
                factory.NewStalfosWalkingDown(),
                factory.NewStalfosWalkingLeft(),
                factory.NewStalfosWalkingRight(),
                factory.NewStalfosAttackingUp(),
                factory.NewStalfosAttackingDown(),
                factory.NewStalfosAttackingLeft(),
                factory.NewStalfosAttackingRight()
            );
        }

        public static EnemyAnimation CreateSpawnerAnimation()
        {
            var staticSprite = EnemySpriteFactory.Instance.NewStaticSpawner();
            return new EnemyAnimation(
                staticSprite, staticSprite, staticSprite, staticSprite,
                staticSprite, staticSprite, staticSprite, staticSprite,
                staticSprite, staticSprite, staticSprite, staticSprite
            );
        }

        public static EnemyAnimation CreateNickAnimation()
        {
            var move = EnemySpriteFactory.Instance.NewNickMoving();
            return new EnemyAnimation(
                move, move, move, move,
                move, move, move, move,
                move, move, move, move
            );
        }
    }
}
