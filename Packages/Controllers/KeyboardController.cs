using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Project
{
	public class KeyboardController : IController
	{
		private Dictionary<Keys, ICommand> _commands;

		public KeyboardController(Game1 game)
		{
			_commands = new Dictionary<Keys, ICommand>();
			//_commands.Add(Keys.D0, new QuitCommand(game));
			//_commands.Add(Keys.D1, new SetSpritesCommand(game, Game1.LuigiSpriteNames.Static));
			//_commands.Add(Keys.D2, new SetSpritesCommand(game, Game1.LuigiSpriteNames.Animated));
			//_commands.Add(Keys.D3, new SetSpritesCommand(game, Game1.LuigiSpriteNames.MovingStatic));
			//_commands.Add(Keys.D4, new SetSpritesCommand(game, Game1.LuigiSpriteNames.MovingAnimated));

		}

		// seems redudent, going to leave it out for now
		//public void RegisterCommand(Keys key, ICommand command)
		//{
		//    _commands[key] = command;
		//}

		public void Update()
		{
			KeyboardState state = Keyboard.GetState();

			Keys[] currentlyPressed = state.GetPressedKeys();

			foreach (var key in currentlyPressed)
			{
				if (_commands.ContainsKey(key))
				{
					_commands.GetValueOrDefault(key).Execute();
				}

			}
		}
	}
}
