namespace Project
{
    public class GameStateMachine
    {
        public GameState State { get; set; }

        public void StartGame()
        {
            State = GameState.Playing;
        }

        public void TogglePause()
        {
            State = (State == GameState.Paused) ? GameState.Playing : GameState.Paused;
        }
    }
}