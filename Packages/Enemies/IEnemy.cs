using Microsoft.Xna.Framework;

namespace Project.Enemies
{
    public interface IEnemy : IGameObject
    {
        Vector2 Position { get; }
        float Speed { get; set; }
        void SetState(IEnemyState newState);
        void SetPosition(Vector2 newPosition);

        /// <summary>
        /// Trigger this Enemy to perform their attack.
        /// </summary>
        void Attack();

        /// <summary>
        /// Return Enemy's internal state to not attacking.
        /// </summary>
        void ResetAttackState();

        /// <summary>
        /// Returns the length of this Enemy's attack animation in seconds.
        /// </summary>
        float GetAttackDuration();
    }
}
