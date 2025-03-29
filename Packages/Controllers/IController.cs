namespace Project.Controllers
{
    public interface IController
    {
        /// <summary>
        /// Process new states from either external devices or other, higher-level
        /// game components, and change current state of the game based on these factors.
        /// </summary>
        void Update();
    }
}
