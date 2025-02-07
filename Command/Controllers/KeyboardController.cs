using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Project
{
	public class KeyboardController : IController
	{
		private Dictionary<Keys, ICommand> _commands;
		private Game1 _game;
        private Keys[] _previouslyPressed = new Keys[0];

        public KeyboardController(Game1 game)
		{
			_game = game;

			_commands = new Dictionary<Keys, ICommand>();
			//_commands.Add(Keys.D0, new QuitCommand(game));
			_commands.Add(Keys.W, new SetCharacterMoveUp(game));
			_commands.Add(Keys.A, new SetCharacterMoveLeft(game));
            _commands.Add(Keys.S, new SetCharacterMoveDown(game));
            _commands.Add(Keys.D, new SetCharacterMoveRight(game));
        }

		public void Update()
		{
            SetCharacterStatic staticCommand = new(_game);
            KeyboardState state = Keyboard.GetState();

			Keys[] currentlyPressed = state.GetPressedKeys();

			if (currentlyPressed.Length > 0)
			{
				foreach (var key in currentlyPressed)
				{
					if (_commands.ContainsKey(key) && !(_previouslyPressed.Contains(key)))
					{
						Debug.WriteLine("Current Key: " + key.ToString());
						_commands.GetValueOrDefault(key).Execute();
					}
				}
			}
			else if (currentlyPressed.Length == 0 && !(_previouslyPressed.Length == 0))
			{
				Debug.WriteLine("I'M STATIC");
				staticCommand.Execute();
			}

			_previouslyPressed = currentlyPressed;
		}
	}
}
