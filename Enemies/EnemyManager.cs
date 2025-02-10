using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;

namespace Project.Enemies;
public class EnemyManager
{
    private List<Enemy> enemies;
    private int currentEnemyIndex;

    public EnemyManager()
    {
        enemies = new List<Enemy>
            {
                new Goblin(new Vector2(100, 100)),
                new Skeleton(new Vector2(100, 100)),
                new Dragon(new Vector2(100, 100))
            };
        currentEnemyIndex = 0;
    }

    public void Update(GameTime gameTime)
    {
        if (enemies.Count > 0)
        {
            enemies[currentEnemyIndex].Update();
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

    public Enemy GetCurrentEnemy()
    {
        return enemies.Count > 0 ? enemies[currentEnemyIndex] : null;
    }
}

