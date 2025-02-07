using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/* No Interface unless multiple movement classes are needed. */

namespace Project {

	public class Movement
	{
		private int _speed;
		public enum Direction { Down, Up, Right, Left }
		private Direction _direction;
		private Rectangle _destinationRectangle;

		public Movement(Rectangle destination, Direction direction, int speed)
		{
			_destinationRectangle = destination;
            _direction = direction;
			_speed = speed;
        }

		public Rectangle UpdateMovement(GameTime gameTime)
		{
			switch (_direction)
			{
				case Direction.Down:
                    _destinationRectangle.Y += _speed;
					break;
				case Direction.Up:
                    _destinationRectangle.Y -= _speed;
                    break;
                case Direction.Right:
                    _destinationRectangle.X += _speed;
                    break;
                case Direction.Left:
                    _destinationRectangle.X -= _speed;
                    break;
                default: throw new ArgumentException("No Direction in Movement.cs");
			}
			return _destinationRectangle;
		}
    }
}
