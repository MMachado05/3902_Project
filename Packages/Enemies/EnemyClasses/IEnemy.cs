using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Items;
using Project.Characters;

namespace Project.Enemies
{
    public interface IEnemy : IGameObject
    {
        int Health { get; set; }
        float Speed { get; set; }
        bool IsDead { get; }
        Rectangle Location { get; set; }

        void Update(GameTime gameTime, ItemManager itemManager);
        void Attack(ItemManager itemManager);
        void ResetAttackState();
        float GetAttackDuration();
        void TakeDamage(int amount);
        void MoveInDirection(Direction direction);
        List<Direction> PossibleMovementDirections();
    }
}