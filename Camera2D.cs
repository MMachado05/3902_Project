using Microsoft.Xna.Framework;

namespace Project
{
    public class Camera2D
    {
        public Vector2 Position { get; private set; } = Vector2.Zero;
        public Vector2 Target => _targetPosition;
        public Vector2 Offset => Position;
        public Matrix Transform => Matrix.CreateTranslation(new Vector3(-Position, 0));

        private Vector2 _targetPosition;
        private Vector2 _startPosition;
        private float _transitionProgress;
        private float _transitionSpeed = 1.0f;
        private bool _isTransitioning;

        public bool IsTransitioning => _isTransitioning;
        public float TransitionProgress => _transitionProgress;

        public void StartTransition(Vector2 offset)
        {
            _startPosition = Position;
            _targetPosition = offset;
            _transitionProgress = 0f;
            _isTransitioning = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!_isTransitioning) return;

            _transitionProgress += (float)(gameTime.ElapsedGameTime.TotalSeconds * _transitionSpeed);

            if (_transitionProgress >= 1f)
            {
                _transitionProgress = 1f;
                _isTransitioning = false;
            }

            Position = Vector2.Lerp(_startPosition, _targetPosition, _transitionProgress);
        }
    }
}
