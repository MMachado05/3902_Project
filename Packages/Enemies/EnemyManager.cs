using System.Collections.Generic;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies
{
    public class EnemyManager
    {
        public List<Enemy> enemies;
        private int currentEnemyIndex;

        public EnemyManager()
        {
            enemies = new List<Enemy>();
            currentEnemyIndex = 0;
        }
        public void addEnemy(List<Enemy> enemyList){
            enemies = enemyList;
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





