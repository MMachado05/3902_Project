using Microsoft.Xna.Framework;
using Project.Factories;
using Project.Enemies.Helper;
using Project.Enemies.EnemyStateClasses;
using Project.Items;
using System.Collections.Generic;

namespace Project.Enemies.EnemyClasses
{
    public class Spawner : Enemy, ICustomCollidable
    {
        private readonly EnemyManager enemyManager;
        private static readonly int SpawnOffsetX = 130;
        private static readonly int SpawnOffsetY = 30;
        private static readonly int NickWidth = 128;
        private static readonly int NickHeight = 128;

        public Spawner(EnemyManager manager, Rectangle spawnArea)
            : base(spawnArea)
        {
            enemyManager = manager;
            Health = 25;
        }

        protected override EnemyMovement CreateMovement(Rectangle spawnArea)
        {
            return new EnemyMovement(spawnArea, 0.0f);
        }

        protected override EnemyAnimation CreateAnimation()
        {
            return EnemyAnimationFactory.CreateSpawnerAnimation();
        }

        protected override EnemyStateMachine CreateStateMachine()
        {
            return new EnemyStateMachine(new SpawnerAI(), new IdleState());
        }

        public override void Attack(ItemManager itemManager = null)
        {
            Rectangle spawnPos = new Rectangle(
                Location.X + SpawnOffsetX,
                Location.Y + SpawnOffsetY,
                NickWidth,
                NickHeight
            );

            enemyManager.AddEnemy(new Nick(spawnPos));
        }

        public List<Rectangle> GetCollisionBoxes()
        {
            return new List<Rectangle> { Location };
        }
    }
}
