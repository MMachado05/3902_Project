using Project.Packages.Sounds;

namespace Project.Commands.PlayerCommands
{
    public class ToggleMusicCommand : ICommand
    {
        private SoundEffectManager _soundEffectManager;

        public ToggleMusicCommand(SoundEffectManager soundEffectManager)
        {
            _soundEffectManager = soundEffectManager;
        }

        public void Execute()
        {
            SoundEffectManager.Instance.ToggleMusic();
        }
    }
}
