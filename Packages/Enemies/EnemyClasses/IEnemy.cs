using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project.Characters;
using Project.Items;

namespace Project.Enemies
{
    public interface IEnemy : IGameObject
    {
        float Speed { get; set; }
        int Health { get; set; }
        /*void SetPosition(Vector2 newPosition);*/

        /// <summary>
        /// Trigger this Enemy to perform their attack.
        /// </summary>
        void Attack(ItemManager itemManager);

        /// <summary>
        /// Return Enemy's internal state to not attacking.
        /// </summary>
        void ResetAttackState();

        /// <summary>
        /// Returns the length of this Enemy's attack animation in seconds.
        /// </summary>
        float GetAttackDuration();
        void Update(GameTime gameTime, ItemManager itemManager);
        bool IsDead { get; }
        void TakeDamage(int amount);
        void MoveInDirection(Direction direction);
        List<Direction> PossibleMovementDirections();
    }
}
