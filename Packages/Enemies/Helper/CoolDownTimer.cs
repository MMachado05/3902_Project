namespace Project.Enemies.Helper
{
    public class CooldownTimer
    {
        private float cooldown;
        private float timer;

        public CooldownTimer(float cooldownSeconds)
        {
            cooldown = cooldownSeconds;
            timer = 0;
        }

        public void Update(float deltaTime)
        {
            if (timer > 0)
                timer -= deltaTime;
        }

        public void Reset()
        {
            timer = cooldown;
        }

        public bool IsReady => timer <= 0;
    }
}
