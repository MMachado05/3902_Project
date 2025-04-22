using System.Collections.Generic;
using Project.Enemies.EnemyClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project.Enemies
{
    public class EnemyManager
    {
        public List<IEnemy> enemies;
        private int currentEnemyIndex;

        public EnemyManager()
        {
            enemies = new List<IEnemy>();
            currentEnemyIndex = 0;
        }


        public void AddEnemy(Enemy enemy)
        {
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }

        public void SwitchToPreviousEnemy()
        {
            if (enemies.Count == 0) return;
            if (currentEnemyIndex <= 0)
                currentEnemyIndex = enemies.Count - 1;
            else
                currentEnemyIndex--;
        }


        public void SwitchToNextEnemy()
        {
            if (enemies.Count == 0) return;
            if (currentEnemyIndex >= enemies.Count - 1)
                currentEnemyIndex = 0;
            else
                currentEnemyIndex++;
        }

        public IEnemy ReturnEnemy()
        {
            return enemies[currentEnemyIndex];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);
                if (enemies[i].IsDead)
                {
                    enemies.RemoveAt(i);
                }
            }
        }
    }
}





