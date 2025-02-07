using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
	public class CharacterManager
	{
		private SpriteBatch _spriteBatch;
		private CharacterFactory _characterFactory = new();
		private Movement _movement;
		private ISprite _currentSprite;
		private int _speed = 3;

		public enum CharacterState
		{
			Static, Animated
		}
		private CharacterState _state = CharacterState.Static;
		public CharacterState State
		{
			get { return _state; } set { _state = value; }
		}

		private Movement.Direction _direction = Movement.Direction.Down;
		public Movement.Direction Direction
		{
			get { return _direction; } set { _direction = value; } 
		}

		private Rectangle _destinationRectangle;
		public Rectangle DestinationRectangle
		{ get { return _destinationRectangle; } set { _destinationRectangle = value; } }

		public CharacterManager()
		{
        }

		public void LoadContent(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, ContentManager content)
		{
			_destinationRectangle = new Rectangle((graphics.PreferredBackBufferWidth / 2),
                                   (graphics.PreferredBackBufferHeight / 2),
                                   32, 32);
			_spriteBatch = spriteBatch;
			_characterFactory.LoadContent(content);
			ReplaceCharacter(State, Direction, 3);
        }

		public void ReplaceCharacter(CharacterState name, Movement.Direction direction, int printScale)
		{
			State = name;
			Direction = direction;
			_destinationRectangle.Width = 32 * printScale;
            _destinationRectangle.Height = 32 * printScale;
			if (State.Equals(CharacterState.Animated))
			{
				_movement = new Movement(DestinationRectangle, Direction, _speed);
			}
			_currentSprite = _characterFactory.ReplaceCharacter(this);
        }

        public void ReplaceCharacter(CharacterState name, int printScale)
        {
            State = name;
            _destinationRectangle.Width = 32 * printScale;
            _destinationRectangle.Height = 32 * printScale;
            if (State.Equals(CharacterState.Animated))
            {
                _movement = new Movement(DestinationRectangle, Direction, _speed);
            }
            _currentSprite = _characterFactory.ReplaceCharacter(this);
        }

        public void Update(GameTime gameTime)
		{
			if (State.Equals(CharacterState.Animated))
			{
				DestinationRectangle = _movement.UpdateMovement(gameTime);
				(_currentSprite as Renderer).DestinationRectangle = DestinationRectangle;
				_currentSprite.Update(gameTime);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			_currentSprite.Draw(spriteBatch);
		}
	}
}
