using Microsoft.Xna.Framework;

namespace Project.Enemies
{
    public interface IEnemy
    {
        Vector2 Position { get; }
        float Speed { get; set; }
        void SetState(IEnemyState newState);
        void SetPosition(Vector2 newPosition);

        void Attack();
        void ResetAttackState();

        float GetAttackDuration();
    }
}
