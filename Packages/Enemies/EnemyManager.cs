using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies
{
    public class EnemyManager
    {
        private List<Enemy> enemies;
        private int currentEnemyIndex;

        public EnemyManager()
        {
            enemies = [
                new RedGoriya(new Vector2(384, 224)),
                new Stalfos(new Vector2(384, 224)),
                new Aquamentus(new Vector2(384, 224))
            ];
            currentEnemyIndex = 0;
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

        public Enemy GetCurrentEnemy()
        {
            return enemies.Count > 0 ? enemies[currentEnemyIndex] : null;
        }
    }
}





