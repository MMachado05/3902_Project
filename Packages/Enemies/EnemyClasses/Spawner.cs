using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;
using Project.Enemies;
using Project.Factories;
using Project.Sprites;
using Project.Items;
using System;
using Project.Enemies.EnemyStateClasses;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Project.Enemies.EnemyClasses
{
    public class Spawner : Enemy, ICustomCollidable
    {
        private readonly EnemyManager enemyManager;
        private double spawnCooldown;
        private double timeSinceLastSpawn;
        private static readonly int SpawnOffsetX = 130;
        private static readonly int SpawnOffsetY = 30;
        private static readonly int NickWidth = 128;
        private static readonly int NickHeight = 128;

        public Spawner(EnemyManager manager, Rectangle spawnArea)
            : base(spawnArea)
        {
            this.enemyManager = manager;
            this.spawnCooldown = GetRandomCooldown();
            this.timeSinceLastSpawn = 0;
            Health = 25;
            stateMachine = new EnemyStateMachine(new StationaryAI(), new IdleState());
        }

        public override void Update(GameTime gameTime, ItemManager itemManager)
        {
            base.Update(gameTime, itemManager);
            timeSinceLastSpawn += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastSpawn >= spawnCooldown)
            {
                SpawnNick();
                timeSinceLastSpawn = 0;
                spawnCooldown = GetRandomCooldown();
            }
        }

        private void SpawnNick()
        {
            Rectangle spawnPos = new Rectangle(
                Location.X + SpawnOffsetX,
                Location.Y + SpawnOffsetY,
                NickWidth,
                NickHeight
            );
            enemyManager.AddEnemy(new Nick(spawnPos));
        }

        private double GetRandomCooldown()
        {
            Random rand = new Random();
            return rand.NextDouble() * 2.0 + 3.0;
        }

        protected override void LoadAnimations()
        {
            idleUp = EnemySpriteFactory.Instance.NewStaticSpawner();
            idleDown = idleUp;
            idleLeft = idleUp;
            idleRight = idleUp;
            walkUp = idleUp;
            walkDown = idleUp;
            walkLeft = idleUp;
            walkRight = idleUp;
            attackUp = idleUp;
            attackDown = idleUp;
            attackLeft = idleUp;
            attackRight = idleUp;
        }

        public List<Rectangle> GetCollisionBoxes()
        {
            return new List<Rectangle>
            {
                this.Location // generate accurate rectangles
            };
        }

    }
}
