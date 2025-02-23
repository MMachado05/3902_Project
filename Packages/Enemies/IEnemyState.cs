namespace Project.Enemies
{
    public interface IEnemyState
    {
        /// <summary>
        /// Update progress completed within this EnemyState.
        /// </summary>
        void Update(IEnemy enemy);
    }
}
