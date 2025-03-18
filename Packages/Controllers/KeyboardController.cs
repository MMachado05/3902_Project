using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Project
{
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> _commands;
        private IEnumerable<Keys> stillPressed;

        public KeyboardController()
        {
            this._commands = new Dictionary<Keys, ICommand>();
        }

        public void RegisterKey(Keys key, ICommand command)
        {
            this._commands.Add(key, command);
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            stillPressed = stillPressed.Intersect(state.GetPressedKeys());

            Keys[] currentlyPressed = state.GetPressedKeys();

            foreach (var key in currentlyPressed)
            {
                if (_commands.ContainsKey(key) && !stillPressed.Contains<Keys>(key))
                {
                    _commands[key].Execute(); // Was: _commands.GetValueOrDefault(key).Execute()
                }
            }
        }
    }
}
