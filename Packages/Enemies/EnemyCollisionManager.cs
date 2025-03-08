using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Enemies.EnemyClasses;
using Project.Blocks;

namespace Project.Enemies;
public class EnemyCollisionManager
{
    public void HandleBlockCollision(Enemy enemy, SolidBlock block)
    {
        Rectangle enemyBounds = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, 64, 64);
        Rectangle blockBounds = block.BoundingBox;

        Vector2 previousPosition = enemy.Position;
        Vector2 newPosition = previousPosition;

        if (enemyBounds.Right > blockBounds.Left && previousPosition.X < blockBounds.Left)
        {
            newPosition.X = blockBounds.Left - enemyBounds.Width; // Push left
        }
        else if (enemyBounds.Left < blockBounds.Right && previousPosition.X > blockBounds.Right)
        {
            newPosition.X = blockBounds.Right; // Push right
        }

        if (enemyBounds.Bottom > blockBounds.Top && previousPosition.Y < blockBounds.Top)
        {
            newPosition.Y = blockBounds.Top - enemyBounds.Height; // Push up
        }
        else if (enemyBounds.Top < blockBounds.Bottom && previousPosition.Y > blockBounds.Bottom)
        {
            newPosition.Y = blockBounds.Bottom; // Push down
        }

        enemy.SetPosition(newPosition);
    }

    public void UpdateEnemyCollisions(Enemy enemy, List<object> blocks)
    {
        foreach (var block in blocks)
        {
            SolidBlock blockHolder = (SolidBlock)block;
            if (new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, 64, 64).Intersects(blockHolder.BoundingBox))
            {
                ICommand collisionCommand = new EnemyBlockCollisionCommand(enemy, blockHolder, this);
                collisionCommand.Execute();
            }
        }
    }
}

